//using System;

//namespace KrTrade.Nt.Services.Caches
//{
//    /// <summary>
//    /// Represents a cache with double values.
//    /// </summary>
//    public abstract class DoubleCache : CacheService<double>
//    {

//        /// <summary>
//        /// Create new <see cref="DoubleCache"/> instance with specific capacity.
//        /// </summary>
//        /// <param name="capacity">The cache capacity.</param>
//        public DoubleCache(int capacity) : base(capacity)
//        {
//        }

//        /// <summary>
//        /// Gets the high value in cache.
//        /// </summary>
//        public double High => GetHigh(0,Count);

//        /// <summary>
//        /// Gets the low value in cache.
//        /// </summary>
//        public double Low => GetLow(0,Count);

//        /// <summary>
//        /// Gets the last value in cache.
//        /// </summary>
//        public double Last => GetLast(0);

//        /// <summary>
//        /// Gets the cache range.
//        /// </summary>
//        public double Range => GetRank(0,Count);

//        /// <summary>
//        /// Gets the last price saved in cache, at the specified bars ago.
//        /// </summary>
//        /// <param name="barsAgo">The specified bars ago.</param>
//        /// <returns>The last value.</returns>
//        public double GetLast(int barsAgo) => this[Count - 1 - barsAgo];

//        /// <summary>
//        /// Gets the high price saved in cache, at the specified range in the cache.
//        /// </summary>
//        /// <param name="barsAgo">The specified bars ago.</param>
//        /// <param name="count">The number of elements in cache.</param>
//        /// <returns>The high value.</returns>
//        public double GetHigh(int barsAgo, int count)
//        {
//            IsValidIndex(barsAgo, barsAgo+count);
//            double value = double.MinValue;

//            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + count); i--)
//            {
//                value = Math.Max(value, this[i]);
//            }
//            return value;
//        }

//        /// <summary>
//        /// Gets the low price saved in cache, at the specified range in the cache.
//        /// </summary>
//        /// <param name="barsAgo">The specified bars ago.</param>
//        /// <param name="count">The number of elements in cache.</param>
//        /// <returns>The low value.</returns>
//        public double GetLow(int barsAgo, int count)
//        {
//            IsValidIndex(barsAgo, barsAgo + count);
//            double value = double.MaxValue;

//            for (int i = Count - 1 - barsAgo; i >= Count - (barsAgo + count); i--)
//            {
//                value = Math.Min(value, this[i]);
//            }
//            return value;
//        }

//        /// <summary>
//        /// Gets the range of elements saved in cache, at the specified range.
//        /// </summary>
//        /// <param name="barsAgo">The specified bars ago.</param>
//        /// <param name="count">The number of elements in cache.</param>
//        /// <returns>The range value.</returns>
//        public double GetRank(int barsAgo, int count)
//        {
//            return GetHigh(barsAgo,count) - GetLow(barsAgo,count);
//        }

//        private bool IsValidIndex(int idx)
//        {
//            if (idx < 0 || idx >= Count)
//                throw new ArgumentOutOfRangeException(nameof(idx));

//            return true;
//        }
//        private bool IsValidIndex(int startIdx, int finalIdx)
//        {
//            if (startIdx > finalIdx)
//                throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx),nameof(finalIdx)));

//            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
//                return true;

//            return false;
//        }

//    }
//}
