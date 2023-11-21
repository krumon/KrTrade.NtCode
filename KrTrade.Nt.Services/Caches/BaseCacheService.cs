using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class BaseCacheService<T> : BaseService,
        IOnBarUpdateService,
        IOnBarClosedService,
        IOnPriceChangedService,
        IOnLastBarRemovedService
    {

        #region Private members

        protected BarsService _barsService;
        protected readonly Cache<T> Cache;

        #endregion

        #region Public properties

        protected internal T CandidateValue { get; protected set; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns><see cref="T"/> element.</returns>
        public T this[int index] => Cache[index];

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity => Cache.Capacity;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public int Count => Cache.Count;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public bool IsFull => Count == Capacity;

        /// <summary>
        /// Represents the next value displacement in NinjaScript Series.
        /// </summary>
        public int Displacement { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="Cache{T}"/> new instance with a specific capacity.
        /// </summary>
        /// <param name="ninjascript">The ninjascript object.</param>
        /// <param name="barsService">The bars service where cache service is executed.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <exception cref="ArgumentNullException">The bars service cannot be null.</exception>
        public BaseCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript)
        {
            Ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));

            if (capacity < 0) 
                throw new ArgumentOutOfRangeException("The cache capacity must be greater or equal than 0 and minor than 'MaximumBarsLookUp'(256)");

            if (Ninjascript.MaximumBarsLookBack == MaximumBarsLookBack.TwoHundredFiftySix && capacity > 255 ) 
                throw new ArgumentOutOfRangeException("The cache capacity must be greater or equal than 0 and minor than 'MaximumBarsLookUp'(256)");

            Cache = new Cache<T>(capacity);
            _barsService = barsService ?? throw new ArgumentNullException($"Error in 'LastPriceCacheService' constructor. The {nameof(barsService)} argument cannot be null."); ;

            if (barsService != null)
                _barsService.AddService(this);
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// gets the next candidate value to enter the cache. 
        /// </summary>
        /// <param name="valueDisplacement">The displacement in any NinjaScript serie thats we are going to find the candidate value.</param>
        /// <returns>The candidate value or no valid candidate value.</returns>
        public abstract T GetNextCandidateValue(int valueDisplacement);

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
        public abstract bool IsBetterCandidateValue();
        
        public void OnBarUpdate()
        {
            CandidateValue = GetNextCandidateValue(Displacement);
        }
        public void OnBarClosed()
        {
            if (IsValidCandidateValue())
                Cache.Add(CandidateValue);
        }
        public void OnLastBarRemoved()
        {
            if (Displacement != 0)
                return;

            Cache.Clear();
            for (int barsBack = Math.Min(Ninjascript.CurrentBars[_barsService.Idx], Capacity) - 1; barsBack >= 0; barsBack--)
                Cache.Add(GetNextCandidateValue(Displacement + barsBack));
        }
        public void OnPriceChanged()
        {
            if (IsBetterCandidateValue())
                Cache.Replace(CandidateValue);
        }

        #endregion

    }
}
