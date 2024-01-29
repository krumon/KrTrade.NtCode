using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface ISeriesCache<TElement,TInput> : ICache<TElement>, IBarUpdateCache
    {

        /// <summary>
        /// The object necesary for get the cache values.</double>
        /// </summary>
        TInput Input { get; }

    }
}
