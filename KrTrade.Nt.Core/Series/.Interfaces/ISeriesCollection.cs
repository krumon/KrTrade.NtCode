﻿using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Series
{

    /// <summary>
    /// Defines methods that are necesary to construct a series collection.
    /// </summary>
    public interface ISeriesCollection<TElement> : ICollection<TElement, SeriesType, ISeriesInfo, ISeriesCollectionInfo, SeriesCollectionType>, IBarUpdate, IMarketData, IMarketDepth
        where TElement : ISeries
    {
        /// <summary>
        /// Dispose the series.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();


    }
}
