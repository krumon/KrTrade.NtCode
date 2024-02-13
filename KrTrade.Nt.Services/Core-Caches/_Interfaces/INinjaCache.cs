using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface INinjaCache<TElement,TInput> : ICache<TElement>, IBarUpdateCache
    {

        /// <summary>
        /// Gets the name of cache.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The object necesary for get the cache values.</double>
        /// </summary>
        TInput Input { get; }

    }
}
