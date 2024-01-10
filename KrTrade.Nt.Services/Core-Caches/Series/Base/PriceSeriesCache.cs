using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class PriceSeriesCache : BaseSeriesCache
    {

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="series"/> cannot be null.</exception>
        public PriceSeriesCache(ISeries<double> series) : base(series, DEFAULT_CAPACITY, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with infinite or default capacity and specified displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="series"/> cannot be null.</exception>
        public PriceSeriesCache(ISeries<double> series, int capacity) : base(series, capacity, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with specified capacity and specified displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="series"/> cannot be null.</exception>
        public PriceSeriesCache(ISeries<double> series, int capacity, int displacement) : base(series, capacity, displacement) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public PriceSeriesCache(NinjaScriptBase ninjascript) : base(ninjascript, DEFAULT_CAPACITY, 0, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public PriceSeriesCache(NinjaScriptBase ninjascript, int capacity) : base(ninjascript, capacity, 0, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public PriceSeriesCache(NinjaScriptBase ninjascript, int capacity, int displacement) : base(ninjascript, capacity, displacement, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="seriesIdx">The index of 'NinjaScript' parent bars.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="seriesIdx"/> cannot be out of range.</exception>
        public PriceSeriesCache(NinjaScriptBase ninjascript, int capacity, int displacement, int seriesIdx) : base(ninjascript, capacity, displacement, seriesIdx) { }

        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript = null) => Series[Displacement];
    }
}
