using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Services
{
    public interface IBarsCacheService : IEnumerable<Bar>, IEnumerable, IBarUpdateService<BarUpdateServiceInfo,BarUpdateServiceOptions>, IBarUpdate, IMarketData
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

        /// <summary>
        /// Gets the <see cref="Bar"/> in the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Bar this[int index] { get; }

        //void Add(IBarsService barsService, int barsAgo);
        //void Add(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo);
        //void Add(IBarsService barsService, NinjaTrader.Data.MarketDataEventArgs args);
        //void Add(NinjaScriptBase ninjascript, int barsInProgress, NinjaTrader.Data.MarketDataEventArgs args);

        //void Update(IBarsService barsService, int barsAgo);
        //void Update(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo);
        //void Update(IBarsService barsService, NinjaTrader.Data.MarketDataEventArgs args);
        //void Update(NinjaScriptBase ninjascript, int barsInProgress, NinjaTrader.Data.MarketDataEventArgs args);

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

        /// <summary>
        /// Returns range of cache elements from specified initial index to specified count.
        /// </summary>
        /// <param name="startIndex">The start index of the elements to returns.</param>
        /// <param name="count">The number of elements to returns.</param>
        /// <returns> Array with the elements specified.</returns>
        Bar[] GetRange(int startIndex, int count);

    }
}
