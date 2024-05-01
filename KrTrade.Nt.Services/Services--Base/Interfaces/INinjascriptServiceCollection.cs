using KrTrade.Nt.Core.Collections;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{

    /// <summary>
    /// Defines properties and methods for any ninjascript service collection.
    /// </summary>
    public interface INinjascriptServiceCollection<T> : IKeyCollection<T>, IConfigure, IDataLoaded, ITerminated
        where T : INinjascriptService
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        NinjascriptServiceOptions Options { get; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Indicates that the logger service is enabled.
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

    }
}
