using KrTrade.Nt.Core.Caches;
using NinjaTrader.Core.FloatingPoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public abstract class DoubleCache<TInput> : ValueCache<double,TInput>, INumericCache<double,TInput>
    {
        /// <summary>
        /// Create <see cref="DoubleCache{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="ISeriesCache{TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected DoubleCache(TInput input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, capacity,period, oldValuesCapacity, barsIndex)
        {
        }

        public double Max(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            double value = double.MinValue;
            for (int i = displacement; i < displacement + period; i++)
                value = Math.Max(value, this[i]);

            return value;
        }
        public double Min(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            double value = double.MaxValue;

            for (int i = displacement; i < displacement + period; i++)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }
        public double Sum(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            double sum = 0;

            for (int i = displacement; i < displacement + period; i++)
            {
                sum += this[i];
            }
            return sum;
        }
        public double Avg(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            return Sum(displacement, period) / Count;
        }
        public double StdDev(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            double avg = Avg(displacement, period) / Count;
            double sumx2 = 0;
            for (int i = displacement; i < displacement + period; i++)
                sumx2 += Math.Pow(Math.Abs(this[i] - avg), 2.0);
            return Math.Sqrt(sumx2 / Count); ;
        }
        public double Quartil(int numberOfQuartil, int displacement, int period)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return Quartils(displacement, period)[numberOfQuartil];
        }
        public double[] Quartils(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);
            double[] rangeCache = new double[period];
            int count = 0;
            for (int i = displacement; i < displacement + period; i++)
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
        public double Range(int displacement = 0, int period = 1)
        {
            return Max(displacement, period) - Min(displacement, period);
        }
        public double SwingHigh(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(displacement, numOfBars);

            bool isSwingHigh = true;
            double candidateValue = this[displacement + strength];
            for (int i = displacement + numOfBars - 1; i > displacement + strength; i--)
                if (candidateValue.ApproxCompare(this[i]) <= 0.0)
                {
                    isSwingHigh = false;
                    break;
                }
            for (int i = displacement + strength - 1; i >= displacement; i--)
                if (candidateValue.ApproxCompare(this[i]) < 0.0)
                {
                    isSwingHigh = false;
                    break;
                }

            return isSwingHigh ? candidateValue : -1;
        }
        public double SwingLow(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(displacement, numOfBars);

            bool isSwingLow = true;
            double candidateValue = this[displacement + strength];
            for (int i = displacement + numOfBars - 1; i > displacement + strength; i--)
                if (candidateValue.ApproxCompare(this[i]) >= 0.0)
                {
                    isSwingLow = false;
                    break;
                }
            for (int i = displacement + strength - 1; i >= displacement; i--)
                if (candidateValue.ApproxCompare(this[i]) > 0.0)
                {
                    isSwingLow = false;
                    break;
                }

            return isSwingLow ? candidateValue : -1;
        }
        public double InterquartilRange(int displacement = 0, int period = 1)
        {
            var quartils = Quartils(displacement,period);
            if (quartils == null || quartils.Length != 3)
                return default;

            return quartils[2] - quartils[0];
        }

        protected sealed override bool IsValidValue(double value) => value > 0 && !double.IsNaN(value) && !double.IsInfinity(value);
        public override string ToString() => $"{Name}[0]: {this[0]:#,0.00}";
    }
}
