using KrTrade.Nt.Core.Bars;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents a cache with bar values.
    /// </summary>
    public class BarsCacheService : BaseCacheService<BarsCache>, IBarsCacheService
    {

        #region Constructors

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService) : base(barsService)
        {
            //Cache = (TCache)GetCache(20, 0);
            //if (Cache == null) throw new NullReferenceException("The BarUpdateCache() cannot be null.");
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="period">The period to calculate values.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService, int period, int displacement) : base(barsService, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService, int period, int displacement, ISeries<double> input, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService, CacheOptions options) : base(barsService, options)
        {
        }

        #endregion

        #region Public properties


        #endregion

        #region Implementation

        public override string Name => $"BarsCache({BarUpdateCache.Period + BarUpdateCache.Displacement})";
        public IndexCache Index => BarUpdateCache.Index;
        public TimeCache Time => BarUpdateCache.Time;
        public DoubleCache<NinjaScriptBase> Open => BarUpdateCache.Open;
        public HighCache High => BarUpdateCache.High;
        public DoubleCache<NinjaScriptBase> Low => BarUpdateCache.Low;
        public DoubleCache<NinjaScriptBase> Close => BarUpdateCache.Close;
        public VolumeCache Volume => BarUpdateCache.Volume;
        public TicksCache Ticks => BarUpdateCache.Ticks;
        public Bar GetBar(int barsAgo) => BarUpdateCache.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => BarUpdateCache.GetBar(barsAgo);
        public IList<Bar> GetBars(int barsAgo, int period) => BarUpdateCache.GetBars(barsAgo, period);

        #endregion

        #region Public methods



        #endregion

        #region Private methods

        #endregion

    }
}
