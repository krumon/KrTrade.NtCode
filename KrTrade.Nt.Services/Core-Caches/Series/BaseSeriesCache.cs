using KrTrade.Nt.Core.Caches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public abstract class BaseSeriesCache : Cache<double>, ISeriesCache
    {
        protected int SeriesIdx { get;set; }

        #region Public properties

        public double Max => GetMax(0, Count);
        public double Min => GetMin(0, Count);
        public double Sum => GetSum(0, Count);
        public double Avg => GetAvg(0, Count);
        public double StdDev => GetStdDev(0, Count);
        public double[] Quartils => GetQuartils(0, Count);
        public double RangeInterQuartil => Quartils[2] - Quartils[0];
        public double Range => GetRange(0, Count);

        #endregion

        #region Constructors

        protected BaseSeriesCache(int capacity, int displacement, bool infiniteCapacity, int seriesIdx) : base(capacity, displacement, infiniteCapacity)
        {
            SeriesIdx = seriesIdx < 0 ? 0 : seriesIdx;
        }

        #endregion
         
        #region Public methods

        /// <summary>
        /// Returns the value of the cache element at a specified index.
        /// </summary>
        /// <param name="idx">The specified idx. 0 is the most recent value.</param>
        /// <returns>The value of the cache element.</returns>
        public double GetValue(int idx)
        {
            IsValidIndex(idx);
            return this[idx];
        }

        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        public double GetMax(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);

            double value = double.MinValue;
            for (int i = initialIdx; i < initialIdx + elementsBack; i++)
                value = Math.Max(value, this[i]);

            return value;
        }

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the minimum value. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        public double GetMin(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);

            double value = double.MaxValue;

            for (int i = initialIdx; i < initialIdx + elementsBack; i++)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }

        /// <summary>
        /// Returns the sum of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        public double GetSum(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);

            double sum = 0;

            for (int i = initialIdx; i < initialIdx + elementsBack; i++)
            {
                sum += this[i];
            }
            return sum;
        }

        /// <summary>
        /// Returns the average of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The average of cache elements between the specified start and the specified number of bars.</returns>
        public double GetAvg(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);

            return GetSum(initialIdx,elementsBack)/Count;
        }

        /// <summary>
        /// Returns the standard desviation of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The standard desviation of cache elements between the specified start and the specified number of bars.</returns>
        public double GetStdDev(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);

            double avg = GetAvg(initialIdx, elementsBack)/Count;
            double sumx2 = 0;
            for (int i = initialIdx; i < initialIdx + elementsBack; i++)
                sumx2 += Math.Abs(this[i]-avg);
            return Math.Sqrt(sumx2 / Count); ;
        }

        /// <summary>
        /// Returns the first, second or third quartil of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="numberOfQuartil">The number of quartil to gets.</param>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The first, second or third quartil of cache elements between the specified start and the specified number of bars.</returns>
        public double GetQuartil(int numberOfQuartil, int initialIdx, int elementsBack)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return GetQuartils(initialIdx,elementsBack)[numberOfQuartil];
        }

        /// <summary>
        /// Returns the quartils of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The quartils of cache elements between the specified start and the specified number of bars.</returns>
        public double[] GetQuartils(int initialIdx, int elementsBack)
        {
            IsValidIndex(initialIdx, initialIdx + elementsBack);
            double[] rangeCache = new double[elementsBack];
            int count = 0;
            for (int i = initialIdx; i < initialIdx + elementsBack; i++)
            {
                rangeCache[count] = this[i];
                count++;
            }
            IList<double> sortedCache = rangeCache.OrderBy(x => x).ToList();
            double[] quartils = new double[3];
            for (int i = 1; i <= 3; i++)
            {
                double quartil = i*(rangeCache.Length + 1) / 4;
                int idx = (int)quartil;
                double dec = quartil % idx;
                quartils[i] = sortedCache[i] + (sortedCache[i + 1] - sortedCache[i]) * dec;
            }
            return quartils;
        }

        /// <summary>
        /// Returns the range value between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="elementsBack">The number of elements to to calculate the minimum value.</param>
        /// <returns>The range value (the difference between the maximum value and the minimum value).</returns>
        public double GetRange(int initialIdx, int elementsBack)
        {
            return GetMax(initialIdx, elementsBack) - GetMin(initialIdx, elementsBack);
        }

        #endregion

        #region Private methods

        protected bool IsValidIndex(int idx)
        {
            if (idx < 0 || idx >= Count)
                throw new ArgumentOutOfRangeException(nameof(idx));

            return true;
        }
        protected bool IsValidIndex(int startIdx, int finalIdx)
        {
            if (startIdx > finalIdx)
                throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx), nameof(finalIdx)));

            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
                return true;

            return false;
        }

        #endregion

    }
}
