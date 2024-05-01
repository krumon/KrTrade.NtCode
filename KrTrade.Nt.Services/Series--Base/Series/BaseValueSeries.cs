using KrTrade.Nt.Core.Series;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseValueSeries<T> : Series<T>, IValueSeries<T>
        where T : struct
    {

        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        protected IBarsService Bars { get; private set; }

        protected T _candidateValue;
        protected bool IsFirstValueToBeAdded => Count == 0 && IsValidValueToBeAdded(_candidateValue, true);

        public bool IsConfigure => _isConfigure;
        public bool IsDataLoaded => _isDataLoaded;

        public void Configure()
        {
            if (IsOutOfConfigurationStates())
                LoggingHelpers.ThrowIsNotConfigureException(Name);
            if (_isConfigure && _isDataLoaded)
                return;
            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.Configure && !_isConfigure)
                Configure(out _isConfigure);

            else if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded && !_isConfigure)
            {
                Configure(out _isConfigure);
                DataLoaded(out _isDataLoaded);
            }
            else if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded && _isConfigure)
                DataLoaded(out _isDataLoaded);

        }
        public void DataLoaded()
        {
            if (Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.DataLoaded)
                LoggingHelpers.ThrowIsNotConfigureException(Name);

            if (_isConfigure && _isDataLoaded)
                return;

            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded && !_isConfigure)
                Configure(out _isConfigure);

            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded && _isConfigure)
                DataLoaded(out _isDataLoaded);

        }
        public virtual void Terminated() => Dispose();

        internal abstract void Configure(out bool isConfigured);
        internal abstract void DataLoaded(out bool isDataLoaded);

        protected BaseValueSeries(IBarsService bars, BaseSeriesInfo info) : base(info)
        {
            Bars = bars ?? throw new ArgumentNullException(nameof(bars));
            Info = info ?? new SeriesInfo()
            {
                Capacity = DEFAULT_CAPACITY,
                OldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY,
            };
            OldValuesCapacity = OldValuesCapacity < 1 ? Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY : OldValuesCapacity;
            Capacity = Capacity <= 0 ? Core.Series.Series.DEFAULT_CAPACITY : Capacity > MaxCapacity ? MaxCapacity : Capacity;
        }

        public virtual void Add()
        {
            _candidateValue = GetCandidateValue(isCandidateValueToUpdate: false);

            if (Count == 0)
            {
                if (IsValidValueToBeAdded(_candidateValue, false))
                {
                    CurrentValue = _candidateValue;
                    Add(_candidateValue);
                }
                else
                {
                    CurrentValue = default;
                    Add(default);
                }
                return;
            }

            if (IsValidValueToBeAdded(_candidateValue, false))
                Add(_candidateValue);
            else
                Add(default);
        }
        public virtual void Update()
        {
            _candidateValue = GetCandidateValue(isCandidateValueToUpdate: true);

            if (IsValidValueToBeUpdated(_candidateValue))
                this[0] = _candidateValue;
        }

        public virtual void BarUpdate()
        {
            if (Bars.LastBarIsRemoved)
                RemoveLastElement();
            else if (Bars.IsClosed)
                Add();
            else if (Bars.IsPriceChanged || Bars.IsTick)
                Update();
        }
        public virtual void BarUpdate(IBarsService updatedBarsService) { }
        public virtual void MarketData(MarketDataEventArgs args) { }
        public virtual void MarketData(IBarsService updatedBarsService) { }
        public virtual void MarketDepth(MarketDepthEventArgs args) { }
        public virtual void MarketDepth(IBarsService updatedBarsService) { }

        protected abstract T GetCandidateValue(bool isCandidateValueToUpdate);
        protected abstract bool IsValidValue(T candidateValue);
        protected abstract bool IsValidValueToAdd(T candidateValue, bool isFirstValueToAdd);
        protected abstract bool IsValidValueToUpdate(T candidateValue);

        protected virtual bool IsValidValueToBeAdded(T candidateValue, bool isFirstValueToAdd) => IsValidValue(candidateValue) && IsValidValueToAdd(candidateValue, isFirstValueToAdd);
        protected virtual bool IsValidValueToBeUpdated(T candidateValue) => IsValidValue(candidateValue) && IsValidValueToUpdate(candidateValue);

        #region Helper methods

        /// <summary>
        /// Indicates whether NinjaScript is in any of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns></returns>
        protected bool IsInConfigurationStates()
        {
            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.Configure || Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is in running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is 'Historical' or 'Realtime'.</returns>
        protected bool IsInRunningStates()
        {
            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.Historical || Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.Realtime)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure' and 'DataLoaded'.</returns>
        protected bool IsOutOfConfigurationStates()
        {
            if (Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.Configure && Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configure state.
        /// The configure state is 'Configure'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure'.</returns>
        protected bool IsOutOfConfigureState()
        {
            if (Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.Configure)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the data loaded state.
        /// The data loaded state is 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'DataLoaded'.</returns>
        protected bool IsOutOfDataLoadedState()
        {
            if (Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Historical' and 'Realtime'.</returns>
        protected bool IsOutOfRunningStates()
        {
            if (Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.Historical && Bars.Ninjascript.State != NinjaTrader.NinjaScript.State.Realtime)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript indexes are available.
        /// The indexs are 'BarsInProgress' and 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript indexes are greater than -1.</returns>
        protected bool IsNinjaScriptIndexesAvailable()
        {
            if (IsNotAvilableBarsInProgressIdx() && IsNotAvailableFirstBarIdx())
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript data series is available to be updated.
        /// The index is 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvailableFirstBarIdx()
        {
            if (Bars.Ninjascript.CurrentBars[Bars.Ninjascript.BarsInProgress] < 0)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript multi data series is available to be updated.
        /// The index is 'BarsInProgress'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvilableBarsInProgressIdx()
        {
            if (Bars.Ninjascript.BarsInProgress < 0)
                return true;

            return false;
        }

        #endregion

    }
}
