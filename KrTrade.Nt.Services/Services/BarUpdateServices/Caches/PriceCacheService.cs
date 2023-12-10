using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market prices.
    /// </summary>
    public class PriceCacheService : SeriesCacheService
    {
        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected PriceCacheService(IBarsService barsService) : base(barsService)
        {
        }

        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected PriceCacheService(IBarsService barsService, int capacity, int displacement) : base(barsService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="PriceCacheService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected PriceCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"PriceCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Inputs[_barsService.Idx];
        public override bool IsBestCandidateValue() => true;

    }
}
