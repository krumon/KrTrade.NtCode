//using NinjaTrader.NinjaScript;

//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Cache to store the market prices.
//    /// </summary>
//    public class PriceCacheService : SeriesCacheService
//    {
//        /// <summary>
//        /// Create <see cref="PriceCacheService"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected PriceCacheService(IBarsService barsService) : base(barsService)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="PriceCacheService"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.</param>
//        /// <param name="capacity">The cache capacity.</param>
//        /// <param name="displacement">The <see cref="PriceCacheService"/> displacement respect the bars collection.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected PriceCacheService(IBarsService barsService, int capacity, int displacement) : base(barsService, capacity, displacement)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="PriceCacheService"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="PriceCacheService"/>.</param>
//        /// <param name="configureOptions">The configure options of the service.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected PriceCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
//        {
//        }

//        /// <summary>
//        /// Gets the name of the service.
//        /// </summary>
//        public override string Name => $"PriceCache({Capacity})";

//        public override ISeries<double> Series => Ninjascript.Inputs[Bars.ParentBarsIdx];

//        //public override bool IsBestCandidateValue() => true;

//    }
//}
