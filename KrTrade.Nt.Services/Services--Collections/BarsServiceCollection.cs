using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Services;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BaseNinjascriptServiceCollection<IBarsService>, IBarsServiceCollection
    {
        public BarsServiceCollection() { }
        public BarsServiceCollection(IEnumerable<IBarsService> elements) : base(elements) { }
        public BarsServiceCollection(int capacity) : base(capacity) { }

        public override string ToString() => GetKey();

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

        public override void Add<TInfo, TOptions>(IService service, TInfo itemInfo, TOptions itemOptions)
        {
            if (service is INinjascriptService ninjascriptService)
                Add(new BarsService(ninjascriptService, itemInfo as BarsServiceInfo, itemOptions as BarsServiceOptions));
        }

        public BarsServiceInfo[] Info { get; protected set; }
        public IndicatorCollection[] Indicators { get; protected set; }
        //public StatsCollection[] Stats { get; protected set; }
        //public FiltersCollection[] Filters { get; protected set; }

        public void MarketData(int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].MarketData();
        }
        public void MarketData(IBarsService updatedBarsSeries, int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].MarketData(updatedBarsSeries);
        }
        public void MarketDepth(int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].MarketDepth();
        }
        public void MarketDepth(IBarsService updatedBarsSeries, int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].MarketDepth(updatedBarsSeries);
        }
        public void Render(int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].Render();
        }
        public void Render(IBarsService updatedBarsSeries, int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].Render(updatedBarsSeries);
        }
        public void Update(int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].Update();
        }
        public void Update(IBarsService updatedBarsSeries, int barsInProgress)
        {
            if (IsValidIndex(barsInProgress))
                _collection[barsInProgress].Update(updatedBarsSeries);
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
