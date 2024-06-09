using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseNinjascriptSeries<T> : Series<T>, INinjascriptSeries<T> // Series<T>, INinjascriptSeries
    {
        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        protected IBarsService Bars { get; private set; }

        public string ToString(string name, string description, bool displayIndex, string separator, int tabOrder, int barsAgo, bool displayValue = true)
        {
            string text = string.Empty;
            string tab = string.Empty;

            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";
            text += tab;

            if (!string.IsNullOrEmpty(name))
            {
                text += tab + name;
                if (!string.IsNullOrEmpty(description))
                    text += description;
            }

            if (displayValue)
            {
                if (displayIndex)
                    text += $"[{barsAgo}]";

                if (string.IsNullOrEmpty(separator))
                    separator = ": ";

                if (barsAgo >= 0 && barsAgo < Count)
                    text += $"{separator}{ValueToString(barsAgo)}";
                else
                    text += $"{separator}Warning!!!. Index is out of range.";
            }
            return text;
        }
        public override string ToString() => ToString(
            name: Name,
            description: Bars.ToString(),
            displayIndex: false,
            separator: ": ",
            tabOrder: 0,
            barsAgo: 0,
            displayValue: false);
        public string ToString(int tabOrder, int barsAgo,string separator = ": ", bool displayIndex = true, bool displayValue = true, bool displayName = true, bool displayDescription = false) => ToString(
            name: displayName ? Name : string.Empty,
            description: displayDescription ? Bars.ToString() : string.Empty,
            displayIndex: displayIndex,
            separator: separator,
            tabOrder: tabOrder,  
            barsAgo: barsAgo,
            displayValue: displayValue);
        
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
            Bars.PrintService.LogValue(ToString(0, barsAgo));
        }

        protected abstract string ValueToString(int barsAgo);

        /// <summary>
        /// Create <see cref="BaseNinjascriptSeries{T}"/> default instance with specified parameters.
        /// </summary>
        /// <param name="info">The series information necesary to construct it.</param>
        protected BaseNinjascriptSeries(IBarsService bars, SeriesInfo info) : base(bars.Ninjascript,info)
        {
            Bars = bars ?? throw new ArgumentNullException(nameof(bars));

            Info.OldValuesCapacity = OldValuesCapacity < 1 ? Core.Elements.Series.DEFAULT_OLD_VALUES_CAPACITY : OldValuesCapacity;
            Info.Capacity = Capacity <= 0 ? Core.Elements.Series.DEFAULT_CAPACITY : Capacity > MaxCapacity ? MaxCapacity : Capacity;
        }

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
                Configure(out _isConfigure);

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

        public abstract void Add();
        public abstract void Update();

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

        /// <summary>
        /// Method to configure the service. This method should be used by 
        /// services that can be configured without first passing any filters.
        /// </summary>
        /// <param name="isConfigured">True, if the service has been configure, otherwise false.</param>
        internal abstract void Configure(out bool isConfigured);

        /// <summary>
        /// Method to configure the service when NinjaScript data is loaded. This method should be used by 
        /// services that can be configured without first passing any filters.
        /// </summary>
        /// <param name="isDataLoaded">True, if the service has been configure, otherwise false.</param>
        internal abstract void DataLoaded(out bool isDataLoaded);

    }
}
