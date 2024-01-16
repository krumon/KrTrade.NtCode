using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class CalculateSeriesCache : BaseSeriesCache
    {

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public CalculateSeriesCache() : base(null, DEFAULT_PERIOD, 0, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public CalculateSeriesCache(int period) : base(null, period, 0, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        public CalculateSeriesCache(int period, int displacement) : base(null, period, displacement, 0) { }

        /// <summary>
        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        /// </summary>
        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="seriesIdx">The index of 'NinjaScript' parent bars.</param>
        public CalculateSeriesCache(int period, int displacement, int seriesIdx) : base(null, period, displacement, seriesIdx) { }

        ///// <summary>
        ///// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        ///// </summary>
        ///// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        ///// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        ///// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        ///// <param name="seriesIdx">The index of 'NinjaScript' parent bars.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        ///// <exception cref="ArgumentOutOfRangeException">The <paramref name="seriesIdx"/> cannot be out of range.</exception>
        //public CalculateSeriesCache(NinjaScriptBase ninjascript, int period, int displacement, int seriesIdx) : base(period, displacement) 
        //{
        //    if (ninjascript == null) throw new ArgumentNullException(nameof(ninjascript));
        //    SeriesIdx = seriesIdx >= 0 && seriesIdx < ninjascript.BarsArray.Length ? seriesIdx : throw new ArgumentOutOfRangeException(nameof(seriesIdx));
        //}

        protected sealed override ISeries<double> GetSeries(ISeries<double> input, int seriesIdx) => null;
        protected sealed override bool UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null) => base.UpdateCurrentValue(ref currentValue, ninjascript);

        //protected sealed override void UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null) => base.UpdateCurrentValue(ref currentValue, ninjascript);

    }
}
