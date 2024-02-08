using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public interface IBarsSeries 
    {

        /// <summary>
        /// Gets the index series.
        /// </summary>
        IndexCache Index { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        TimeCache Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        DoubleCache<NinjaScriptBase> Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        HighCache High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        DoubleCache<NinjaScriptBase> Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        DoubleCache<NinjaScriptBase> Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        VolumeCache Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        TicksCache Ticks { get; }

    }
}
