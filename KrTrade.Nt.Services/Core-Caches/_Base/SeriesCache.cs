using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class SeriesCache<TElement,TInput> : Cache<TElement>, ISeriesCache<TElement,TInput>
    {
        public TInput Input { get; protected set; }

        /// <summary>
        /// Create <see cref="SeriesCache{TElement,TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="ISeriesCache{TElement,TInput}"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="TInput"/> object used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected SeriesCache(TInput input, int period, int displacement) : base(period, displacement)
        {
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        }

        protected abstract TInput GetInput(TInput input);
        public abstract bool Add();
        public abstract bool Update();

    }
}
