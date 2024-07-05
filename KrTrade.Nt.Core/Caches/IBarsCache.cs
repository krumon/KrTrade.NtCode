using KrTrade.Nt.Core.Bars;
using System;

namespace KrTrade.Nt.Core.Caches
{
    public interface IBarsCache : ICache<Bar>
    {
        /// <summary>
        /// Gets the index series.
        /// </summary>
        int Idx { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        double Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        double High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        double Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        double Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        double Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        long Ticks { get; }

        //void Add(IBarsService barsService, int barsAgo = 0);
        //void Add(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo);

        //void Update(IBarsService barsService = null, int barsAgo = 0);
        //void Update(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo = 0);

        //void Update(NinjaTrader.Data.MarketDataEventArgs args, int barsAgo = 0, IBarsService barsService = null);
        //void Update(NinjaScriptBase ninjascript, NinjaTrader.Data.MarketDataEventArgs args, int barsAgo = 0);

        /// <summary>
        /// Gets the bar of the specified index. 
        /// </summary>
        /// <param name="barsAgo">The specified index.</param>
        /// <returns></returns>
        Bar GetBar(int barsAgo);

        /// <summary>
        /// Gets a new bar form with specified number of bars. 
        /// </summary>
        /// <param name="barsAgo">The specified index.</param>
        /// <param name="period">The number of the bars will form the new bar.</param>
        /// <returns></returns>
        Bar GetBar(int barsAgo, int period);

    }
}
