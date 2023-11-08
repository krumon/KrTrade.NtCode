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
        public double Max => GetMax(0,Count-1);

        /// <summary>
        /// Gets the low value in cache.
        /// </summary>
        public double Min => GetMin(0, Count-1);

        /// <summary>
        /// Gets the last value in cache.
        /// </summary>
        public double LastValue => GetValue(0);

        /// <summary>
        /// Gets the cache range.
        /// </summary>
        public double Range => GetRange(0,Count - 1);

        /// <summary>
        /// Gets the sum of cache values.
        /// </summary>
        public double Sum => GetSum(0,Count - 1);

        /// <summary>
        /// Gets the last price saved in cache, at the specified bars ago.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <returns>The last value.</returns>
        public double GetValue(int barsAgo)
        {
            IsValidIndex(barsAgo);
            return this[Count - 1 - barsAgo];
        }

        /// <summary>
        /// Gets the high price saved in cache, at the specified range in the cache.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="barsBack">The number of elements in cache.</param>
        /// <returns>The high value.</returns>
        public double GetMax(int barsAgo, int barsBack)
        {
            IsValidIndex(barsAgo, barsAgo+barsBack);

            double value = double.MinValue;
            for (int i = Count - 1 - barsAgo; i >= Count - 1 - barsAgo - barsBack; i--)
                value = Math.Max(value, this[i]);

            return value;
        }

        /// <summary>
        /// Gets the low price saved in cache, at the specified range in the cache.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="barsBack">The number of elements in cache.</param>
        /// <returns>The low value.</returns>
        public double GetMin(int barsAgo, int barsBack)
        {
            IsValidIndex(barsAgo, barsAgo + barsBack);

            double value = double.MaxValue;

            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + barsBack); i--)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }

        /// <summary>
        /// Gets the sum of values has been saved in cache.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="barsBack">The number of elements in cache.</param>
        /// <returns>The sum of cache values.</returns>
        public double GetSum(int barsAgo, int barsBack)
        {
            IsValidIndex(barsAgo, barsAgo + barsBack);

            double sum = 0;

            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + barsBack); i--)
            {
                sum += this[i];
            }
            return sum;
        }

        /// <summary>
        /// Gets the range of elements saved in cache, at the specified range.
        /// </summary>
        /// <param name="barsAgo">The specified bars ago.</param>
        /// <param name="barsBack">The number of elements in cache.</param>
        /// <returns>The range value.</returns>
        public double GetRange(int barsAgo, int barsBack)
        {
            return GetMax(barsAgo,barsBack) - GetMin(barsAgo,barsBack);
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
