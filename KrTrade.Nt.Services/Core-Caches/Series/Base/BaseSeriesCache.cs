using KrTrade.Nt.Core.Caches;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public abstract class BaseSeriesCache : Cache<double>, ISeriesCache
    {
        protected ISeries<double> Series { get; set; }
        protected int SeriesIdx { get; set; }

        #region Implementation

        public double Max => GetMax(0, Count);
        public double Min => GetMin(0, Count);
        public double Sum => GetSum(0, Count);
        public double Avg => GetAvg(0, Count);
        public double StdDev => GetStdDev(0, Count);
        public double[] Quartils => GetQuartils(0, Count);
        public double InterquartilRange => Quartils[2] - Quartils[0];
        public double Range => GetRange(0, Count);

        public double GetMax(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            double value = double.MinValue;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
                value = Math.Max(value, this[i]);

            return value;
        }
        public double GetMin(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            double value = double.MaxValue;

            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }
        public double GetSum(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            double sum = 0;

            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                sum += this[i];
            }
            return sum;
        }
        public double GetAvg(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            return GetSum(initialIdx, numberOfElements) / Count;
        }
        public double GetStdDev(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            double avg = GetAvg(initialIdx, numberOfElements) / Count;
            double sumx2 = 0;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
                sumx2 += Math.Abs(this[i] - avg);
            return Math.Sqrt(sumx2 / Count); ;
        }
        public double GetQuartil(int numberOfQuartil, int initialIdx, int numberOfElements)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return GetQuartils(initialIdx, numberOfElements)[numberOfQuartil];
        }
        public double[] GetQuartils(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);
            double[] rangeCache = new double[numberOfElements];
            int count = 0;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                rangeCache[count] = this[i];
                count++;
            }
            IList<double> sortedCache = rangeCache.OrderBy(x => x).ToList();
            double[] quartils = new double[3];
            for (int i = 1; i <= 3; i++)
            {
                double quartil = i * (rangeCache.Length + 1) / 4;
                int idx = (int)quartil;
                double dec = quartil % idx;
                quartils[i] = sortedCache[i] + (sortedCache[i + 1] - sortedCache[i]) * dec;
            }
            return quartils;
        }
        public double GetRange(int initialIdx, int numberOfElements)
        {
            return GetMax(initialIdx, numberOfElements) - GetMin(initialIdx, numberOfElements);
        }

        protected abstract ISeries<double> GetNinjascriptSeries(NinjaScriptBase ninjascript, int seriesIdx);
        protected sealed override bool IsValidCandidateValueToAdd(double candidateValue) => candidateValue >= 0 && candidateValue <= double.MaxValue;
        protected sealed override void UpdateCurrentValue(ref double currentValue, double candidateValue) => currentValue = candidateValue;
        protected sealed override void UpdateCandidateValue(ref double candidateValue, NinjaScriptBase ninjascript = null, MarketDataEventArgs marketDataEventArgs = null) => candidateValue = GetCandidateValue();

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="infiniteCapacity">If 'true' indicates infinite <see cref="ICache{T}"/> capacity, otherwise indicates specified or default capacity.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="series"/> cannot be null.</exception>
        protected BaseSeriesCache(ISeries<double> series, int capacity, int displacement, bool infiniteCapacity) : base(capacity, displacement, infiniteCapacity)
        {
            Series = series ?? throw new ArgumentNullException($"The Cache nedd an input serie. The {nameof(series)} is null.");
            SeriesIdx = 0;
        }

        /// <summary>
        /// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript parent of <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="capacity">The <see cref="Core.Caches.ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="infiniteCapacity">If 'true' indicates infinite <see cref="Core.Caches.ICache{T}"/> capacity, otherwise indicates specified or default capacity.</param>
        /// <param name="seriesIdx">The index of 'NinjaScript' parent bars.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="seriesIdx"/> cannot be out of range.</exception>
        protected BaseSeriesCache(NinjaScriptBase ninjascript, int capacity, int displacement, bool infiniteCapacity, int seriesIdx) : base(capacity, displacement, infiniteCapacity)
        {
            if (ninjascript == null) throw new ArgumentNullException(nameof(ninjascript));
            SeriesIdx = seriesIdx >= 0 && seriesIdx < ninjascript.BarsArray.Length ? seriesIdx : throw new ArgumentOutOfRangeException(nameof(seriesIdx));
            Series = GetNinjascriptSeries(ninjascript, seriesIdx) ?? throw new NullReferenceException(nameof(Series));
        }

        #endregion

    }
}
