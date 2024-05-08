﻿using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Series;
using NinjaTrader.NinjaScript;

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

    }
}