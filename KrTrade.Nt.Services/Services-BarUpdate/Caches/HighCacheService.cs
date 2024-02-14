﻿//using NinjaTrader.NinjaScript;

//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Cache to store the latest market high prices.
//    /// </summary>
//    public class HighCacheService : SeriesCacheService
//    {
//        /// <summary>
//        /// Create <see cref="HighCache"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="HighCache"/>.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected HighCacheService(IBarsService barsService) : base(barsService)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="HighCache"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="HighCache"/>.</param>
//        /// <param name="capacity">The cache capacity.</param>
//        /// <param name="displacement">The <see cref="HighCache"/> displacement respect the bars collection.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected HighCacheService(IBarsService barsService, int capacity, int displacement) : base(barsService, capacity, displacement)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="HighCache"/> instance and configure it.
//        /// </summary>
//        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="HighCache"/>.</param>
//        /// <param name="configureOptions">The configure options of the service.</param>
//        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
//        protected HighCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
//        {
//        }

//        /// <summary>
//        /// Gets the name of the service.
//        /// </summary>
//        public override string Name => $"HighCache({Capacity})";

//        public override ISeries<double> Series => Ninjascript.Highs[Bars.ParentBarsIdx];
//        //public override bool IsBestCandidateValue() => true;


//    }
//}