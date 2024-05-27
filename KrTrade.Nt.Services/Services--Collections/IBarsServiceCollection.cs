using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a data series service.
    /// </summary>
    public interface IBarsServiceCollection : INinjascriptServiceCollection<IBarsService>, IBarUpdate, IMarketData, IMarketDepth, IRender
    {

        // Data series information
        BarsServiceInfo[] InfoArray { get; }
        int BarsInProgress { get; }

        // Bars series
        CurrentBarSeries[] CurrentBars { get; }
        TimeSeries[] Times { get; }
        PriceSeries[] Opens { get; }
        PriceSeries[] Highs { get; }
        PriceSeries[] Lows { get; }
        PriceSeries[] Closes { get; }
        VolumeSeries[] Volumes { get; }
        TickSeries[] Ticks { get; }

        /// <summary>
        /// Gets the index series.
        /// </summary>
        CurrentBarSeries CurrentBar { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        TimeSeries Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        PriceSeries Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        PriceSeries High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        PriceSeries Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        PriceSeries Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        VolumeSeries Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        TickSeries Tick { get; }

        /// <summary>
        /// Indicates primary bars service is updated.
        /// </summary>
        bool IsUpdated {get;}

        /// <summary>
        /// Indicates primary bars service is closed.
        /// </summary>
        bool IsClosed {get;}

        /// <summary>
        /// Indicates primary bars service is removed.
        /// </summary>
        bool LastBarIsRemoved {get;}

        /// <summary>
        /// Indicates new tick success in primary bars service.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool IsTick {get;}

        /// <summary>
        /// Indicates first tick success in primary bars service.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool IsFirstTick {get;}

        /// <summary>
        /// Indicates new price success in primary bars service.
        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
        /// </summary>
        bool IsPriceChanged {get;}

        /// <summary>
        /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
        /// </summary>
        /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo, int barsIndex);

        /// <summary>
        /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo, int period, int barsIndex);

        /// <summary>
        /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
        /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        IList<Bar> GetBars(int barsAgo, int period, int barsIndex);


    }
}
