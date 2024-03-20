using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface ISeries : ICache
    {

        /// <summary>
        /// Gets the series name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the series key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the series period.
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
