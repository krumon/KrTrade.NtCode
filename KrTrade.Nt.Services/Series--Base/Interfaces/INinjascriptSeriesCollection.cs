using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Defines methods that are necesary to construct a series collection.
    /// </summary>
    public interface INinjascriptSeriesCollection<TElement,TInfo> : ICollection<TElement,TInfo>, IConfigure, IDataLoaded, ITerminated, IBarUpdate, IMarketData, IMarketDepth
        where TElement : INinjascriptSeries
        where TInfo : ISeriesCollectionInfo
    {
        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Gets the type of the series.
        /// </summary>
        new SeriesCollectionType Type { get; }

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
