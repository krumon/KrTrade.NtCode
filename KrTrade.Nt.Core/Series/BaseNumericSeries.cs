using NinjaTrader.Core.FloatingPoint;
using System.Collections.Generic;
using System.Linq;
using System;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Core.Series
{
    public abstract class BaseNumericSeries : BaseValueSeries<double>, INumericSeries
    {
        protected BaseNumericSeries(IBarsService bars, SeriesInfo info) : base(bars, info)
        {
        }

        public double Max(int displacement = 0, int period = 1)
        {
            double value = 0;
            if (IsValidIndex(displacement, period))
            {
                value = double.MinValue;
                for (int i = displacement; i < displacement + period; i++)
                    value = Math.Max(value, this[i]);
            }

            return value;
        }
        public double Min(int displacement = 0, int period = 1)
        {
            double value = 0;
            if (IsValidIndex(displacement, period))
            {
                value = double.MaxValue;
                for (int i = displacement; i < displacement + period; i++)
                    value = Math.Min(value, this[i]);
            }

            return value;
        }
        public double Sum(int displacement = 0, int period = 1)
        {
            double sum = 0;
            if (IsValidIndex(displacement, period))
                for (int i = displacement; i < displacement + period; i++)
                    sum += this[i];

            return sum;
        }
        public double Avg(int displacement = 0, int period = 1)  => IsValidIndex(displacement, period) ? Sum(displacement, period) / Count : default;
        public double StdDev(int displacement = 0, int period = 1)
        {
            double stdDev = 0;
            if (IsValidIndex(displacement, period))
            {
                double avg = Avg(displacement, period);
                double sumx2 = 0;
                for (int i = displacement; i < displacement + period; i++)
                    sumx2 += Math.Pow(this[i] - avg, 2);
                stdDev = Math.Sqrt(sumx2 / period);
            }
            return stdDev;
        }

        public double Quartil(int numberOfQuartil, int displacement, int period)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return Quartils(displacement, period)[numberOfQuartil];
        }
        public double[] Quartils(int displacement = 0, int period = 1)
        {
            double[] quartils = new double[] { 0.0, 0.0, 0.0, };
            if (IsValidIndex(displacement, period))
            {
                double[] rangeCache = new double[period];
                int count = 0;
                for (int i = displacement; i < displacement + period; i++)
                {
                    rangeCache[count] = this[i];
                    count++;
                }
                IList<double> sortedCache = rangeCache.OrderBy(x => x).ToList();
                //double[] quartils = new double[3];
                for (int i = 1; i <= 3; i++)
                {
                    double quartil = i * (rangeCache.Length + 1) / 4;
                    int idx = (int)quartil;
                    double dec = quartil % idx;
                    quartils[i] = sortedCache[i] + (sortedCache[i + 1] - sortedCache[i]) * dec;
                }
            }

            return quartils;
        }
        public double InterquartilRange(int displacement = 0, int period = 1)
        {
            var quartils = Quartils(displacement, period);
            if (quartils == null || quartils.Length != 3)
                return default;

            return quartils[2] - quartils[0];
        }
        
        public double Range(int displacement = 0, int period = 1) => Max(displacement, period) - Min(displacement, period);
        public double SwingHigh(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            bool isSwingHigh = false;
            double candidateValue = -1.0;
            if (IsValidIndex(displacement, numOfBars))
            {
                isSwingHigh = true;
                candidateValue = this[displacement + strength];
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
            }

            return isSwingHigh ? candidateValue : 0.0;
        }
        public double SwingLow(int displacement = 0, int strength = 4)
        {
            int numOfBars = (strength * 2) + 1;
            bool isSwingLow = false;
            double candidateValue = -1.0;

            if (IsValidIndex(displacement, numOfBars))
            {
                isSwingLow = true;
                candidateValue = this[displacement + strength];
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
            }

            return isSwingLow ? candidateValue : 0.0;
        }

        protected override bool IsValidValue(double value) => value > 0 && !double.IsNaN(value) && !double.IsInfinity(value);
        protected string ValueToString(int barsAgo) => IsValidIndex(barsAgo) ? $"{this[barsAgo]:#,0.00}" : "0.00";

    }
}
