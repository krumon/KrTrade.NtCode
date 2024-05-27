using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Defines methods that are necesary to construct a series collection.
    /// </summary>
    public interface INinjascriptSeriesCollection<T> : IKeyCollection<T>, IConfigure, IDataLoaded, ITerminated, IBarUpdate, IMarketData, IMarketDepth
        where T : INinjascriptSeries
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Gets the information of the service.
        /// </summary>
        SeriesCollectionInfo Info { get; }

        /// <summary>
        /// Gets the type of the series.
        /// </summary>
        SeriesCollectionType Type { get; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Dispose the series.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        /// <summary>
        /// Gets series log string.
        /// </summary>
        /// <returns>The string thats represents the series in the logger.</returns>
        string ToLogString();

        /// <summary>
        /// Gets the series collection logging string with sepecified parameters.
        /// </summary>
        /// <param name="header">The header of the string.</param>
        /// <param name="tabOrder">The number of tabulation strings to insert in the log string.</param>
        /// <param name="separator">The separator between the collection values.</param>
        /// <param name="barsAgo">The bars ago of the elements to represent in the logger.</param>
        /// <returns>The string thats represents the series in the logger.</returns>
        string ToLogString(string header, int tabOrder, string separator, int barsAgo);

    }
}
