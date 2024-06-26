//using KrTrade.Nt.Core;
//using KrTrade.Nt.Core.Bars;
//using NinjaTrader.Data;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Defines methods that are necesary to be executed when the bar is updated.
//    /// </summary>
//    public interface IBarsManager : IService<BarsManagerInfo,BarsManagerOptions>
//    {

//        /// <summary>
//        /// Gets the list of <see cref="BarsServiceInfo"/>.
//        /// </summary>
//        new IList<BarsServiceInfo> Info { get; }

//        /// <summary>
//        /// Gets the bars service of a sepecific index.
//        /// </summary>
//        /// <param name="index">The specific index.</param>
//        /// <returns>Bars service located at specified index.</returns>
//        IBarsService this[int index] { get; }

//        /// <summary>
//        /// Gets the bars service with a speecific name.
//        /// </summary>
//        /// <param name="name">The specific name.</param>
//        /// <returns>Bars service with the specified name.</returns>
//        IBarsService this[string name] { get; }

//        /// <summary>
//        /// The index of the data series that is running in the NinjaScript.
//        /// </summary>
//        int BarsInProgress { get; }

//        /// <summary>
//        /// Gets the number of bars services hosted in it.
//        /// </summary>
//        int Count { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        ICurrentBarSeries[] CurrentBars { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        ITimeSeries[] Times { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        IPriceSeries[] Opens { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        IPriceSeries[] Highs { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        IPriceSeries[] Lows { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        IPriceSeries[] Closes { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        IVolumeSeries[] Volumes { get; }

//        /// <summary>
//        /// Gets all index series
//        /// </summary>
//        ITickSeries[] Ticks { get; }

//        /// <summary>
//        /// Gets the index series.
//        /// </summary>
//        ICurrentBarSeries CurrentBar { get; }

//        /// <summary>
//        /// Gets the time series.
//        /// </summary>
//        ITimeSeries Time { get; }

//        /// <summary>
//        /// Gets the open series.
//        /// </summary>
//        IPriceSeries Open { get; }

//        /// <summary>
//        /// Gets the high series.
//        /// </summary>
//        IPriceSeries High { get; }

//        /// <summary>
//        /// Gets the low series.
//        /// </summary>
//        IPriceSeries Low { get; }

//        /// <summary>
//        /// Gets the close series.
//        /// </summary>
//        IPriceSeries Close { get; }

//        /// <summary>
//        /// Gets the volume series.
//        /// </summary>
//        IVolumeSeries Volume { get; }

//        /// <summary>
//        /// Gets the tick count series.
//        /// </summary>
//        ITickSeries Tick { get; }

//        /// <summary>
//        /// Indicates <see cref="IBarsManager"/> is updated.
//        /// </summary>
//        bool IsUpdated {get;} 

//        /// <summary>
//        /// Indicates the last bar of 'Ninjatrader.ChartBars' is closed.
//        /// </summary>
//        bool IsClosed {get;}

//        /// <summary>
//        /// Indicates the last bar of 'Ninjatrader.ChartBars' is removed.
//        /// </summary>
//        bool LastBarIsRemoved {get;}

//        /// <summary>
//        /// Indicates new tick success in 'Ninjatrader.ChartBars'.
//        /// If calculate mode is 'BarClosed', this value is always false.
//        /// </summary>
//        bool IsTick {get;}

//        /// <summary>
//        /// Indicates first tick success in 'Ninjatrader.ChartBars'.
//        /// If calculate mode is 'BarClosed', this value is always false.
//        /// </summary>
//        bool IsFirstTick {get;}

//        /// <summary>
//        /// Indicates new price success in 'Ninjatrader.ChartBars'.
//        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
//        /// </summary>
//        bool IsPriceChanged {get;}

//        /// <summary>
//        /// Method to be executed in 'NinjaScript.OnBarUpdate()' method.
//        /// </summary>
//        void OnBarUpdate();

//        /// <summary>
//        /// Method to be executed in 'NinjaScript.OnMarketData()' method.
//        /// </summary>
//        void OnMarketData(MarketDataEventArgs args);

//        /// <summary>
//        /// Method to be executed in 'NinjaScript.OnMarketDepth()' method.
//        /// </summary>
//        void OnMarketDepth(MarketDepthEventArgs args);

//        /// <summary>
//        /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
//        /// </summary>
//        /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
//        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        Core.Bars.Bar GetBar(int barsAgo);

//        /// <summary>
//        /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
//        /// </summary>
//        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
//        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        Core.Bars.Bar GetBar(int barsAgo, int period);

//        /// <summary>
//        /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
//        /// </summary>
//        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
//        /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
//        IList<Core.Bars.Bar> GetBars(int barsAgo, int period);

//    }
//}
