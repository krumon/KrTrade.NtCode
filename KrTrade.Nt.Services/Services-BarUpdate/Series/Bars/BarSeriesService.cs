//using KrTrade.Nt.Core.Bars;
//using KrTrade.Nt.Services.Series;
//using System;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Represents a cache with bar values.
//    /// </summary>
//    public class BarSeriesService : SeriesService<BarsSeries>, IBarSeriesService
//    {
//        /// <summary>
//        /// Create <see cref="BarSeriesService"/> instance with <see cref="IBarsService"/> options.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BarSeriesService"/>.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        public BarSeriesService(IBarsService barsService) : base(barsService, new SeriesServiceInfo(), new SeriesServiceOptions()) /* new SeriesOptions(){ BarsIndex = barsService.Index }*/
//        {
//        }

//        double IBarSeriesService.this[int index] => throw new NotImplementedException();

//        public CurrentBarSeries CurrentBar => Series.CurrentBar;
//        public TimeSeries Time => Series.Time;
//        public PriceSeries Open => Series.Open;
//        public PriceSeries High => Series.High;
//        public PriceSeries Low => Series.Low;
//        public PriceSeries Close => Series.Close;
//        public VolumeSeries Volume => Series.Volume;
//        public TickSeries Tick => Series.Tick;
//        public Bar GetBar(int barsAgo) => Series.GetBar(barsAgo);
//        public Bar GetBar(int barsAgo, int period) => Series.GetBar(barsAgo);
//        public IList<Bar> GetBars(int barsAgo, int period) => Series.GetBars(barsAgo, period);

//    }
//}
