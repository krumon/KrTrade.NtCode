using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BaseNinjascriptServiceCollection<IBarsService, IServiceCollectionInfo>, IBarsServiceCollection
    {

        internal BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, ServiceCollectionInfo info, BarsServiceCollectionOptions options) : base(ninjascript, printService, info, options) 
        {
        }
        //internal BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, BarsServiceCollectionOptions options, int capacity) : base(ninjascript, printService, info, options, capacity) { }
        //internal BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceCollectionOptions options) : base(ninjascript, printService, null, options) { }
        //internal BarsServiceCollection(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceCollectionOptions options, int capacity) : base(ninjascript, printService, null, options, capacity) { }
        internal BarsServiceCollection(IBarsManager barsManager) : this(barsManager.Ninjascript, barsManager.PrintService, null, null) { }
        internal BarsServiceCollection(IBarsManager barsManager, BarsServiceCollectionOptions options) : base(barsManager.Ninjascript, barsManager.PrintService, null, options) 
        {
        }

        public int BarsInProgress => Ninjascript.BarsInProgress < 0 || Ninjascript.BarsInProgress > Count ? 0 : Ninjascript.BarsInProgress;
        protected override ServiceCollectionType GetServiceType() => ServiceCollectionType.BARS_COLLECTION;

        new public BarsServiceCollectionOptions Options => (BarsServiceCollectionOptions)base.Options;
        
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

        internal override void Configure(out bool isConfigured) => isConfigured = true;
        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;

            if (Count == 0)
            {
                isDataLoaded = false;
                PrintService.LogError($"'{Name}' Count: 0. '{Name}' must contain at least the primary series.");
                PrintService.LogError($"'{Name}' cannot be configured when data loaded.");
                return;
            }
            if (Count != Ninjascript.BarsArray.Length)
            {
                isDataLoaded = false;
                PrintService.LogError($"'{Name}' Count: {Count} and 'NinjaScript.BarsArray.Length': {Ninjascript.BarsArray.Length} must be the same. ");
                PrintService.LogError($"'{Name}' cannot be configured when data loaded.");
                return;
            }

            CurrentBars = new CurrentBarSeries[Count];
            Times = new TimeSeries[Count];
            Opens = new PriceSeries[Count];
            Highs = new HighSeries[Count];
            Lows = new PriceSeries[Count];
            Closes = new PriceSeries[Count];
            Volumes = new VolumeSeries[Count];
            Ticks = new TickSeries[Count];

            for (int i = 0; i < Count; i++)
            {
                CurrentBars[i] = this[i].CurrentBar;
                Times[i] = this[i].Time;
                Opens[i] = this[i].Open;
                Highs[i] = this[i].High;
                Lows[i] = this[i].Low;
                Closes[i] = this[i].Close;
                Volumes[i] = this[i].Volume;
                Ticks[i] = this[i].Tick;
            }
        }

        public Bar GetBar(int barsAgo, int barsIndex) => _collection[barsIndex].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period, int barsIndex) => _collection[barsIndex].GetBar(barsAgo,period);
        public IList<Bar> GetBars(int barsAgo, int period, int barsIndex) => _collection[barsIndex].GetBars(barsAgo, period);

        public BarsServiceInfo[] InfoArray { get; protected set; }
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

        public override string ToString() => ToString(); //this[BarsInProgress].ToString();
        public string ToString(int tabOrder, int barsAgo, string valuesSeparator = ": ", string elementsSeparator = ", ", bool displayIndex = true, bool displayValues = true, bool displayName = true, bool displayDescription = false)
            => ToString(); //this[BarsInProgress].ToString(tabOrder, null);

        protected override string GetHeaderString() => "BARS";
        protected override string GetParentString() => null;
        protected override string GetDescriptionString() => ToString();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }
    }
}
