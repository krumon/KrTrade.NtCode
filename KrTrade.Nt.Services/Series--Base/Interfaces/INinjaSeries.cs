using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface INinjaSeries : ICache
    {

        /// <summary>
        /// Gets the name of cache.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the period of cache.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Add new element to cache using cache input series.
        /// </summary>
        /// <returns><c>true</c> if the element has been added, otherwise <c>false</c>.</returns>
        bool Add();

        /// <summary>
        /// Replace the last element of cache using.
        /// </summary>
        /// <returns><c>true</c> if the last element has been replaced, otherwise <c>false</c>.</returns>
        bool Replace();

        ///// <summary>
        ///// The object necesary to get or calculate the cache values.</double>
        ///// </summary>
        //object Input { get; }

        ///// <summary>
        ///// Gets the Input series.
        ///// </summary>
        ///// <returns>The instance of the input series.</returns>
        //object GetInput(object input);

        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        int Count { get; }

    }

    public interface INinjaSeries<TElement, TInput> : INinjaSeries, ICache<TElement>
    {
        /// <summary>
        /// The <typeparamref name="TInput"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Gets the <see cref="Input"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <returns>The instance of the input series.</returns>
        TInput GetInput(object input);

    }
}
