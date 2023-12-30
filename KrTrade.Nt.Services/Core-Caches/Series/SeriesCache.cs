using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class SeriesCache : BaseSeriesCache
    {

        private readonly ISeries<double> _series;

        /// <summary>
        /// Create <see cref="SeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        protected SeriesCache(ISeries<double> series) : this(series, DEFAULT_CAPACITY, 0, false)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesCache"/> instance with infinite or default capacity and zero displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="infiniteCapacity">Indicates infinite <see cref="BaseSeriesCache"/> capacity.</param>
        protected SeriesCache(ISeries<double> series, bool infiniteCapacity) : this(series, DEFAULT_CAPACITY, 0, infiniteCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesCache"/> instance with infinite or default capacity and specified displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="infiniteCapacity">Indicates infinite <see cref="BaseSeriesCache"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="BaseSeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        protected SeriesCache(ISeries<double> series, bool infiniteCapacity, int displacement) : this(series, DEFAULT_CAPACITY, displacement, infiniteCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesCache"/> instance with specified capacity and zero displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="BaseSeriesCache"/> capacity.</param>
        protected SeriesCache(ISeries<double> series, int capacity) : this(series, capacity, 0, false)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesCache"/> instance with specified capacity and specified displacement.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="BaseSeriesCache"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="BaseSeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        protected SeriesCache(ISeries<double> series, int capacity, int displacement) : this(series, capacity, displacement, false)
        {
        }

        protected SeriesCache(ISeries<double> series, int capacity, int displacement, bool infiniteCapacity) : base(capacity, displacement, infiniteCapacity, 0)
        {
            _series = series ?? throw new ArgumentNullException($"The Cache nedd an input serie. The {nameof(series)} is null.");
        }

        protected override double GetCandidateValue(NinjaScriptBase ninjascript = null) => _series[Displacement];

    }
}
