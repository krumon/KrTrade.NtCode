using KrTrade.Nt.Core.Caches;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public abstract class BaseSeriesCache : Cache<double>, ISeriesCache
    {
        
        public ISeries<double> Input { get; protected set; }
        protected int SeriesIdx { get; set; }

        #region Implementation

        public double Max => GetMax(Displacement, Count);
        public double Min => GetMin(Displacement, Count);
        public double Sum => GetSum(Displacement, Count);
        public double Avg => GetAvg(Displacement, Count);
        public double StdDev => GetStdDev(Displacement, Count);
        public double[] Quartils => GetQuartils(Displacement, Count);
        public double InterquartilRange => Quartils[2] - Quartils[0];
        public double Range => GetRange(Displacement, Count);

        public double GetMax(int initialIdx, int numberOfElements)
        {
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);

            double value = double.MinValue;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
                value = Math.Max(value, this[i]);

            return value;
        }
        public double GetMin(int initialIdx, int numberOfElements)
        {
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);

            double value = double.MaxValue;

            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }
        public double GetSum(int initialIdx, int numberOfElements)
        {
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);

            double sum = 0;

            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                sum += this[i];
            }
            return sum;
        }
        public double GetAvg(int initialIdx, int numberOfElements)
        {
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);

            return GetSum(initialIdx, numberOfElements) / Count;
        }
        public double GetStdDev(int initialIdx, int numberOfElements)
        {
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);

            double avg = GetAvg(initialIdx, numberOfElements) / Count;
            double sumx2 = 0;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
                sumx2 += Math.Pow(Math.Abs(this[i] - avg), 2.0);
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
            IsValidIndexs(initialIdx, initialIdx + numberOfElements);
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
        public double GetSwingHigh(int initialIdx, int strength)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndexs(initialIdx, initialIdx + numOfBars);

            bool isSwingHigh = true;
            double candidateValue = this[initialIdx + strength];
            for (int i = initialIdx + numOfBars -1; i > initialIdx + strength; i--)
                if (candidateValue.ApproxCompare(this[i]) <= 0.0)
                {
                    isSwingHigh = false;
                    break;
                }
            for (int i = initialIdx + strength - 1; i >= initialIdx; i--)
                if (candidateValue.ApproxCompare(this[i]) < 0.0)
                {
                    isSwingHigh = false;
                    break;
                }

            return isSwingHigh ? candidateValue : -1;
        }
        public double GetSwingLow(int initialIdx, int strength)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndexs(initialIdx, initialIdx + numOfBars);

            bool isSwingLow = true;
            double candidateValue = this[initialIdx + strength];
            for (int i = initialIdx + numOfBars - 1; i > initialIdx + strength; i--)
                if (candidateValue.ApproxCompare(this[i]) >= 0.0)
                {
                    isSwingLow = false;
                    break;
                }
            for (int i = initialIdx + strength - 1; i >= initialIdx; i--)
                if (candidateValue.ApproxCompare(this[i]) > 0.0)
                {
                    isSwingLow = false;
                    break;
                }

            return isSwingLow ? candidateValue : -1;
        }

        protected abstract ISeries<double> GetSeries(ISeries<double> input, int seriesIdx);
        protected abstract bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue);
        
        protected sealed override bool IsValidValue(double candidateValue) => !double.IsNaN(candidateValue) && candidateValue >= 0;
        protected sealed override double GetCandidateValue(MarketDataEventArgs marketDataEventArgs) => double.NaN;
        protected sealed override bool UpdateCurrentValue(ref double currentValue, MarketDataEventArgs marketDataEventArgs) { currentValue = default; return true; }
        public sealed override bool IsValidDataPoint(int barsAgo) => barsAgo >= 0 && barsAgo < Count ? IsValidValue(this[barsAgo]) : throw new ArgumentOutOfRangeException(nameof(barsAgo));
        public sealed override bool IsValidDataPointAt(int barIndex) => throw new NotImplementedException();

        protected override bool UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null)
        {
            bool isUpdated = false;
            double candidateValue = GetCandidateValue(ninjascript);
            if (IsValidCandidateValueToUpdate(currentValue, candidateValue))
            {
                currentValue = candidateValue;
                isUpdated = true;
            }
            return isUpdated;
        }

        #endregion

        #region Constructors

        ///// <summary>
        ///// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        ///// </summary>
        //protected BaseSeriesCache() : base(DEFAULT_PERIOD + 1, 0)
        //{
        //    Period = DEFAULT_PERIOD;
        //}

        ///// <summary>
        ///// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="displacement">The displacement of <see cref="ISeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        //protected BaseSeriesCache(int displacement) : base(DEFAULT_PERIOD, displacement)
        //{
        //    Period = DEFAULT_PERIOD;
        //}

        ///// <summary>
        ///// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        ///// <param name="displacement">The displacement of <see cref="ISeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        //protected BaseSeriesCache(int period, int displacement) : base(displacement == 0 ? period + 1 : displacement + period, displacement)
        //{
        //    Period = period < 0 ? DEFAULT_PERIOD : period;
        //}

        ///// <summary>
        ///// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="series">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity.</param>
        ///// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="series"/> cannot be null.</exception>
        //protected BaseSeriesCache(ISeries<double> series, int capacity, int displacement) : base(capacity, displacement)
        //{
        //    Series = series ?? throw new ArgumentNullException($"The Cache nedd an input serie. The {nameof(series)} is null.");
        //    SeriesIdx = 0;
        //}

        /// <summary>
        /// Create <see cref="BaseSeriesCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The NinjaScript <see cref="ISeries{double}"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without displacement. <see cref="Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="Core.Caches.ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="seriesIdx">The index of 'NinjaScript' parent bars.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="seriesIdx"/> cannot be out of range.</exception>
        protected BaseSeriesCache(ISeries<double> input, int period, int displacement, int seriesIdx) : base(period, displacement)
        {
            if (Input is NinjaScriptBase ninjascript)
                SeriesIdx = seriesIdx < 0 ? 0 : ninjascript.BarsArray.Length > seriesIdx ? seriesIdx : throw new ArgumentOutOfRangeException(nameof(seriesIdx));
            else 
                SeriesIdx = 0;

            Input = GetSeries(input, SeriesIdx);
        }

        #endregion

    }
}
