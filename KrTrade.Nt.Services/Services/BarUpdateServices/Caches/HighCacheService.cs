using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market high prices.
    /// </summary>
    public class HighCacheService : SeriesCacheService
    {
        /// <summary>
        /// Create <see cref="HighCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="HighCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected HighCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="HighCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="HighCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected HighCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="HighCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="HighCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected HighCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"HighCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Highs[DataSeriesService.Idx];
        public override bool IsBestCandidateValue() => true;
    }
}
