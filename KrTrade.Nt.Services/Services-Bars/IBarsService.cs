using KrTrade.Nt.Core.Bars;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarsService : INinjascriptService<BarsOptions>
    {

        /// <summary>
        /// Gets <see cref="IBarsSeriesCache"/>.
        /// </summary>
        IBarsCacheService Series { get; }

        /// <summary>
        /// Gets the index of the data series to which it belongs. 
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets <see cref="ICache{T}"/> period.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Gets the displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/>.
        /// </summary>
        int Displacement { get; }

        /// <summary>
        /// Indicates <see cref="IBarsService"/> is updated.
        /// </summary>
        bool IsUpdated {get;} 

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is closed.
        /// </summary>
        bool BarClosed {get;}

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is removed.
        /// </summary>
        bool LastBarRemoved {get;}

        /// <summary>
        /// Indicates new tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool Tick {get;}

        /// <summary>
        /// Indicates first tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool FirstTick {get;}

        /// <summary>
        /// Indicates new price success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
        /// </summary>
        bool PriceChanged {get;}

        /// <summary>
        /// Method to be executed in 'NinjaScript.OnBarUpdate()' method.
        /// </summary>
        void OnBarUpdate();

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
        /// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        /// </summary>
        /// <typeparam name="TService">The generic type of the service.</typeparam>
        /// <typeparam name="TOptions">The generic type of the service options.</typeparam>
        /// <param name="configureOptions">The options to configure the service.</param>
        /// <param name="name">The optional name of the service.</param>
        IBarsService AddService<TService, TOptions>(Action<TOptions> configureOptions, string name = "")
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new();

        /// <summary>
        /// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        /// </summary>
        /// <typeparam name="TService">The generic type of the service.</typeparam>
        /// <typeparam name="TOptions">The generic type of the service options.</typeparam>
        /// <param name="options">The options to configure the service.</param>
        /// <param name="name">The optional name of the service.</param>
        IBarsService AddService<TService, TOptions>(TOptions options, string name = "")
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new();

        /// <summary>
        /// Add <typeparamref name="TService"/> to the <see cref="IBarsService"./>
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> to add.</typeparam>
        /// <param name="service">The <typeparamref name="TService"/> instance to add.</param>
        /// <param name="name">The optional name of the service.</param>
        /// <returns>The <see cref="IBarsService"/> to continue chaining services.</returns>
        IBarsService AddService<TService>(TService service, string name = "")
            where TService : IBarUpdateService;

        /// <summary>
        /// Gets <typeparamref name="TService"/> thats exist in <see cref="IBarsService"./>
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> to add.</typeparam>
        /// <param name="name">The optional name of the service.</param>
        /// <returns>The <typeparamref name="TService"/> instance or null if doesn't exist.</returns>
        TService GetService<TService>(string name = "")
            where TService : class, IBarUpdateService;

        /// <summary>
        /// Gets service with specified <paramref name="name"/> thats exist in <see cref="IBarsService"./>
        /// </summary>
        /// <param name="name">The specified name of the service.</param>
        /// <returns>The <see cref="IBarUpdateService"/> instance or null if doesn't exist.</returns>
        IBarUpdateService GetService(string name);

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
