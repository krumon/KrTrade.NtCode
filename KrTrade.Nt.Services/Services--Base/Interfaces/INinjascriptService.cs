using KrTrade.Nt.Core.Data;
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
        new NinjascriptServiceOptions Options {  get; }

        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Indicates that the service is enabled for printing on NinjaScript output window.
        /// </summary>
        bool IsLogEnable { get; }

        /// <summary>
        /// Gets service log string.
        /// </summary>
        /// <returns></returns>
        string ToLogString();

        /// <summary>
        /// Print in NinjaScript output winw the log string.
        /// </summary>
        void Log();

        ///// <summary>
        ///// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        ///// </summary>
        //void LogConfigurationState();

    }
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService<TOptions> : INinjascriptService
        where TOptions : NinjascriptServiceOptions, new()
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }
    }
}
