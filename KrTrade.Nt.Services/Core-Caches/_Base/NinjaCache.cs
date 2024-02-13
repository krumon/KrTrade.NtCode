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
        public TInput Input { get; protected set; }

        /// <summary>
        /// Create <see cref="NinjaCache{TElement,TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="INinjaCache{TElement,TInput}"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected NinjaCache(TInput input, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(capacity,lengthOfRemovedCache)
        {
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
            BarsIndex = barsIndex < 0 ? 0 : barsIndex;
        }

        protected abstract TInput GetInput(TInput input);
        public abstract bool Add();
        public abstract bool Update();
        public abstract string Name {  get; }


    }
}
