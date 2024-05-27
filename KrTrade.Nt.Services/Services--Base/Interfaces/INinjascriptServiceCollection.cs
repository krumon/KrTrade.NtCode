using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Services;
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
        IServiceOptions Options { get; }

        /// <summary>
        /// Gets the info of the service.
        /// </summary>
        IServiceInfo Info { get; }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        ServiceCollectionType Type { get; }

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
        /// Gets series log string.
        /// </summary>
        /// <returns>The string thats represents the series in the logger.</returns>
        string ToLogString();

        /// <summary>
        /// Gets the series collection logging string with sepecified parameters.
        /// </summary>
        /// <param name="header">The header of the string.</param>
        /// <param name="tabOrder">The number of tabulation strings to insert in the log string.</param>
        /// <param name="separator">The separator between the collection values.</param>
        /// <returns>The string thats represents the series in the logger.</returns>
        string ToLogString(string header, int tabOrder, string separator);

        /// <summary>
        /// Print in NinjaScript output winw the log string.
        /// </summary>
        void Log();

        /// <summary>
        /// Print in NinjaScript output window the log string.
        /// </summary>
        /// <param name="tabOrder">The number of tabulation strings to insert in the log string.</param>
        void Log(int tabOrder);

    }
}
