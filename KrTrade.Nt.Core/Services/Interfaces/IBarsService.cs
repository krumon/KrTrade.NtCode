using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a data series service.
    /// </summary>
    public interface IBarsService : IService<IBarsServiceInfo,IBarsServiceOptions>, IBarUpdate, IMarketData, IMarketDepth, IRender
    {
        /// <summary>
        /// Method to be executed in 'NinjaScript.OnBarUpdate()' method.
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Gets the capacity of bars cache.
        /// </summary>
        int CacheCapacity { get; }

        /// <summary>
        /// Gets the capacity of removed bars cache.
        /// </summary>
        int RemovedCacheCapacity { get; }

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
        Bar this[int index] { get; }

        /// <summary>
        /// Gets the bars cache of the service.
        /// </summary>
        BarsCache Bars {  get; }

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
        Bar[] GetRange(int barsAgo, int period);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogOpen(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the HIGH price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogHigh(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the LOW price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogLow(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the CLOSE price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogClose(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the INPUT price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogInput(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogVolume(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogOHLC(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="label">The label of the values.</param>
        /// <param name="barsAgo">The specific bar.</param>
        void LogOHLC(string label, int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogOHLCV(int barsAgo = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        void LogOHLCV(string label, int barsAgo = 0);

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="value">The value to log.</param>
        void Log(object value);

        /// <summary>
        /// Log a collection of values with label in NinjaScript output window.
        /// </summary>
        /// <param name="labels">The labels. If exists one more label than values, 
        /// this label will be used as main label. "label0 => label1: value0 label2: value1".
        /// if only passed one label, will be used as main label. "label0 => value0 value1 value 2".
        /// The labels must be passed as a set of words separated by special characters ("," ";" " " "_" "-" ) 
        /// If exists a labels format error or number of labels don't match with values the method print only the values. </param>
        /// <param name="values">The values to log.</param>
        void Log(string labels, params object[] values);

        /// <summary>
        /// Print in NinjaScript output window the state of the service.
        /// </summary>
        void LogState();

    }

    ///// <summary>
    ///// Defines properties and methods that are necesary to create a data series service.
    ///// </summary>
    //public interface IBarsService : IService<IBarsServiceInfo, IBarsServiceOptions>, IBarUpdate, IMarketData, IMarketDepth, IRender
    //{
    //    //bool IsWaitingFirstTick { get; }

    //    /// <summary>
    //    /// Gets the capacity of bars cache.
    //    /// </summary>
    //    int CacheCapacity { get; }

    //    /// <summary>
    //    /// Gets the capacity of removed bars cache.
    //    /// </summary>
    //    int RemovedCacheCapacity { get; }

    //    ///// <summary>
    //    ///// Indicates if the service contains the 'NinjaScript' promary data series information.
    //    ///// </summary>
    //    //bool IsPrimaryBars { get; }

    //    /// <summary>
    //    /// Gets or sets the trading hours name of the bars series.
    //    /// </summary>
    //    string TradingHoursName { get; }

    //    /// <summary>
    //    /// Gets the instument name.
    //    /// </summary>
    //    string InstrumentName { get; }

    //    /// <summary>
    //    /// The bars period of the DataSeries.
    //    /// </summary>
    //    NinjaTrader.Data.BarsPeriod BarsPeriod { get; }

    //    /// <summary>
    //    /// Gets the element of a sepecific index.
    //    /// </summary>
    //    /// <param name="index">The specific index.</param>
    //    /// <returns>Series element located at specified index.</returns>
    //    double this[int index] { get; }

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
    //    /// Gets the bars index in the 'NinjaScript.BarsArray'.
    //    /// </summary>
    //    int Index { get; }

    //    /// <summary>
    //    /// Indicates <see cref="IBarsService"/> is updated.
    //    /// </summary>
    //    bool IsUpdated { get; }

    //    /// <summary>
    //    /// Indicates the last bar of 'Ninjatrader.ChartBars' is closed.
    //    /// </summary>
    //    bool IsClosed { get; }

    //    /// <summary>
    //    /// Indicates the last bar of 'Ninjatrader.ChartBars' is removed.
    //    /// </summary>
    //    bool LastBarIsRemoved { get; }

    //    /// <summary>
    //    /// Indicates new tick success in 'Ninjatrader.ChartBars'.
    //    /// If calculate mode is 'BarClosed', this value is always false.
    //    /// </summary>
    //    bool IsTick { get; }

    //    /// <summary>
    //    /// Indicates first tick success in 'Ninjatrader.ChartBars'.
    //    /// If calculate mode is 'BarClosed', this value is always false.
    //    /// </summary>
    //    bool IsFirstTick { get; }

    //    /// <summary>
    //    /// Indicates new price success in 'Ninjatrader.ChartBars'.
    //    /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
    //    /// </summary>
    //    bool IsPriceChanged { get; }

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
    //    /// </summary>
    //    /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
    //    /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    Bar GetBar(int barsAgo);

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
    //    /// </summary>
    //    /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
    //    /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    Bar GetBar(int barsAgo, int period);

    //    /// <summary>
    //    /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
    //    /// </summary>
    //    /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
    //    /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
    //    IList<Bar> GetBars(int barsAgo, int period);

    //    /// <summary>
    //    /// Print in NinjaScript output window the state of the service.
    //    /// </summary>
    //    void LogState();

    //}
}
