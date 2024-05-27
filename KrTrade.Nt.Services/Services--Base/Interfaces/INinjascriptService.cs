using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{

    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService : IService, IConfigure, IDataLoaded, ITerminated
    {

        /// <summary>
        /// Gets the calculate mode of the service.
        /// </summary>
        Calculate CalculateMode { get; }

        /// <summary>
        /// Gets the service calculation mode when another series is updated. 
        /// </summary>
        MultiSeriesCalculateMode MultiSeriesCalculateMode { get; }

        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new INinjascriptServiceOptions Options {  get; }

        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        ///// <summary>
        ///// Gets service log string.
        ///// </summary>
        ///// <returns></returns>
        //string ToLogString();

        ///// <summary>
        ///// Gets service log string.
        ///// </summary>
        ///// <param name="tabOrder">The number of tabulation of the string.</param>
        ///// <returns>The string that represents the service in the logger.</returns>
        //string ToLogString(int tabOrder);

        ///// <summary>
        ///// Print in NinjaScript output winw the log string.
        ///// </summary>
        //void Log();

        /// <summary>
        /// Print in NinjaScript output winw the log string.
        /// </summary>
        /// <param name="tabOrder">The number of tabulation of the string.</param>
        void Log(int tabOrder = 0);

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        void LogConfigurationState();

    }
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService<TInfo> : INinjascriptService, IService<TInfo>
        where TInfo : INinjascriptServiceInfo
    {

    }
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService<TInfo,TOptions> : INinjascriptService<TInfo>, IService<TInfo,TOptions>
        where TInfo : INinjascriptServiceInfo
        where TOptions : INinjascriptServiceOptions, new()
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }
    }
}
