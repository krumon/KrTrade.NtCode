using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Represents a cache with double values.
    /// </summary>
    public abstract class DoubleCacheService : CacheService<double>
    {

        /// <summary>
        /// Create <see cref="DoubleCacheService"/> new instance with a specific capacity.
        /// </summary>
        /// <param name="ninjascript">The ninjascript object.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <exception cref="ArgumentNullException">The bars service cannot be null.</exception>
        public DoubleCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        /// <summary>
        /// Gets the high value in cache.
        /// </summary>
        public double High => GetHigh(0,BarsBack());

        /// <summary>
        /// Gets the low value in cache.
        /// </summary>
        public double Low => GetLow(0, BarsBack());

        /// <summary>
        /// Gets the last value in cache.
        /// </summary>
        public double Last => GetLast(0);

        /// <summary>
        /// Gets the cache range.
        /// </summary>
        public double Range => GetRank(0,BarsBack());

        /// <summary>
        /// Gets the last price saved in cache, at the specified bars ago.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <returns>The last value.</returns>
        public double GetLast(int barsAgo)
        {
            IsValidIndex(barsAgo);

            if (CurrentBar < 0)
                return -1;

            return this[BarsBack() - 1 - barsAgo];
        }

        /// <summary>
        /// Gets the high price saved in cache, at the specified range in the cache.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="count">The number of elements in cache.</param>
        /// <returns>The high value.</returns>
        public double GetHigh(int barsAgo, int count)
        {
            IsValidIndex(barsAgo, barsAgo+count);

            if (CurrentBar < 0)
                return -1;

            double value = double.MinValue;

            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + count); i--)
            {
                value = Math.Max(value, this[i]);
            }
            return value;
        }

        /// <summary>
        /// Gets the low price saved in cache, at the specified range in the cache.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="count">The number of elements in cache.</param>
        /// <returns>The low value.</returns>
        public double GetLow(int barsAgo, int count)
        {
            IsValidIndex(barsAgo, barsAgo + count);

            if (CurrentBar < 0)
                return -1;

            double value = double.MaxValue;

            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + count); i--)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }

        /// <summary>
        /// Gets the range of elements saved in cache, at the specified range.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="count">The number of elements in cache.</param>
        /// <returns>The range value.</returns>
        public double GetRank(int barsAgo, int count)
        {
            if (CurrentBar < 0)
                return 0;

            return GetHigh(barsAgo,count) - GetLow(barsAgo,count);
        }

        private int CurrentBar => _ninjascript.CurrentBars[_ninjascript.BarsInProgress];
        private int BarsBack()
        {
            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBar < 0)
                return 0;

            return Math.Min(Count, _ninjascript.CurrentBars[_ninjascript.BarsInProgress] + 1);
        }
        private bool IsValidIndex(int idx)
        {
            if (idx < 0 || idx >= Count)
                throw new ArgumentOutOfRangeException(nameof(idx));

            return true;
        }
        private bool IsValidIndex(int startIdx, int finalIdx)
        {
            if (startIdx > finalIdx)
                throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx),nameof(finalIdx)));

            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
                return true;

            return false;
        }

    }
}
