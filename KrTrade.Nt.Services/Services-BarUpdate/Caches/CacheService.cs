using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public class CacheService<TCache> : BarUpdateService<CacheOptions>
        where TCache : IBarUpdateCache
    {
        #region Private members

        private readonly object _input1;
        private readonly object _input2;

        #endregion

        #region Public properties

        public TCache BarUpdateCache { get; private set; }

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
        /// Create <see cref="BaseCacheService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService) : this(barsService,null,null,barsService.Options.CacheOptions.Period, barsService.Options.CacheOptions.Displacement, barsService.Options.CacheOptions.LengthOfRemovedValuesCache, barsService.Options.CacheOptions.BarsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input) : this(barsService,null,null,barsService.Options.CacheOptions.Period, barsService.Options.CacheOptions.Displacement, barsService.Options.CacheOptions.LengthOfRemovedValuesCache, barsService.Options.CacheOptions.BarsIndex)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input, Action<CacheOptions> configureOptions) : base(barsService, configureOptions)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="options">The specified options to configure the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input, CacheOptions options) : base(barsService,options)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance with specified options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input1">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="input2">Second input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="period">The specified period.</param>
        /// <param name="displacement">The displacement respect the input series.</param>
        /// <param name="lengthOfRemovedValuesCache">The length of the removed values cache.</param>
        /// <param name="barsIndex">The index of the 'NijaScript.Bars' used to get the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input1, object input2 = null, int period = Cache.DEFAULT_PERIOD, int displacement = Cache.DEFAULT_DISPLACEMENT,int lengthOfRemovedValuesCache = Cache.DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(barsService,period,displacement,lengthOfRemovedValuesCache,barsIndex)
        {
            _input1 = input1 ?? (input2 ?? barsService.Ninjascript);
            _input2 = input2;
        }

        #endregion

        #region Implementation

        public override string Name => $"{BarUpdateCache}";
        internal override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            BarUpdateCache = (TCache)GetCache(_input1,_input2, Options.Period, Options.Displacement,Options.LengthOfRemovedValuesCache, Options.BarsIndex);
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
        public override string ToLogString() => $"{Name}[0]: {this[0]}";
        
        #endregion

        private IBarUpdateCache GetCache(object input,object input2, int period, int displacement,int lengthOfRemovedValuesCache, int barsIndex)
        {
            if (input == null)
                if(input2 == null)
                    input = Bars.Ninjascript;
                else
                    input = input2;
            Type type = typeof(TCache);
            if (type == typeof(BarsCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new BarsCache(ninjascript, period, displacement,lengthOfRemovedValuesCache, barsIndex);
                return null;
            }
            if (type == typeof(AvgCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new AvgCache(ninjascript, period, displacement,lengthOfRemovedValuesCache, barsIndex);
                if (input is ISeries<double> series)
                    return new AvgCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(HighCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new HighCache(ninjascript, period, displacement, lengthOfRemovedValuesCache, barsIndex);
                if (input is ISeries<double> series)
                    return new HighCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(IndexCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new IndexCache(ninjascript, period, displacement, barsIndex, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(MaxCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MaxCache(ninjascript, period, displacement,lengthOfRemovedValuesCache,barsIndex);
                if (input is ISeries<double> series)
                    return new MaxCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(MinCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MinCache(ninjascript, period, displacement, lengthOfRemovedValuesCache, barsIndex);
                if (input is ISeries<double> series)
                    return new MinCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(RangeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new RangeCache(ninjascript, period, displacement, lengthOfRemovedValuesCache, barsIndex);
                return null;
            }
            if (type == typeof(SumCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new SumCache(ninjascript, period, displacement, lengthOfRemovedValuesCache,barsIndex);
                if (input is ISeries<double> series)
                    return new SumCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(TicksCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TicksCache(ninjascript, period, displacement, lengthOfRemovedValuesCache,barsIndex);
                return null;
            }
            if (type == typeof(TimeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TimeCache(ninjascript, period, displacement, lengthOfRemovedValuesCache, barsIndex);
                if (input is TimeSeries series)
                    return new TimeCache(series, period, displacement, lengthOfRemovedValuesCache);
                return null;
            }
            if (type == typeof(VolumeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new VolumeCache(ninjascript, period, displacement, lengthOfRemovedValuesCache, barsIndex);
                if (input is VolumeSeries series)
                    return new VolumeCache(series, period, displacement,lengthOfRemovedValuesCache);
                return null;
            }
            return null;
        }
    }
}
