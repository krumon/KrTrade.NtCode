using System;

namespace KrTrade.Nt.Services
{
    public abstract class DateTimeSeries<TInput> : ValueSeries<DateTime,TInput>, IDateTimeSeries<TInput>
    {

        //public override ISeries<DateTime> Input { get; protected set; }
        //public abstract NinjaTrader.NinjaScript.TimeSeries GetInput(object input);

        protected DateTimeSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        ///// <summary>
        ///// Create <see cref="DateTimeSeries{TInput}"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The object instance used to gets elements for <see cref="DateTimeSeries{TInput}"/>.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //protected DateTimeSeries(TInput input, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, capacity, oldValuesCapacity, barsIndex)
        //{
        //    Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        //}

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

        protected sealed override bool IsValidValue(DateTime value) => value != default;
        public override string ToString() => $"{Name}[0]: {this[0].ToShortDateString()}";
    }
}
