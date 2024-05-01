using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Services;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BaseNinjascriptServiceCollection<IBarsService>, IBarsServiceCollection
    {
        private string _name;

        public BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, string name, BarsServiceOptions options) : base(ninjascript, printService, name, options) { }
        public BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, string name, BarsServiceOptions options, int capacity) : base(ninjascript, printService, name, options, capacity) { }
        public BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceOptions options) : base(ninjascript, printService, null, options) { }
        public BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceOptions options, int capacity) : base(ninjascript, printService, null, options, capacity) { }
        public BarsServiceCollection(IBarsManager barsManager) : base(barsManager.Ninjascript, barsManager.PrintService, null, new BarsServiceOptions()) { }
        public BarsServiceCollection(IBarsManager barsManager, BarsServiceOptions options) : base(barsManager.Ninjascript, barsManager.PrintService, null, options) { }

        public int BarsInProgress => this.Ninjascript.BarsInProgress;
        public override string ToString() => GetKey();
        public override string Name
        {
            get => _name;
            protected set => _name = string.IsNullOrEmpty(value) ? GetKey() : value;
        }
        protected new BarsServiceCollectionOptions _options;
        public new BarsServiceCollectionOptions Options { get => _options ?? new BarsServiceCollectionOptions(); protected set { _options = value; } }

        public CurrentBarSeries CurrentBar => IsValidIndex(0) ? _collection[0].CurrentBar : null;
        public TimeSeries Time => IsValidIndex(0) ? _collection[0].Time : null;
        public PriceSeries Open => IsValidIndex(0) ? _collection[0].Open : null;
        public PriceSeries High => IsValidIndex(0) ? _collection[0].High : null;
        public PriceSeries Low => IsValidIndex(0) ? _collection[0].Low : null;
        public PriceSeries Close => IsValidIndex(0) ? _collection[0].Close : null;
        public VolumeSeries Volume => IsValidIndex(0) ? _collection[0].Volume : null;
        public TickSeries Tick => IsValidIndex(0) ? _collection[0].Tick : null;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }
        public TickSeries[] Ticks { get; protected set; }

        public bool IsUpdated => IsValidIndex(0) && _collection[0].IsUpdated;
        public bool IsClosed => IsValidIndex(0) && _collection[0].IsClosed;
        public bool LastBarIsRemoved => IsValidIndex(0) && _collection[0].LastBarIsRemoved;
        public bool IsTick => IsValidIndex(0) && _collection[0].IsTick;
        public bool IsFirstTick => IsValidIndex(0) && _collection[0].IsFirstTick;
        public bool IsPriceChanged => IsValidIndex(0) && _collection[0].IsPriceChanged;

        public Bar GetBar(int barsAgo, int barsIndex) => _collection[barsIndex].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period, int barsIndex) => _collection[barsIndex].GetBar(barsAgo,period);
        public IList<Bar> GetBars(int barsAgo, int period, int barsIndex) => _collection[barsIndex].GetBars(barsAgo, period);

        public BarsServiceInfo[] Info { get; protected set; }
        //public IndicatorCollection[] Indicators { get; protected set; }
        //public StatsCollection[] Stats { get; protected set; }
        //public FiltersCollection[] Filters { get; protected set; }

        public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].MarketData(args);
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].MarketData(updatedBarsSeries);
        }
        public void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].MarketDepth(args);
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].MarketDepth(updatedBarsSeries);
        }
        public void Render()
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].Render();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].Render(updatedBarsSeries);
        }
        public void BarUpdate()
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].BarUpdate();
        }
        public void BarUpdate(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _collection[BarsInProgress].BarUpdate(updatedBarsSeries);
        }

        private string GetKey()
        {
            string key = "Bars";
            if (_collection != null && _collection.Count > 0)
            {
                key += "[";
                for (int i = 0; i < _collection.Count; i++)
                {
                    key += _collection[i].Key;
                    if (i == _collection.Count - 1)
                        key += ",";
                }
                key += "]";
            }
            else
                key += "[EMPTY]";
            return key;
        }

    }
}
