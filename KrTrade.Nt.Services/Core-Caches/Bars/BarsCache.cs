using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class BarsCache : Cache<Bar>, IBarsCache
    {

        #region Public properties

        /// <summary>
        /// Gets the maximum value in cache.
        /// </summary>
        public double High => GetMax(0, Count);

        /// <summary>
        /// Gets the minimum value in cache.
        /// </summary>
        public double Low => GetMin(0, Count);

        /// <summary>
        /// Gets the last value in cache.
        /// </summary>
        public Bar LastValue => GetValue(0);

        /// <summary>
        /// Gets the range of cache values.
        /// </summary>
        public double Range => GetRange(0, Count);

        /// <summary>
        /// Gets the sum of cache values.
        /// </summary>
        public double Sum => GetSum(0, Count);

        public double Avg => throw new NotImplementedException();

        public double StdDev => throw new NotImplementedException();

        public double[] Quartils => throw new NotImplementedException();

        public double InterquartilRange => throw new NotImplementedException();

        #endregion

        #region Constructors

        public BarsCache(int capacity) : base(capacity)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the value of the cache element at a specified index.
        /// </summary>
        /// <param name="idx">The specified idx. 0 is the most recent value.</param>
        /// <returns>The value of the cache element.</returns>
        public Bar GetValue(int idx)
        {
            IsValidIndex(idx);
            return this[idx];
        }

        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="barsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        public double GetMax(int initialIdx, int barsBack)
        {
            IsValidIndex(initialIdx, initialIdx + barsBack);

            double value = double.MinValue;
            for (int i = initialIdx; i < initialIdx + barsBack; i++)
                value = Math.Max(value, this[i].High);

            return value;
        }

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the minimum value. 0 is the most recent value in the cache.</param>
        /// <param name="barsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        public double GetMin(int initialIdx, int barsBack)
        {
            IsValidIndex(initialIdx, initialIdx + barsBack);

            double value = double.MaxValue;

            for (int i = initialIdx; i < initialIdx + barsBack; i++)
            {
                value = Math.Min(value, this[i].Low);
            }
            return value;
        }

        /// <summary>
        /// Returns the sum of close values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="barsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        public double GetSum(int initialIdx, int barsBack)
        {
            IsValidIndex(initialIdx, initialIdx + barsBack);

            double sum = 0;

            for (int i = initialIdx; i < initialIdx + barsBack; i++)
            {
                sum += this[i].Close;
            }
            return sum;
        }

        /// <summary>
        /// Returns the range value between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="barsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The range value (the difference between the maximum value and the minimum value).</returns>
        public double GetRange(int initialIdx, int barsBack)
        {
            return GetMax(initialIdx, barsBack) - GetMin(initialIdx, barsBack);
        }

        #endregion

        #region Private methods

        protected override Bar GetCandidateValue(NinjaScriptBase ninjascript)
        {
            return new Bar()
            {
                Idx = ninjascript.CurrentBars[0],
                Open = ninjascript.Opens[0][Displacement]
              
            };
        }

        #endregion
    }
}
