//using KrTrade.Nt.Core.Data;
//using KrTrade.Nt.Core;
//using KrTrade.Nt.Core.Logging;
//using NinjaTrader.NinjaScript;

//namespace KrTrade.Nt.Services
//{

//    /// <summary>
//    /// Defines properties and methods for any ninjascript service.
//    /// </summary>
//    public interface INinjascriptService : IService, IConfigure, IDataLoaded, ITerminated
//    {

//        /// <summary>
//        /// Gets the calculate mode of the service.
//        /// </summary>
//        Calculate CalculateMode { get; }

//        /// <summary>
//        /// Gets the service calculation mode when another series is updated. 
//        /// </summary>
//        MultiSeriesCalculateMode MultiSeriesCalculateMode { get; }

//        /// <summary>
//        /// Gets the options of the service.
//        /// </summary>
//        new INinjascriptServiceOptions Options {  get; }

//        /// <summary>
//        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
//        /// </summary>
//        IPrintService PrintService { get; }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="header"></param>
//        /// <param name="description"></param>
//        /// <param name="separator"></param>
//        /// <param name="value"></param>
//        /// <param name="tabOrder"></param>
//        /// <returns></returns>
//        string ToString(string header, string description, string separator, object value, int tabOrder);

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="tabOrder"></param>
//        /// <param name="value"></param>
//        /// <param name="isHeaderVisible"></param>
//        /// <param name="isDescriptionVisible"></param>
//        /// <param name="isSeparatorVisible"></param>
//        /// <param name="isValueVisible"></param>
//        /// <returns></returns>
//        string ToString(int tabOrder, object value, bool isHeaderVisible = true, bool isDescriptionVisible = true, bool isSeparatorVisible = false, bool isValueVisible = true);

//        /// <summary>
//        /// Print in NinjaScript output window the instance string.
//        /// </summary>
//        void Log();

//        /// <summary>
//        /// Print in NinjaScript output window the log message.
//        /// </summary>
//        /// <param name="tabOrder">The number of tabulation of the string.</param>
//        void Log(int tabOrder);

//        /// <summary>
//        /// Print in NinjaScript output window the log message.
//        /// </summary>
//        /// <param name="message">The message to logs.</param>
//        /// <param name="tabOrder">The number of tabulation of the string.</param>
//        void Log(string message, int tabOrder = 0);

//        /// <summary>
//        /// Print in NinjaScript output window the log message.
//        /// </summary>
//        /// <param name="level">The message log level.</param>
//        /// <param name="message">The message to logs.</param>
//        /// <param name="tabOrder">The number of tabulation of the string.</param>
//        void Log(LogLevel level, string message, int tabOrder = 0);

//        /// <summary>
//        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
//        /// </summary>
//        void LogConfigurationState();

//        void Log(LogLevel level, string state);


//    }
//    /// <summary>
//    /// Defines properties and methods for any ninjascript service.
//    /// </summary>
//    public interface INinjascriptService<TInfo> : INinjascriptService, IService<TInfo>
//        where TInfo : INinjascriptServiceInfo
//    {

//    }
//    /// <summary>
//    /// Defines properties and methods for any ninjascript service.
//    /// </summary>
//    public interface INinjascriptService<TInfo,TOptions> : INinjascriptService<TInfo>, IService<TInfo,TOptions>
//        where TInfo : INinjascriptServiceInfo
//        where TOptions : INinjascriptServiceOptions, new()
//    {
//        /// <summary>
//        /// Gets the options of the service.
//        /// </summary>
//        new TOptions Options { get; }
//    }
//}
