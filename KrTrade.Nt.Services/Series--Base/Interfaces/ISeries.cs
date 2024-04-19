using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Collections;

namespace KrTrade.Nt.Services.Series
{
    public interface ISeries : ICache, IKeyItem
    {

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

        void Configure(IBarsService barsService);
        void DataLoaded(IBarsService barsService);
    }

    public interface ISeries<TElement> : ISeries, ICache<TElement>
    {
    }
}
