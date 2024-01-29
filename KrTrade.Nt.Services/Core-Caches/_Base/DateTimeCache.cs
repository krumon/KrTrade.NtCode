using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class DateTimeCache<TInput> : ValueCache<DateTime,TInput>, IDateTimeCache<TInput>
    {
        /// <summary>
        /// Create <see cref="DateTimeCache{TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="DateTimeCache{TInput}"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="TInput"/> object used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected DateTimeCache(TInput input, int period, int displacement) : base(input,period, displacement)
        {
        }

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

    }
}
