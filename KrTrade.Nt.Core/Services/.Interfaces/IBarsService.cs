using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a data series service.
    /// </summary>
    public interface IBarsService : IService<IBarsServiceInfo,IBarsServiceOptions>, IBarUpdate, IMarketData, IMarketDepth, IRender
    {
        bool IsWaitingFirstTick { get; }

        /// <summary>
        /// Gets the capacity of bars cache.
        /// </summary>
        int CacheCapacity { get; }

        /// <summary>
        /// Gets the capacity of removed bars cache.
        /// </summary>
        int RemovedCacheCapacity { get; }

        /// <summary>
        /// Indicates if the service contains the 'NinjaScript' promary data series information.
        /// </summary>
        bool IsPrimaryBars { get; }

        /// <summary>
        /// Gets or sets the trading hours name of the bars series.
        /// </summary>
        string TradingHoursName { get; }

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        string InstrumentName { get; }

        /// <summary>
        /// The bars period of the DataSeries.
        /// </summary>
        NinjaTrader.Data.BarsPeriod BarsPeriod { get; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Series element located at specified index.</returns>
        double this[int index] { get; }

        /// <summary>
        /// Gets the index series.
        /// </summary>
        ICurrentBarSeries CurrentBar { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        ITimeSeries Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        IPriceSeries Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        IPriceSeries High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        IPriceSeries Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        IPriceSeries Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        IVolumeSeries Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        ITickSeries Tick { get; }

        /// <summary>
        /// Gets the bars index in the 'NinjaScript.BarsArray'.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Indicates <see cref="IBarsService"/> is updated.
        /// </summary>
        bool IsUpdated {get;} 

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is closed.
        /// </summary>
        bool IsClosed {get;}

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is removed.
        /// </summary>
        bool LastBarIsRemoved {get;}

        /// <summary>
        /// Indicates new tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool IsTick {get;}

        /// <summary>
        /// Indicates first tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool IsFirstTick {get;}

        /// <summary>
        /// Indicates new price success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
        /// </summary>
        bool IsPriceChanged {get;}

        /// <summary>
        /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
        /// </summary>
        /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo);

        /// <summary>
        /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo, int period);

        /// <summary>
        /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        IList<Bar> GetBars(int barsAgo, int period);

        /// <summary>
        /// Print in NinjaScript output window the state of the service.
        /// </summary>
        void LogState();

    }
}
