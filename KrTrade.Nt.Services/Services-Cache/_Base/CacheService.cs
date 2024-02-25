using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public class CacheService<TCache> : BarUpdateService<CacheServiceOptions>
        where TCache : IBarUpdateCache
    {
        #region Private members

        private readonly object _input1;
        private readonly object _input2;
        //protected IBarUpdateCache _cache;

        #endregion

        #region Public properties

        public TCache _cache { get; private set; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns><see cref="TElement"/> element.</returns>
        public object this[int index] => _cache[index];

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity => _cache.Capacity; 

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public int Count => _cache.Count;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public bool IsFull => _cache.IsFull;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="CacheService{TCache}"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService) : this(barsService,null,null,barsService.Capacity, barsService.RemovedCacheCapacity, barsService.Index)
        {
        }

        /// <summary>
        /// Create <see cref="CacheService{TCache}"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input) : this(barsService,null,null,barsService.Capacity, barsService.RemovedCacheCapacity, barsService.Index)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="CacheService{TCache}"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input, Action<CacheServiceOptions> configureOptions) : base(barsService, configureOptions)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="CacheService{TCache}"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="CacheService{TCache}"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="options">The specified options to configure the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input, CacheServiceOptions options) : base(barsService,options)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="CacheService{TCache}"/> instance with specified options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="CacheService{TCache}"/>.</param>
        /// <param name="input1">The input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="input2">Second input series necesary for calculate the new elements of the <see cref="CacheService{TCache}"/></param>
        /// <param name="period">The specified period.</param>
        /// <param name="displacement">The displacement respect the input series.</param>
        /// <param name="oldValuesCache">The length of the removed values cache.</param>
        /// <param name="barsIndex">The index of the 'NijaScript.Bars' used to get the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public CacheService(IBarsService barsService, object input1, object input2 = null, int capacity = Cache.DEFAULT_CAPACITY, int oldValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(barsService, new CacheServiceOptions()
        {
            Capacity = capacity,
            OldValuesCapacity = oldValuesCache,
            BarsIndex = barsIndex
        })
        {
            _input1 = input1 ?? (input2 ?? barsService.Ninjascript);
            _input2 = input2;
        }

        #endregion

        #region Implementation

        public override string Name => $"{_cache}";
        internal override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            _cache = (TCache)GetCache(_input1,_input2, Options.OldValuesCapacity,Options.OldValuesCapacity, Options.BarsIndex);
            isDataLoaded = true;
        }
        public override void Update()
        {
            if (Bars.LastBarIsRemoved)
                _cache.RemoveLastElement();
            else if (Bars.IsClosed)
                _cache.Add();
            else if (Bars.IsPriceChanged)
                _cache.Update();
            else if (Bars.IsTick)
                _cache.Update();
        }
        public override string ToLogString() => $"{Name}[0]: {this[0]}";
        
        #endregion

        private IBarUpdateCache GetCache(object input,object input2, int capacity, int oldValuesCapacity, int barsIndex)
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
                    return new BarsCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(AvgCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new AvgCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is ISeries<double> series)
                    return new AvgCache(series, capacity, oldValuesCapacity,barsIndex);
                return null;
            }
            if (type == typeof(HighCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new HighCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is ISeries<double> series)
                    return new HighCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(IndexCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new IndexCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(MaxCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MaxCache(ninjascript, capacity,oldValuesCapacity,barsIndex);
                if (input is ISeries<double> series)
                    return new MaxCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(MinCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MinCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is ISeries<double> series)
                    return new MinCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(RangeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new RangeCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(SumCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new SumCache(ninjascript, capacity, oldValuesCapacity,barsIndex);
                if (input is ISeries<double> series)
                    return new SumCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(TicksCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TicksCache(ninjascript, capacity, oldValuesCapacity,barsIndex);
                return null;
            }
            if (type == typeof(TimeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TimeCache(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is TimeSeries series)
                    return new TimeCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(VolumeCache))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new VolumeCache(ninjascript, capacity,oldValuesCapacity, barsIndex);
                if (input is VolumeSeries series)
                    return new VolumeCache(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            return null;
        }
    }
}
