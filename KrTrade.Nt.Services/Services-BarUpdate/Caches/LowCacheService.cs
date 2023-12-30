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
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected LowCacheService(IBarsService barsService) : base(barsService)
        {
        }

        /// <summary>
        /// Create <see cref="LowCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="LowCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected LowCacheService(IBarsService barsService, int capacity, int displacement) : base(barsService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="LowCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="LowCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected LowCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"LowCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Lows[Bars.ParentBarsIdx];
        public override bool IsBestCandidateValue() => true;
    }
}
