using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market low prices.
    /// </summary>
    public class LowCacheService : SeriesCacheService
    {
        /// <summary>
        /// Create <see cref="LowCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected LowCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="LowCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected LowCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="LowCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected LowCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"LowCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Lows[DataSeriesService.Idx];
        public override bool IsBestCandidateValue() => true;
    }
}
