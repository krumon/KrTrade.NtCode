using System;

namespace KrTrade.Nt.Services
{
    public abstract class DateTimeSeries<TInput> : ValueSeries<DateTime>, IDateTimeSeries<TInput>
    {

        /// <summary>
        /// Create <see cref="IDateTimeSeries{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IntSeries{TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected DateTimeSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        }

        public TInput Input { get; protected set; }
        public abstract TInput GetInput(object input);

        public DateTime Max(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            DateTime value = DateTime.MinValue;
            for (int i = displacement; i < displacement + period; i++)
                value = this[i] > value ? this[i] : value;

            return value;
        }
        public DateTime Min(int displacement = 0, int period = 1)
        {
            IsValidIndex(displacement, period);

            DateTime value = DateTime.MaxValue;

            for (int i = displacement; i < displacement + period; i++)
            {
                value = this[i] < value ? this[i] : value;
            }
            return value;
        }
        public TimeSpan Sum(int displacement = 0, int period = 1)
        {
            return Max(displacement, period) - Min(displacement, period);
        }
        public TimeSpan Interval(int displacement = 0, int period = 1)
        {
            return Count > 1 ? this[0] - this[1] : default;
        }

        protected sealed override bool IsValidValue(DateTime value) => value != default;
        public override string ToString() => $"{Name}[0]: {this[0].ToShortDateString()}";

    }
}
