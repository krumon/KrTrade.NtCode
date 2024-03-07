using KrTrade.Nt.Core.Bars;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents a cache with bar values.
    /// </summary>
    public class BarsSeriesService : SeriesService<BarsSeries>, IBarsSeriesService
    {
        /// <summary>
        /// Create <see cref="BarsSeriesService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BarsSeriesService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsSeriesService(IBarsService barsService) : base(barsService, barsService?.Ninjascript, new SeriesOptions()
        {
            Capacity = barsService.CacheCapacity,
            OldValuesCapacity = barsService.RemovedCacheCapacity,
            BarsIndex = barsService.Index
            
        }){ }

        public override string Name => _series.ToString();
        public CurrentBarSeries CurrentBar => _series.CurrentBar;
        public TimeSeries Time => _series.Time;
        public PriceSeries Open => _series.Open;
        public PriceSeries High => _series.High;
        public PriceSeries Low => _series.Low;
        public PriceSeries Close => _series.Close;
        public VolumeSeries Volume => _series.Volume;
        public TickSeries Tick => _series.Tick;
        public Bar GetBar(int barsAgo) => _series.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _series.GetBar(barsAgo);
        public IList<Bar> GetBars(int barsAgo, int period) => _series.GetBars(barsAgo, period);

    }
}
