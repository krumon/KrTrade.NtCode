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
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected DateTimeCache(TInput input, int capacity = DEFAULT_CAPACITY,int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(input, capacity, lengthOfRemovedCache, barsIndex)
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
        public override string ToString() => $"{Name}[0]: {this[0].ToShortDateString()}";
    }
}
