using KrTrade.Nt.Core.Data;
using System.Collections.Generic;
using Bar = KrTrade.Nt.Core.Bars.Bar;

namespace KrTrade.Nt.Services.Series
{
    public class BarsSeries : BaseNinjascriptSeries, IBarsSeries // BaseNumericSeries
    {

        public CurrentBarSeries CurrentBar { get; protected set; }
        public TimeSeries Time { get; protected set; }
        public PriceSeries Open { get; protected set; }
        public PriceSeries High { get; protected set; }
        public PriceSeries Low { get; protected set; }
        public PriceSeries Close { get; protected set; }
        public VolumeSeries Volume { get; protected set; }
        public TickSeries Tick { get; protected set; }

        public BarsSeries(IBarsService bars) 
            : this(bars, new BarsSeriesInfo(
                      BarsSeriesType.INPUT,
                      bars?.CacheCapacity ?? Core.Series.Series.DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY))
        {
        }
        public BarsSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            CurrentBar = new CurrentBarSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Time = new TimeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Open = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            High = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Low = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Close = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Volume = new VolumeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Tick = new TickSeries(Bars, info.Capacity, info.OldValuesCapacity);
        }

        #region Implementation

        internal override void Configure(out bool isConfigured)
        {
            CurrentBar.Configure();
            Time.Configure();
            Open.Configure();
            High.Configure();
            Low.Configure();
            Close.Configure();
            Volume.Configure();
            Tick.Configure();

            isConfigured = 
                CurrentBar.IsConfigure && 
                Time.IsConfigure && 
                Open.IsConfigure && 
                High.IsConfigure && 
                Low.IsConfigure && 
                Close.IsConfigure && 
                Volume.IsConfigure && 
                Tick.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            CurrentBar.DataLoaded();
            Time.DataLoaded();
            Open.DataLoaded();
            High.DataLoaded();
            Low.DataLoaded();
            Close.DataLoaded();
            Volume.DataLoaded();
            Tick.DataLoaded();

            isDataLoaded =
                CurrentBar.IsDataLoaded &&
                Time.IsDataLoaded &&
                Open.IsDataLoaded &&
                High.IsDataLoaded &&
                Low.IsDataLoaded &&
                Close.IsDataLoaded &&
                Volume.IsDataLoaded &&
                Tick.IsDataLoaded;
        }
        public override void Add()
        {
            CurrentBar.Add();
            Time.Add();
            Open.Add();
            High.Add();
            Low.Add();
            Close.Add();
            Volume.Add();
            Tick.Add();
        }
        public override void Update()
        {
            CurrentBar.Update();
            Time.Update();
            Open.Update();
            High.Update();
            Low.Update();
            Close.Update();
            Volume.Update();
            Tick.Update();
        }
        public override void RemoveLastElement()
        {
            CurrentBar.RemoveLastElement();
            Time.RemoveLastElement();
            Open.RemoveLastElement();
            High.RemoveLastElement();
            Low.RemoveLastElement();
            Close.RemoveLastElement();
            Volume.RemoveLastElement();
            Tick.RemoveLastElement();
        }
        public override void Reset()
        {
            CurrentBar.Reset();
            Time.Reset();
            Open.Reset();
            High.Reset();
            Low.Reset();
            Close.Reset();
            Volume.Reset();
            Tick.Reset();
        }
        public override void Dispose()
        {
            CurrentBar.Terminated();
            Time.Terminated();
            Open.Terminated();
            High.Terminated();
            Low.Terminated();
            Close.Terminated();
            Volume.Terminated();
            Tick.Terminated();
        }

        #endregion

        #region Public methods

        public Bar GetBar(int barsAgo)
        {
            if (!IsValidIndex(barsAgo))
                return null;

            Bar bar = new Bar() 
            { 
                Idx = CurrentBar[barsAgo],
                Open = Open[barsAgo],
                High = High[barsAgo],
                Low = Low[barsAgo],
                Close = Close[barsAgo],
                Volume = Volume[barsAgo],
                Ticks = (long)Tick[barsAgo],
             };
            return bar;
        }
        public Bar GetBar(int barsAgo, int period)
        {
            IsValidIndex(barsAgo, period);

            Bar bar = new Bar();
            for (int i = barsAgo + period - 1; i >= 0; i--)
                bar += new Bar()
                {
                    Idx = CurrentBar[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Tick[i],
                };

            return bar;
        }
        public IList<Bar> GetBars(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo, period))
                return null;
            IList<Bar> bars = new List<Bar>();
            for (int i = barsAgo + period - 1; i >= 0; i--)
                bars.Add(new Bar()
                {
                    Idx = CurrentBar[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Tick[i],
                });
            return bars;
        }

        public override string ToString() => 
            $"{Name}[0]: Open:{Open[0]:#,0.00} - High:{High[0]:#,0.00} - Low:{Low[0]:#,0.00} - Close:{Close[0]:#,0.00} - Volume:{Volume[0]:#,0.##} - Ticks:{Tick[0]:#,0.##}";

        #endregion

        protected bool IsValidIndex(int barsAgo)
            => CurrentBar.IsValidIndex(barsAgo)
            && Time.IsValidIndex(barsAgo)
            && Open.IsValidIndex(barsAgo)
            && High.IsValidIndex(barsAgo)
            && Low.IsValidIndex(barsAgo)
            && Close.IsValidIndex(barsAgo)
            && Volume.IsValidIndex(barsAgo)
            && Tick.IsValidIndex(barsAgo);
        protected bool IsValidIndex(int barsAgo, int period)
            => CurrentBar.IsValidIndex(barsAgo, period)
            && Time.IsValidIndex(barsAgo, period)
            && Open.IsValidIndex(barsAgo, period)
            && High.IsValidIndex(barsAgo, period)
            && Low.IsValidIndex(barsAgo, period)
            && Close.IsValidIndex(barsAgo, period)
            && Volume.IsValidIndex(barsAgo, period)
            && Tick.IsValidIndex(barsAgo, period);
        protected bool IsValidIndexRange(int initialBarsAgo, int finalBarsAgo)
            => CurrentBar.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Time.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Open.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && High.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Low.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Close.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Volume.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Tick.IsValidIndex(initialBarsAgo, finalBarsAgo);

    }
}
