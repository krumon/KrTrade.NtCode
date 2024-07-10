using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bar = KrTrade.Nt.Core.Bars.Bar;

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
        public ITickSeries Ticks { get; protected set; }

        public BarsSeriesCollection(IBarsService bars) 
            : this(bars, new BarsSeriesCollectionInfo()
            {
                Type = SeriesCollectionType.BARS,
                Capacity = bars?.CacheCapacity ?? Globals.SERIES_DEFAULT_CAPACITY,
                OldValuesCapacity = bars?.RemovedCacheCapacity ?? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY 
            }) { }
        public BarsSeriesCollection(IBarsService bars, BarsSeriesCollectionInfo info) : base(bars, info, new ServiceOptions())
        {
            int capacity = Math.Max(bars.CacheCapacity, info.Capacity);
            int oldValuesCapacity = Math.Max(bars.RemovedCacheCapacity, info.OldValuesCapacity);

            CurrentBar = new CurrentBarSeries(Bars, capacity, oldValuesCapacity);
            Time = new TimeSeries(Bars, capacity, oldValuesCapacity);
            High = new HighSeries(Bars, capacity, oldValuesCapacity);
            Open = High;
            Low = High;
            Close = High;
            Volume = new VolumeSeries(Bars, capacity, oldValuesCapacity);
            Ticks = new TickSeries(Bars, capacity, oldValuesCapacity);
            Add(CurrentBar);
            Add(Time);
            //Add(Open);
            Add(High);
            //Add(Low);
            //Add(Close);
            Add(Volume);
            Add(Ticks);
        }

        public override void BarUpdate()
        {
            Debugger.Break();

            if (Bars.LastBarIsRemoved)
                ForEach(x => { x.RemoveLastElement(); });
            else if (Bars.IsClosed)
                ForEach(x => { x.Add(); });
            else if (Bars.IsPriceChanged || Bars.IsTick)
                ForEach(x => { x.Update(); });
        }

        public Bar GetBar(int barsAgo)
        {
            if (!IsValidIndex(barsAgo))
                return default;

            Bar bar = new Bar() 
            { 
                Idx = CurrentBar[barsAgo],
                Time = Time[barsAgo],
                Open = Open[barsAgo],
                High = High[barsAgo],
                Low = Low[barsAgo],
                Close = Close[barsAgo],
                Volume = Volume[barsAgo],
                Ticks = (long)Ticks[barsAgo],
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
                    Time = Time[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Ticks[i],
                };

            return bar;
        }
        public IList<Bar> GetRange(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo, period))
                return null;
            IList<Bar> bars = new List<Bar>();
            for (int i = barsAgo + period - 1; i >= 0; i--)
                bars.Add(new Bar()
                {
                    Idx = CurrentBar[i],
                    Time = Time[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Ticks[i],
                });
            return bars;
        }

        public string ToLogString(int barsAgo) =>
            $"{Name}[{barsAgo}]: Open:{Open[barsAgo]:#,0.00} - High:{High[barsAgo]:#,0.00} - Low:{Low[barsAgo]:#,0.00} - Close:{Close[barsAgo]:#,0.00} - Volume:{Volume[barsAgo]:#,0.##} - Ticks:{Ticks[barsAgo]:#,0.##}";
        public void Log(int barsAgo) => PrintService?.LogInformation(ToLogString(barsAgo));

        new protected bool IsValidIndex(int barsAgo, int period)
            => CurrentBar.IsValidIndex(barsAgo, period)
            && Time.IsValidIndex(barsAgo, period)
            && Open.IsValidIndex(barsAgo, period)
            && High.IsValidIndex(barsAgo, period)
            && Low.IsValidIndex(barsAgo, period)
            && Close.IsValidIndex(barsAgo, period)
            && Volume.IsValidIndex(barsAgo, period)
            && Ticks.IsValidIndex(barsAgo, period);
        new protected bool IsValidIndexRange(int initialBarsAgo, int finalBarsAgo)
            => CurrentBar.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Time.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Open.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && High.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Low.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Close.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Volume.IsValidIndex(initialBarsAgo, finalBarsAgo)
            && Ticks.IsValidIndex(initialBarsAgo, finalBarsAgo);

        protected override string GetHeaderString() => "BARS_SERIES";
        protected override string GetParentString() => Bars.ToString();
        protected override string GetDescriptionString() => ToString();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }

        protected override SeriesCollectionType ToElementType() => SeriesCollectionType.BARS;
    }
}
