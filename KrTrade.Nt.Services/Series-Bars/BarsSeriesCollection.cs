using KrTrade.Nt.Core.Data;
using System.Collections.Generic;
using Bar = KrTrade.Nt.Core.Bars.Bar;

namespace KrTrade.Nt.Services.Series
{
    public class BarsSeriesCollection : BaseNinjascriptSeriesCollection<IBarsSeries>, IBarsSeriesCollection // BaseNumericSeries
    {

        public int Capacity { get => Info.Capacity; internal set => Info.Capacity = value; }
        public int OldValuesCapacity { get => Info.OldValuesCapacity; internal set => Info.OldValuesCapacity = value; }

        public CurrentBarSeries CurrentBar { get; protected set; }
        public TimeSeries Time { get; protected set; }
        public PriceSeries Open { get; protected set; }
        public PriceSeries High { get; protected set; }
        public PriceSeries Low { get; protected set; }
        public PriceSeries Close { get; protected set; }
        public VolumeSeries Volume { get; protected set; }
        public TickSeries Tick { get; protected set; }

        public BarsSeriesCollection(IBarsService bars) 
            : this(bars, new BarsSeriesCollectionInfo()
            {
                Type = SeriesCollectionType.BARS_SERIES_COLLECTION,
                Capacity = bars?.CacheCapacity ?? Core.Series.Series.DEFAULT_CAPACITY,
                OldValuesCapacity = bars?.RemovedCacheCapacity ?? Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY 
            }) { }
        public BarsSeriesCollection(IBarsService bars, BarsSeriesCollectionInfo info) : base(bars, info)
        {
            CurrentBar = new CurrentBarSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Time = new TimeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Open = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            High = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Low = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Close = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Volume = new VolumeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Tick = new TickSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Add(CurrentBar);
            Add(Time);
            Add(Open);
            Add(High);
            Add(Low);
            Add(Close);
            Add(Volume);
            Add(Tick);
        }

        //public void Add()
        //{
        //    CurrentBar.Add();
        //    Time.Add();
        //    Open.Add();
        //    High.Add();
        //    Low.Add();
        //    Close.Add();
        //    Volume.Add();
        //    Tick.Add();
        //}
        //public void Update()
        //{
        //    CurrentBar.Update();
        //    Time.Update();
        //    Open.Update();
        //    High.Update();
        //    Low.Update();
        //    Close.Update();
        //    Volume.Update();
        //    Tick.Update();
        //}
        //public void RemoveLastElement()
        //{
        //    CurrentBar.RemoveLastElement();
        //    Time.RemoveLastElement();
        //    Open.RemoveLastElement();
        //    High.RemoveLastElement();
        //    Low.RemoveLastElement();
        //    Close.RemoveLastElement();
        //    Volume.RemoveLastElement();
        //    Tick.RemoveLastElement();
        //}
        //public void Reset()
        //{
        //    CurrentBar.Reset();
        //    Time.Reset();
        //    Open.Reset();
        //    High.Reset();
        //    Low.Reset();
        //    Close.Reset();
        //    Volume.Reset();
        //    Tick.Reset();
        //}
        //public override void Dispose()
        //{
        //    CurrentBar.Terminated();
        //    Time.Terminated();
        //    Open.Terminated();
        //    High.Terminated();
        //    Low.Terminated();
        //    Close.Terminated();
        //    Volume.Terminated();
        //    Tick.Terminated();
        //}

        public override void BarUpdate() => ForEach(x => x.BarUpdate());
        public override void BarUpdate(IBarsService updatedBarsService) => ForEach(x => x.BarUpdate(updatedBarsService));

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
