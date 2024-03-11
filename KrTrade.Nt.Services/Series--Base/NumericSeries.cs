using System;

namespace KrTrade.Nt.Services
{
    public abstract class NumericSeries<TElement> : ValueSeries<TElement>, INumericSeries<TElement>
        where TElement : struct
    {

        /// <summary>
        /// Create <see cref="IDoubleSeries{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IntSeries{TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NumericSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
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

    public abstract class NumericSeries<TElement,TInput> : NumericSeries<TElement>, INumericSeries<TElement>
        where TElement : struct
    {

        /// <summary>
        /// Create <see cref="IDoubleSeries{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IntSeries{TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NumericSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        }

        public TInput Input { get; protected set; }
        public abstract TInput GetInput(object input);

    }
}
