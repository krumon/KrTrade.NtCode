using System;

namespace KrTrade.Nt.Services
{
    public class IndicatorSeriesService : SeriesService<IIndicatorSeries>, IIndicatorSeriesService
    {
        /// <summary>
        /// Create <see cref="IndicatorSeriesService"/> instance with specified options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BarSeriesService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public IndicatorSeriesService(IBarsService barsService) : base(barsService, barsService?.Ninjascript, new SeriesOptions()
        {
            //Capacity = barsService.CacheCapacity,
            //OldValuesCapacity = barsService.RemovedCacheCapacity,
            BarsIndex = barsService.Index

        })
        { }

        public new double this[int index] => Series[index];
        public double Max(int displacement = 0, int period = 1) => Series.Max(displacement,period);
        public double Min(int displacement = 0, int period = 1) => Series.Min(displacement,period);
        public double Sum(int displacement = 0, int period = 1) => Series.Sum(displacement,period);
        public double Avg(int displacement = 0, int period = 1) => Series.Avg(displacement, period);
        public double StdDev(int displacement = 0, int period = 1) => Series.StdDev(displacement,period);
        
        public double InterquartilRange(int displacement = 0, int period = 1) => Series.InterquartilRange(displacement,period);
        public double Quartil(int numberOfQuartil, int displacement, int period) => Series.Quartil(numberOfQuartil,displacement, period);
        public double[] Quartils(int displacement = 0, int period = 1) => Series.Quartils(displacement,period);
        
        public double Range(int displacement = 0, int period = 1) => Series.Range(displacement,period);
        public double SwingHigh(int displacement = 0, int strength = 4) => Series.SwingHigh(displacement,strength);
        public double SwingLow(int displacement = 0, int strength = 4) => Series.SwingLow(displacement,strength);
    }
}
