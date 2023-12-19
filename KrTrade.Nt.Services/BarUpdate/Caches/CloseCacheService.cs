using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market close prices.
    /// </summary>
    public class CloseCacheService : SeriesCacheService
    {
        /// <summary>
        /// Create <see cref="CloseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected CloseCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="CloseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected CloseCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="CloseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected CloseCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"CloseCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Closes[DataSeriesService.Idx];
        public override bool IsBestCandidateValue() => true;

    }
}
