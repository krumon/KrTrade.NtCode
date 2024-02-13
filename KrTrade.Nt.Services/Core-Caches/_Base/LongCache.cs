using KrTrade.Nt.Core.Caches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public abstract class LongCache<TInput> : ValueCache<long,TInput>, INumericCache<long,TInput>
    {
        /// <summary>
        /// Create <see cref="IntCache{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IntCache{TInput}"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected LongCache(TInput input, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE,int barsIndex = 0) : base(input,capacity,lengthOfRemovedCache,barsIndex)
        {
        }

        public long Max(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            long value = long.MinValue;
            for (int i = displacement; i < displacement + period; i++)
                value = Math.Max(value, this[i]);

            return value;
        }
        public long Min(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            long value = long.MaxValue;

            for (int i = displacement; i < displacement + period; i++)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }
        public long Sum(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            long sum = 0;

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
        
        public double[] Quartils(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);
            long[] rangeCache = new long[period];
            int count = 0;
            for (int i = displacement; i < displacement + period; i++)
            {
                rangeCache[count] = this[i];
                count++;
            }
            IList<long> sortedCache = rangeCache.OrderBy(x => x).ToList();
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
        public double Quartil(int numberOfQuartil, int displacement, int period)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return Quartils(displacement, period)[numberOfQuartil];
        }
        public double InterquartilRange(int displacement = 0, int period = 1)
        {
            var quartils = Quartils(displacement,period);
            if (quartils == null || quartils.Length != 3)
                return default;

            return quartils[2] - quartils[0];
        }
        
        public long Range(int displacement = 0, int period = 1)
        {
            return Max(displacement, period) - Min(displacement, period);
        }
        public long SwingHigh(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(displacement, numOfBars);

            bool isSwingHigh = true;
            long candidateValue = this[displacement + strength];
            for (int i = displacement + numOfBars - 1; i > displacement + strength; i--)
                if (candidateValue <= this[i])
                {
                    isSwingHigh = false;
                    break;
                }
            for (int i = displacement + strength - 1; i >= displacement; i--)
                if (candidateValue < this[i])
                {
                    isSwingHigh = false;
                    break;
                }

            return isSwingHigh ? candidateValue : -1;
        }
        public long SwingLow(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(displacement, numOfBars);

            bool isSwingLow = true;
            long candidateValue = this[displacement + strength];
            for (int i = displacement + numOfBars - 1; i > displacement + strength; i--)
                if (candidateValue >= this[i])
                {
                    isSwingLow = false;
                    break;
                }
            for (int i = displacement + strength - 1; i >= displacement; i--)
                if (candidateValue > this[i])
                {
                    isSwingLow = false;
                    break;
                }

            return isSwingLow ? candidateValue : -1;
        }

        protected sealed override bool IsValidValue(long value) => value > 0;

        public override string ToString() => $"{Name}[0]: {this[0]:#,0.##}";
    }
}
