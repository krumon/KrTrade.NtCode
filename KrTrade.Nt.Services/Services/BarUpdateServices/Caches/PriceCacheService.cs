using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the market prices.
    /// </summary>
    public class PriceCacheService : SeriesCacheService
    {
        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="PriceCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected PriceCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="PriceCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected PriceCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="PriceCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected PriceCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"PriceCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Inputs[DataSeriesService.Idx];
        public override bool IsBestCandidateValue() => true;

    }
}
