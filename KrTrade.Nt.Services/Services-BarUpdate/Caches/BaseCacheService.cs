using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public abstract class BaseCacheService<TCache> : BarUpdateService<CacheOptions>
        where TCache : IBarUpdateCache
    {
        #region Private members

        //private TCache _cache;
        private readonly object _input;
        private readonly int _period = Cache.DEFAULT_PERIOD;
        private readonly int _displacement = 0;
        private readonly int _barsIndex = 0;

        #endregion

        #region Public properties

        protected TCache BarUpdateCache { get; private set; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns><see cref="TElement"/> element.</returns>
        public object this[int index] => BarUpdateCache[index];

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity => BarUpdateCache.Capacity; 

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public int Count => BarUpdateCache.Count;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public bool IsFull => BarUpdateCache.IsFull;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for <see cref="BaseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BaseCacheService(IBarsService barsService) : base(barsService)
        {
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for <see cref="BaseCacheService"/>.</param>
        /// <param name="period">The cache period.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BaseCacheService(IBarsService barsService, object input, int period = Cache.DEFAULT_PERIOD, int displacement = 0, int barsIndex = 0) : base(barsService)
        {
            _input = input;
            _period = period;
            _displacement = displacement;
            _barsIndex = barsIndex;

            //Options.Displacement = displacement;
            //Options.Capacity = period;
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for <see cref="BaseCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BaseCacheService(IBarsService barsService, IConfigureOptions<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        #endregion

        #region Implementation

        internal override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            BarUpdateCache = (TCache)GetCache(_input, _period, _displacement, _barsIndex);
            isDataLoaded = true;
        }
        public override void Update()
        {
            if (Bars.LastBarRemoved)
                BarUpdateCache.RemoveLastElement();
            else if (Bars.BarClosed)
                BarUpdateCache.Add();
            else if (Bars.PriceChanged)
                BarUpdateCache.Update();
            else if (Bars.Tick)
                BarUpdateCache.Update();
        }
        public override string ToLogString() => $"{Name}[{Displacement}]:{this[Count]}";

        #endregion

        private IBarUpdateCache GetCache(object input, int period, int displacement, int barsIndex)
        {
            Type type = typeof(TCache);
            if (type == typeof(BarsCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new BarsCache(ninjascript, period, displacement, barsIndex);
                return null;
            }
            if (type == typeof(AvgCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new AvgCache(ninjascript, period, displacement, barsIndex);
                if (input is ISeries<double> series)
                    return new AvgCache(series, period, displacement);
                return null;
            }
            if (type == typeof(HighCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new HighCache(ninjascript, period, displacement, barsIndex);
                if (input is ISeries<double> series)
                    return new HighCache(series, period, displacement);
                return null;
            }
            if (type == typeof(IndexCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new IndexCache(ninjascript, period, displacement, barsIndex);
                return null;
            }
            if (type == typeof(MaxCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MaxCache(ninjascript, period, displacement, barsIndex);
                if (input is ISeries<double> series)
                    return new MaxCache(series, period, displacement);
                return null;
            }
            if (type == typeof(MinCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MinCache(ninjascript, period, displacement,barsIndex);
                if (input is ISeries<double> series)
                    return new MinCache(series, period, displacement);
                return null;
            }
            if (type == typeof(RangeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new RangeCache(ninjascript, period, displacement, barsIndex);
                return null;
            }
            if (type == typeof(SumCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new SumCache(ninjascript.Inputs[barsIndex], period, displacement);
                if (input is ISeries<double> series)
                    return new SumCache(series, period, displacement);
                return null;
            }
            if (type == typeof(TicksCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TicksCache(ninjascript, period, displacement);
                return null;
            }
            if (type == typeof(TimeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TimeCache(ninjascript, period, displacement,barsIndex);
                if (input is TimeSeries series)
                    return new TimeCache(series, period, displacement);
                return null;
            }
            if (type == typeof(VolumeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new VolumeCache(ninjascript, period, displacement, barsIndex);
                if (input is VolumeSeries series)
                    return new VolumeCache(series, period, displacement);
                return null;
            }
            return null;
        }

    }
}
