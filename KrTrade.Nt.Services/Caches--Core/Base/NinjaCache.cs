using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class NinjaCache<TElement,TInput> : Cache<TElement>, INinjaCache<TElement,TInput>
    {
        object IBarUpdateCache.CurrentValue => CurrentValue;
        object IBarUpdateCache.this[int index] => this[index];
        object IBarUpdateCache.GetValue(int valuesAgo) => GetValue(valuesAgo);

        public int BarsIndex { get; internal set; }
        public int Period { get; internal set; }
        public TInput Input { get; protected set; }

        /// <summary>
        /// Create <see cref="NinjaCache{TElement,TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="INinjaCache{TElement,TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NinjaCache(TInput input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(capacity,oldValuesCapacity)
        {
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
            BarsIndex = barsIndex < 0 ? 0 : barsIndex;
            Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
        }

        protected abstract TInput GetInput(TInput input);
        public abstract bool Add();
        public abstract bool Update();
        public abstract string Name { get; }


    }
}
