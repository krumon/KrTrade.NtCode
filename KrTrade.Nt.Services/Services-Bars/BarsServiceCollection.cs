using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BaseNinjascriptServiceCollection<BarsService, BarsServiceCollectionOptions>, IBarsServiceCollection
    {
        public BarsServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, options)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options)
        {
        }

        internal override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;
        }

        public override void Terminated()
        {
            _services.Clear();
            _services = null;
        }

        public new BarsService this[int index] => _services[index];
        public override string Key
        {
            get
            {
                string key = "Bars";
                if (_services != null && _services.Count > 0)
                {
                    key += "[";
                    for (int i = 0; i < _services.Count; i++) 
                    { 
                        key += _services[i].Key;
                        if (i == _services.Count - 1)
                            key += ",";
                    }
                    key += "]";
                }
                else
                    key += "[EMPTY]";
                return key;
            }
        }
        public int BarsInProgress => Ninjascript.BarsInProgress;

        public CurrentBarSeries CurrentBar => IsValidIndex(0) ? _services[0].CurrentBar : null;
        public TimeSeries Time => IsValidIndex(0) ? _services[0].Time : null;
        public PriceSeries Open => IsValidIndex(0) ? _services[0].Open : null;
        public PriceSeries High => IsValidIndex(0) ? _services[0].High : null;
        public PriceSeries Low => IsValidIndex(0) ? _services[0].Low : null;
        public PriceSeries Close => IsValidIndex(0) ? _services[0].Close : null;
        public VolumeSeries Volume => IsValidIndex(0) ? _services[0].Volume : null;
        public TickSeries Tick => IsValidIndex(0) ? _services[0].Tick : null;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }
        public TickSeries[] Ticks { get; protected set; }

        public bool IsUpdated => IsValidIndex(0) && _services[0].IsUpdated;
        public bool IsClosed => IsValidIndex(0) && _services[0].IsClosed;
        public bool LastBarIsRemoved => IsValidIndex(0) && _services[0].LastBarIsRemoved;
        public bool IsTick => IsValidIndex(0) && _services[0].IsTick;
        public bool IsFirstTick => IsValidIndex(0) && _services[0].IsFirstTick;
        public bool IsPriceChanged => IsValidIndex(0) && _services[0].IsPriceChanged;

        public Bar GetBar(int barsAgo, int barsIndex) => _services[barsIndex].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period, int barsIndex) => _services[barsIndex].GetBar(barsAgo,period);
        public IList<Bar> GetBars(int barsAgo, int period, int barsIndex) => _services[barsIndex].GetBars(barsAgo, period);

        public DataSeriesInfo[] DataSeries { get; protected set; }
        public IndicatorCollection[] Indicators { get; protected set; }
        public StatsCollection[] Stats { get; protected set; }
        public FiltersCollection[] Filters { get; protected set; }

        public void MarketData()
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].MarketData();
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].MarketData(updatedBarsSeries);
        }
        public void MarketDepth()
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].MarketDepth();
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].MarketDepth(updatedBarsSeries);
        }
        public void Render()
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].Render();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].Render(updatedBarsSeries);
        }
        public void Update()
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].Update();
        }
        public void Update(IBarsService updatedBarsSeries)
        {
            if (IsValidIndex(BarsInProgress))
                _services[BarsInProgress].Update(updatedBarsSeries);
        }

    }
}
