using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;
using System.Collections.Generic;
using Bar = KrTrade.Nt.Core.Bars.Bar;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{
    public class BarsSeriesCollection : BaseSeriesCollection<IBarsSeries>, IBarsSeriesCollection // BaseNumericSeries
    {

        public int Capacity { get => Info.Capacity; internal set => Info.Capacity = value; }
        public int OldValuesCapacity { get => Info.OldValuesCapacity; internal set => Info.OldValuesCapacity = value; }

        public ICurrentBarSeries CurrentBar { get; protected set; }
        public ITimeSeries Time { get; protected set; }
        public IPriceSeries Open { get; protected set; }
        public IPriceSeries High { get; protected set; }
        public IPriceSeries Low { get; protected set; }
        public IPriceSeries Close { get; protected set; }
        public IVolumeSeries Volume { get; protected set; }
        public ITickSeries Tick { get; protected set; }

        public BarsSeriesCollection(IBarsService bars) 
            : this(bars, new BarsSeriesCollectionInfo()
            {
                Type = SeriesCollectionType.BARS,
                Capacity = bars?.CacheCapacity ?? Globals.SERIES_DEFAULT_CAPACITY,
                OldValuesCapacity = bars?.RemovedCacheCapacity ?? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY 
            }) { }
        public BarsSeriesCollection(IBarsService bars, BarsSeriesCollectionInfo info) : base(bars, info, new ServiceOptions())
        {
            CurrentBar = new CurrentBarSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Time = new TimeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            //Open = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            High = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            //Low = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            //Close = new HighSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Volume = new VolumeSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Tick = new TickSeries(Bars, info.Capacity, info.OldValuesCapacity);
            Add(CurrentBar);
            Add(Time);
            //Add(Open);
            Add(High);
            //Add(Low);
            //Add(Close);
            Add(Volume);
            Add(Tick);
        }

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

        public string ToLogString(int barsAgo) =>
            $"{Name}[{barsAgo}]: Open:{Open[barsAgo]:#,0.00} - High:{High[barsAgo]:#,0.00} - Low:{Low[barsAgo]:#,0.00} - Close:{Close[barsAgo]:#,0.00} - Volume:{Volume[barsAgo]:#,0.##} - Ticks:{Tick[barsAgo]:#,0.##}";

        new protected bool IsValidIndex(int barsAgo, int period)
            => CurrentBar.IsValidIndex(barsAgo, period)
            && Time.IsValidIndex(barsAgo, period)
            && Open.IsValidIndex(barsAgo, period)
            && High.IsValidIndex(barsAgo, period)
            && Low.IsValidIndex(barsAgo, period)
            && Close.IsValidIndex(barsAgo, period)
            && Volume.IsValidIndex(barsAgo, period)
            && Tick.IsValidIndex(barsAgo, period);
        new protected bool IsValidIndexRange(int initialBarsAgo, int finalBarsAgo)
            => CurrentBar.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Time.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Open.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && High.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Low.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Close.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Volume.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Tick.IsValidIndex(initialBarsAgo, finalBarsAgo);

        protected override string GetHeaderString() => "BARS_SERIES";
        protected override string GetParentString() => Bars.ToString();
        protected override string GetDescriptionString() => ToString();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }

        protected override SeriesCollectionType ToElementType()
        {
            throw new System.NotImplementedException();
        }
    }
}
