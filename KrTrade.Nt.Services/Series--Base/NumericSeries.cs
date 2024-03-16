using System;

namespace KrTrade.Nt.Services
{
    public abstract class NumericSeries<TElement> : BaseValueSeries<TElement>, INumericSeries<TElement>
        where TElement : struct
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        protected NumericSeries(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public abstract double Avg(int displacement = 0, int period = 1);
        public abstract double InterquartilRange(int displacement = 0, int period = 1);
        public abstract TElement Max(int displacement = 0, int period = 1);
        public abstract TElement Min(int displacement = 0, int period = 1);
        public abstract double Quartil(int numberOfQuartil, int displacement, int period);
        public abstract double[] Quartils(int displacement = 0, int period = 1);
        public abstract TElement Range(int displacement = 0, int period = 1);
        public abstract double StdDev(int displacement = 0, int period = 1);
        public abstract TElement Sum(int displacement = 0, int period = 1);
        public abstract TElement SwingHigh(int displacement = 0, int strength = 4);
        public abstract TElement SwingLow(int displacement = 0, int strength = 4);

    }

    public abstract class NumericSeries<TElement, TInput> : NumericSeries<TElement>, INumericSeries<TElement,TInput>
        where TElement : struct
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NumericSeries(TInput input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            if (input == null) throw new ArgumentNullException("input");
            Input = input;
        }

        public TInput Input { get; protected set; }

    }

    public abstract class NumericSeries<TElement,TInput,TEntry> : NumericSeries<TElement>, INumericSeries<TElement,TInput,TEntry>
        where TElement : struct
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets elements of the series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        protected NumericSeries(TEntry entry, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input = entry != null ? GetInput(entry) : throw new ArgumentNullException(nameof(entry));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets elements of the series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NumericSeries(TInput input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input = input != null ? input : throw new ArgumentNullException(nameof(input));
        }

        public TInput Input { get; protected set; }
        public abstract TInput GetInput(TEntry entry);

    }

    public abstract class NumericSeries<TElement, TInput1, TInput2, TEntry> : NumericSeries<TElement>, INumericSeries<TElement, TInput1, TInput2, TEntry>
        where TElement : struct
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        protected NumericSeries(TEntry entry, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input1 = entry != null ? GetInput1(entry) : throw new ArgumentNullException(nameof(entry));
            Input2 = entry != null ? GetInput2(entry) : throw new ArgumentNullException(nameof(entry));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input1">The first object used to gets series elements.</param>
        /// <param name="input2">The second object used to gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
        protected NumericSeries(TInput1 input1,TInput2 input2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input1 = input1 != null ?input1 : throw new ArgumentNullException(nameof(input1));
            Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
        }

        public TInput1 Input1 { get; protected set; }
        public TInput2 Input2 { get; protected set; }
        public abstract TInput1 GetInput1(TEntry entry);
        public abstract TInput2 GetInput2(TEntry entry);

    }

    public abstract class NumericSeries<TElement, TInput1, TInput2, TEntry1, TEntry2> : NumericSeries<TElement>, INumericSeries<TElement, TInput1, TInput2, TEntry1, TEntry2>
        where TElement : struct
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry1">The first entry instance used to gets the first input series. The input series is necesary for gets series elements.</param>
        /// <param name="entry1">The second entry instance used to gets the second input series. The input series is necesary for gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NumericSeries(TEntry1 entry1, TEntry2 entry2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input1 = entry1 != null ? GetInput1(entry1) : throw new ArgumentNullException(nameof(entry1));
            Input2 = entry2 != null ? GetInput2(entry2) : throw new ArgumentNullException(nameof(entry2));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input1">The first object used to gets series elements.</param>
        /// <param name="input2">The second object used to gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
        protected NumericSeries(TInput1 input1, TInput2 input2, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
            Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
            Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
        }

        public TInput1 Input1 { get; protected set; }
        public TInput2 Input2 { get; protected set; }
        public abstract TInput1 GetInput1(TEntry1 entry1);
        public abstract TInput2 GetInput2(TEntry2 entry2);

    }
}
