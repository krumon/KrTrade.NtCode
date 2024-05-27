using KrTrade.Nt.Core.Series;
using NinjaTrader.Data;
using System;
using System.Diagnostics;

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

        public SeriesType Type { get => Info.Type; protected set => Info.Type = value; }

        public void Configure()
        {

            // TODO: Delete this line
            Debugger.Break();

            if (IsOutOfConfigurationStates())
                LoggingHelpers.ThrowIsNotConfigureException(Name);
            
            if (_isConfigure && _isDataLoaded)
                return;
            
            if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.Configure && !_isConfigure)
                Configure(out _isConfigure);

            else if (Bars.Ninjascript.State == NinjaTrader.NinjaScript.State.DataLoaded && !_isConfigure)
                Configure(out _isConfigure);
            
        }
        public void DataLoaded()
        {

            // TODO: Delete this line
            Debugger.Break();

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
            Info = info ?? throw new ArgumentNullException(nameof(info));

            Info.OldValuesCapacity = OldValuesCapacity < 1 ? Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY : OldValuesCapacity;
            Info.Capacity = Capacity <= 0 ? Core.Series.Series.DEFAULT_CAPACITY : Capacity > MaxCapacity ? MaxCapacity : Capacity;
        }

        public override string ToString() => ToString(true, ": ", 0);
        public string ToString(int tabOrder, int barsAgo) => ToString(
            includeOwner: true,
            separator: ": ",
            tabOrder: tabOrder,
            barsAgo: barsAgo);
        internal string ToString(bool includeOwner, string separator, int tabOrder, int barsAgo = -1)
        {
            string text = includeOwner ? Info.ToString(Bars.ToString()) : Info.ToString();
            string tab = string.Empty;
            separator = string.IsNullOrEmpty(separator) ? ": " : separator;

            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";
            text += tab;

            text += includeOwner ? Info.ToString(Bars.ToString()) : Info.ToString();

            if (barsAgo > 0 && barsAgo < Count)
            {
                text += $"[{barsAgo}]";
                if (string.IsNullOrEmpty(separator))
                    separator = ": ";
                text += $"{separator}{this[barsAgo]}";
            }

            return text;
        }
        protected abstract string GetValueString(int barsAgo);
        public void Log()
        {
            if (Bars.PrintService == null || !Bars.Options.IsLogEnable)
                return;
            Bars.PrintService.LogValue(ToString());
        }
        public void Log(int barsAgo)
        {
            if (Bars.PrintService == null || !Bars.Options.IsLogEnable)
                return;
            Bars.PrintService.LogValue(ToString(0,barsAgo));
        }

        //public virtual string ToLogString() => ToLogString(Type.ToString(), 0, 0);
        //public virtual string ToLogString(int barsAgo) => ToLogString(Type.ToString(), barsAgo, 0);
        //public virtual string ToLogString(int barsAgo, int tabOrder) => ToLogString(Type.ToString(), barsAgo, tabOrder);
        //protected string ToLogString(string header, int barsAgo) => ToLogString(header, barsAgo, 0);
        //protected virtual string ToLogString(string header, int barsAgo, int tabOrder)
        //{
        //    string text = string.Empty;
        //    string tab = string.Empty;
        //    for (int i = 0; i < tabOrder; i++)
        //        tab += "\t";

        //    text += tab;

        //    if (IsValidIndex(barsAgo))
        //        text += $"{header}({Bars})[{barsAgo}]: {this[barsAgo]}";
        //    else
        //        text += $"{header}({Bars})[{barsAgo}]: 'barsAgo': {barsAgo} is out of range.";

        //    return text;

        //}

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
