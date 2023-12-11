using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class BaseCacheService<T,TOptions> :  BarUpdateService<TOptions>,
        IBarClosedService,
        IPriceChangedService,
        ILastBarRemovedService
        where TOptions : CacheOptions, new()
    {

        #region Public properties

        protected internal T CandidateValue { get; protected set; }
        protected internal Cache<T> Cache {  get; protected set; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns><see cref="T"/> element.</returns>
        public T this[int index] => Cache[index];

        /// <summary>
        /// Tha maximum cache capacity.
        /// </summary>
        public int MaxCapacity => CacheOptions.MaxCapacity;

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity { get => Options.Capacity; set { Options.Capacity = value; } }

        /// <summary>
        /// Represents the next value displacement in NinjaScript Series.
        /// </summary>
        public int Displacement { get => Options.Capacity; set { Options.Capacity = value; } }

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public int Count => Cache.Count;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public bool IsFull => Count == Capacity;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BaseCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BaseCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BaseCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BaseCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService)
        {
            Options.Displacement = displacement;
            Options.Capacity = capacity;
        }

        /// <summary>
        /// Create <see cref="BaseCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BaseCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BaseCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        #endregion

        #region Abstract methods

        internal override void Configure(out bool isConfigured)
        {
            Cache = new Cache<T>(Capacity);
            isConfigured = true;
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;
        }

        /// <summary>
        /// gets the next candidate value to enter the cache. 
        /// </summary>
        /// <param name="displacement">The displacement in any NinjaScript serie thats we are going to find the candidate value.</param>
        /// <returns>The candidate value or no valid candidate value.</returns>
        public abstract T GetNextCandidateValue(int displacement);

        /// <summary>
        /// Indicates if the candidate value is valid. For any class, the no valid value are 'null'.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValidCandidateValue();

        /// <summary>
        /// Indicates if the candidate value is better than the last value in the cache. 
        /// If the cache is updated because the price changed when the bar is running, 
        /// this method checked if the last value is necesary to be replaced.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsBestCandidateValue();
        
        public override void Update()
        {
            CandidateValue = GetNextCandidateValue(Displacement);
        }

        public void BarClosed()
        {
            if (IsValidCandidateValue())
                Cache.Add(CandidateValue);
        }

        public void PriceChanged()
        {
            if (IsBestCandidateValue())
                Cache.Replace(CandidateValue);
        }

        public void LastBarRemoved()
        {
            if (Displacement != 0)
                return;

            Cache.Clear();
            for (int barsBack = Math.Min(Ninjascript.CurrentBars[DataSeriesService.Idx], Capacity) - 1; barsBack >= 0; barsBack--)
                Cache.Add(GetNextCandidateValue(Displacement + barsBack));
        }

        public override void LogUpdatedState()
        {
            if (PrintService != null && Options.IsLogEnable)
                PrintService.LogValue(ToLogString());
        }

        protected virtual string ToLogString() => $"{Name}[Current]:{this[Count]}";

        #endregion

    }
}
