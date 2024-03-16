using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface ISeries : ICache
    {

        /// <summary>
        /// Gets the name of series.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the key of series.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the period of series.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Add new element to the series.
        /// </summary>
        /// <returns><c>true</c> if the element has been added, otherwise <c>false</c>.</returns>
        bool Add();

        /// <summary>
        /// Replace the last element of the series.
        /// </summary>
        /// <returns><c>true</c> if the last element has been replaced, otherwise <c>false</c>.</returns>
        bool Update();

    }

    public interface ISeries<TElement> : ISeries, ICache<TElement>
    {
    }
}
