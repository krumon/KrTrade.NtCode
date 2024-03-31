using KrTrade.Nt.Core.Bars;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents a cache with bar values.
    /// </summary>
    public class BarSeriesService : SeriesService<BarSeries>, IBarSeriesService
    {
        /// <summary>
        /// Create <see cref="BarSeriesService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BarSeriesService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarSeriesService(IBarsService barsService) : base(barsService, barsService?.Ninjascript, new SeriesOptions() /* new SeriesOptions(){ BarsIndex = barsService.Index }*/)
        {
            barsService.PrintService.LogTrace("BarSeriesService constructor.");
        }

        public new double this[int index] => Series[index];
        public override string Name => Series.ToString();
        public CurrentBarSeries CurrentBar => Series.CurrentBar;
        public TimeSeries Time => Series.Time;
        public PriceSeries Open => Series.Open;
        public PriceSeries High => Series.High;
        public PriceSeries Low => Series.Low;
        public PriceSeries Close => Series.Close;
        public VolumeSeries Volume => Series.Volume;
        public TickSeries Tick => Series.Tick;
        public Bar GetBar(int barsAgo) => Series.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => Series.GetBar(barsAgo);
        public IList<Bar> GetBars(int barsAgo, int period) => Series.GetBars(barsAgo, period);

    }
}
