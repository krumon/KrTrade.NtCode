using KrTrade.Nt.Core.Series;
using NinjaTrader.Core.FloatingPoint;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseNumericSeries : BaseValueSeries<double>, INumericSeries
    {
        ///// <summary>
        ///// Create default instance with specified parameters.
        ///// </summary>
        ///// <param name="period">The specified period to calculate values in cache.</param>
        ///// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        //protected NumericSeries(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        //{
        //}

        protected BaseNumericSeries(IBarsService bars, BaseSeriesInfo info) : base(bars, info)
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
            if (IsValidIndex(displacement, period))
            {
                double avg = Avg(displacement, period);
                double sumx2 = 0;
                for (int i = displacement; i < displacement + period; i++)
                    sumx2 += Math.Pow(Math.Abs(this[i] - avg), 2.0);
                return Math.Sqrt(sumx2 / Count);
            }
            else
                return 0;
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
        public double InterquartilRange(int displacement = 0, int period = 1)
        {
            var quartils = Quartils(displacement, period);
            if (quartils == null || quartils.Length != 3)
                return default;

            return quartils[2] - quartils[0];
        }

        protected override bool IsValidValue(double value) => value > 0 && !double.IsNaN(value) && !double.IsInfinity(value);
        protected override string GetValueString(int barsAgo) => IsValidIndex(barsAgo) ? $"{this[barsAgo]:#,0.00}" : "0.00";

    }

    //public abstract class NumericSeries<T, TInput> : NumericSeries<T>, INumericSeries<T,TInput>
    //    where T : struct
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input">The input instance used to gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected NumericSeries(TInput input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        if (input == null) throw new ArgumentNullException("input");
    //        Input = input;
    //    }

    //    public TInput Input { get; protected set; }

    //}

    //public abstract class NumericSeries<T,TInput,TEntry> : NumericSeries<T>, INumericSeries<T,TInput,TEntry>
    //    where T : struct
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets elements of the series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
    //    protected NumericSeries(TEntry entry, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input = entry != null ? GetInput(entry) : throw new ArgumentNullException(nameof(entry));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input">The input instance used to gets elements of the series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected NumericSeries(TInput input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input = input != null ? input : throw new ArgumentNullException(nameof(input));
    //    }

    //    public TInput Input { get; protected set; }
    //    public abstract TInput GetInput(TEntry entry);

    //}

    //public abstract class NumericSeries<T, TInput1, TInput2, TEntry> : NumericSeries<T>, INumericSeries<T, TInput1, TInput2, TEntry>
    //    where T : struct
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
    //    protected NumericSeries(TEntry entry, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input1 = entry != null ? GetInput1(entry) : throw new ArgumentNullException(nameof(entry));
    //        Input2 = entry != null ? GetInput2(entry) : throw new ArgumentNullException(nameof(entry));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input1">The first object used to gets series elements.</param>
    //    /// <param name="input2">The second object used to gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
    //    protected NumericSeries(TInput1 input1,TInput2 input2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input1 = input1 != null ?input1 : throw new ArgumentNullException(nameof(input1));
    //        Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
    //    }

    //    public TInput1 Input1 { get; protected set; }
    //    public TInput2 Input2 { get; protected set; }
    //    public abstract TInput1 GetInput1(TEntry entry);
    //    public abstract TInput2 GetInput2(TEntry entry);

    //}

    //public abstract class NumericSeries<T, TInput1, TInput2, TEntry1, TEntry2> : NumericSeries<T>, INumericSeries<T, TInput1, TInput2, TEntry1, TEntry2>
    //    where T : struct
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry1">The first entry instance used to gets the first input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="entry1">The second entry instance used to gets the second input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected NumericSeries(TEntry1 entry1, TEntry2 entry2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input1 = entry1 != null ? GetInput1(entry1) : throw new ArgumentNullException(nameof(entry1));
    //        Input2 = entry2 != null ? GetInput2(entry2) : throw new ArgumentNullException(nameof(entry2));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input1">The first object used to gets series elements.</param>
    //    /// <param name="input2">The second object used to gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
    //    /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
    //    protected NumericSeries(TInput1 input1, TInput2 input2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
    //    {
    //        Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
    //        Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
    //    }

    //    public TInput1 Input1 { get; protected set; }
    //    public TInput2 Input2 { get; protected set; }
    //    public abstract TInput1 GetInput1(TEntry1 entry1);
    //    public abstract TInput2 GetInput2(TEntry2 entry2);

    //}

}
