//using KrTrade.Nt.Core;
//using KrTrade.Nt.Core.Bars;
//using KrTrade.Nt.Core.Services;
//using System;

//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Defines properties and methods that are necesary to create a data series service.
//    /// </summary>
//    public interface IBarsServiceCollection : IServiceCollection<IBarsService, IBarsServiceInfo,IBarsServiceOptions>, IBarUpdate, IMarketData, IMarketDepth, IRender
//    {

//        // Data series information
//        int BarsInProgress { get; }

//        // Bars series
//        int[] CurrentBars { get; }
//        DateTime[] Times { get; }
//        double[] Opens { get; }
//        double[] Highs { get; }
//        double[] Lows { get; }
//        double[] Closes { get; }
//        double[] Volumes { get; }
//        long[] Ticks { get; }

//        /// <summary>
//        /// Gets the index series.
//        /// </summary>
//        int CurrentBar { get; }

//        /// <summary>
//        /// Gets the time series.
//        /// </summary>
//        DateTime Time { get; }

//        /// <summary>
//        /// Gets the open series.
//        /// </summary>
//        double Open { get; }

//        /// <summary>
//        /// Gets the high series.
//        /// </summary>
//        double High { get; }

//        /// <summary>
//        /// Gets the low series.
//        /// </summary>
//        double Low { get; }

//        /// <summary>
//        /// Gets the close series.
//        /// </summary>
//        double Close { get; }

//        /// <summary>
//        /// Gets the volume series.
//        /// </summary>
//        double Volume { get; }

//        /// <summary>
//        /// Gets the tick count series.
//        /// </summary>
//        long Tick { get; }

//        /// <summary>
//        /// Indicates primary bars service is updated.
//        /// </summary>
//        bool IsUpdated {get;}

//        /// <summary>
//        /// Indicates primary bars service is closed.
//        /// </summary>
//        bool IsClosed {get;}

//        /// <summary>
//        /// Indicates primary bars service is removed.
//        /// </summary>
//        bool LastBarIsRemoved {get;}

//        /// <summary>
//        /// Indicates new tick success in primary bars service.
//        /// If calculate mode is 'BarClosed', this value is always false.
//        /// </summary>
//        bool IsTick {get;}

//        /// <summary>
//        /// Indicates first tick success in primary bars service.
//        /// If calculate mode is 'BarClosed', this value is always false.
//        /// </summary>
//        bool IsFirstTick {get;}

//        /// <summary>
//        /// Indicates new price success in primary bars service.
//        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
//        /// </summary>
//        bool IsPriceChanged {get;}

//        /// <summary>
//        /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
//        /// </summary>
//        /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
//        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
//        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        Bar GetBar(int barsAgo, int barsIndex);

//        /// <summary>
//        /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
//        /// </summary>
//        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
//        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
//        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        Bar GetBar(int barsAgo, int period, int barsIndex);

//        /// <summary>
//        /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
//        /// </summary>
//        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
//        /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
//        /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        Bar[] GetRange(int barsAgo, int period, int barsIndex);
//    }

    ///// <summary>
    ///// Defines properties and methods that are necesary to create a data series service.
    ///// </summary>
    //public interface IBarsServiceCollection : IServiceCollection<IBarsService, IBarsServiceInfo, IBarsServiceOptions>, IBarUpdate, IMarketData, IMarketDepth, IRender
    //{

    //    // Data series information
    //    int BarsInProgress { get; }

    //    // Bars series
    //    ICurrentBarSeries[] CurrentBars { get; }
    //    ITimeSeries[] Times { get; }
    //    IPriceSeries[] Opens { get; }
    //    IPriceSeries[] Highs { get; }
    //    IPriceSeries[] Lows { get; }
    //    IPriceSeries[] Closes { get; }
    //    IVolumeSeries[] Volumes { get; }
    //    ITickSeries[] Ticks { get; }

    //    /// <summary>
    //    /// Gets the index series.
    //    /// </summary>
    //    ICurrentBarSeries CurrentBar { get; }

    //    /// <summary>
    //    /// Gets the time series.
    //    /// </summary>
    //    ITimeSeries Time { get; }

    //    /// <summary>
    //    /// Gets the open series.
    //    /// </summary>
    //    IPriceSeries Open { get; }

    //    /// <summary>
    //    /// Gets the high series.
    //    /// </summary>
    //    IPriceSeries High { get; }

    //    /// <summary>
    //    /// Gets the low series.
    //    /// </summary>
    //    IPriceSeries Low { get; }

    //    /// <summary>
    //    /// Gets the close series.
    //    /// </summary>
    //    IPriceSeries Close { get; }

    //    /// <summary>
    //    /// Gets the volume series.
    //    /// </summary>
    //    IVolumeSeries Volume { get; }

    //    /// <summary>
    //    /// Gets the tick count series.
    //    /// </summary>
    //    ITickSeries Tick { get; }

    //    /// <summary>
    //    /// Indicates primary bars service is updated.
    //    /// </summary>
    //    bool IsUpdated { get; }

    //    /// <summary>
    //    /// Indicates primary bars service is closed.
    //    /// </summary>
    //    bool IsClosed { get; }

    //    /// <summary>
    //    /// Indicates primary bars service is removed.
    //    /// </summary>
    //    bool LastBarIsRemoved { get; }

    //    /// <summary>
    //    /// Indicates new tick success in primary bars service.
    //    /// If calculate mode is 'BarClosed', this value is always false.
    //    /// </summary>
    //    bool IsTick { get; }

    //    /// <summary>
    //    /// Indicates first tick success in primary bars service.
    //    /// If calculate mode is 'BarClosed', this value is always false.
    //    /// </summary>
    //    bool IsFirstTick { get; }

    //    /// <summary>
    //    /// Indicates new price success in primary bars service.
    //    /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
    //    /// </summary>
    //    bool IsPriceChanged { get; }

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
    //    /// </summary>
    //    /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
    //    /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
    //    /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    Bar GetBar(int barsAgo, int barsIndex);

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
    //    /// </summary>
    //    /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
    //    /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
    //    /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    Bar GetBar(int barsAgo, int period, int barsIndex);

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
    //    /// </summary>
    //    /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
    //    /// <param name="barsIndex">The index of <see cref="IBarsService"/> in the <see cref="IBarsServiceCollection"/></param>
    //    /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    IList<Bar> GetBars(int barsAgo, int period, int barsIndex);
    //}
//}
