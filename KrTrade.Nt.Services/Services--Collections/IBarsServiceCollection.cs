using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a data series service.
    /// </summary>
    public interface IBarsServiceCollection : IBarUpdate, IMarketData, IMarketDepth, IRender
    {
        /// <summary>
        /// Gets the bars service of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Bars service located at specified index.</returns>
        IBarsService this[int index] { get; }

        /// <summary>
        /// Gets the bars service with a speecific name.
        /// </summary>
        /// <param name="name">The specific name.</param>
        /// <returns>Bars service with the specified name.</returns>
        IBarsService this[string name] { get; }

        ///// <summary>
        ///// The index of the data series that is running in the NinjaScript.
        ///// </summary>
        //int BarsInProgress { get; }

        // CalculateMode
        // MultiSeriesCalculateMode
        // UpdatedSeriesIndex

        // Methods
        // Add(DataSeriesInfo info)

        // Data series information
        BarsServiceInfo[] Info { get; }

        // Bars series
        CurrentBarSeries[] CurrentBars { get; }
        TimeSeries[] Times { get; }
        PriceSeries[] Opens { get; }
        PriceSeries[] Highs { get; }
        PriceSeries[] Lows { get; }
        PriceSeries[] Closes { get; }
        VolumeSeries[] Volumes { get; }
        TickSeries[] Ticks { get; }

        //// Services
        //IndicatorCollection[] Indicators { get; }
        //StatsCollection[] Stats { get; }
        //FiltersCollection[] Filters { get; }

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

        ///// <summary>
        ///// Gets the bars index in the 'NinjaScript.BarsArray'.
        ///// </summary>
        //int Index { get; }

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

        ///// <summary>
        ///// Method to be executed in 'NinjaScript.OnBarUpdate()' method.
        ///// </summary>
        //void OnBarUpdate();

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

        ///// <summary>
        ///// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        ///// </summary>
        ///// <typeparam name="TService">The generic type of the service.</typeparam>
        ///// <typeparam name="TOptions">The generic type of the service options.</typeparam>
        ///// <param name="key">The key of the service.</param>
        ///// <param name="configureOptions">The options to configure the service.</param>
        ///// <param name="input1">Input serie to construct the service.</param>
        ///// <param name="input2">Input serie to construct the service.</param>
        //IBarsService AddService<TService, TOptions>(string key, Action<TOptions> configureOptions = null, object input1 = null, object input2 = null)
        //    where TService : IBarUpdateService
        //    where TOptions : BarUpdateServiceOptions, new();

        ///// <summary>
        ///// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        ///// </summary>
        ///// <typeparam name="TService">The generic type of the service.</typeparam>
        ///// <typeparam name="TOptions">The generic type of the service options.</typeparam>
        ///// <param name="key">The key of the service.</param>
        ///// <param name="options">The options to configure the service.</param>
        ///// <param name="input1">Input serie to construct the service.</param>
        ///// <param name="input2">Input serie to construct the service.</param>
        //IBarsService AddService<TService, TOptions>(string key, TOptions options, object input1 = null, object input2 = null)
        //    where TService : IBarUpdateService
        //    where TOptions : BarUpdateServiceOptions, new();

        ///// <summary>
        ///// Add <typeparamref name="TService"/> to the <see cref="IBarsService"./>
        ///// </summary>
        ///// <typeparam name="TService">The <typeparamref name="TService"/> to add.</typeparam>
        ///// <param name="service">The <typeparamref name="TService"/> instance to add.</param>
        ///// <param name="key">The key of the service.</param>
        ///// <returns>The <see cref="IBarsService"/> to continue chaining services.</returns>
        //IBarsService AddService<TService>(string key, TService service)
        //    where TService : IBarUpdateService;

        ///// <summary>
        ///// Gets <typeparamref name="TService"/> thats exist in <see cref="IBarsService"./>
        ///// </summary>
        ///// <typeparam name="TService">The <typeparamref name="TService"/> to add.</typeparam>
        ///// <param name="key">The optional name of the service.</param>
        ///// <returns>The <typeparamref name="TService"/> instance or null if doesn't exist.</returns>
        //TService Get<TService>(string key = "")
        //    where TService : class, IBarUpdateService;

        ///// <summary>
        ///// Gets <typeparamref name="TCache"/> cache thats exists in <see cref="IBarsService"/>.
        ///// </summary>
        ///// <typeparam name="TCache">The <typeparamref name="TCache"/> to add.</typeparam>
        ///// <param name="key">The key of the service.</param>
        ///// <returns>The <typeparamref name="TService"/> instance or null if doesn't exist.</returns>
        //CacheService<TCache> GetCache<TCache>(string key = "")
        //    where TCache : class, IBarUpdateCache;

        ///// <summary>
        ///// Gets service with specified <paramref name="name"/> thats exist in <see cref="IBarsService"./>
        ///// </summary>
        ///// <param name="name">The specified name of the service.</param>
        ///// <returns>The <see cref="IBarUpdateService"/> instance or null if doesn't exist.</returns>
        //IBarUpdateService Get(string name);

        ///// <summary>
        ///// Method to be executed when 'Ninjatrader.ChartBars' is updated.
        ///// </summary>
        //void Update();

        ///// <summary>
        ///// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        ///// This services needs <see cref="IBarsService"/> to be executed because they are executed in <see cref="IBarsService"/>
        ///// after the bars have been updated.
        ///// </summary>
        //void Add(IBarUpdateService service);


    }
}
