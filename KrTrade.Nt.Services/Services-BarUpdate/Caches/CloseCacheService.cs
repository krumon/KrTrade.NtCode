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
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected CloseCacheService(IBarsService barsService) : base(barsService)
        {
        }

        /// <summary>
        /// Create <see cref="CloseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="CloseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected CloseCacheService(IBarsService barsService, int capacity, int displacement) : base(barsService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="CloseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="CloseCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        protected CloseCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"CloseCache({Capacity})";

        public override ISeries<double> Series => Ninjascript.Closes[Bars.ParentBarsIdx];
        public override bool IsBestCandidateValue() => true;

    }
}
