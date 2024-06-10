using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services
{

    /// <summary>
    /// Defines properties and methods for any ninjascript service collection.
    /// </summary>
    public interface INinjascriptServiceCollection<TElement,TInfo> : ICollection<TElement,TInfo>, IConfigure, IDataLoaded, ITerminated
        where TElement : INinjascriptService
        where TInfo : IServiceCollectionInfo
    {
        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        IServiceOptions Options { get; }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        new ServiceCollectionType Type { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Indicates that the logger service is enabled.
        /// </summary>
        bool IsLogEnable { get; }

        ///// <summary>
        ///// Gets series log string.
        ///// </summary>
        ///// <returns>The string thats represents the series in the logger.</returns>
        //string ToLogString();

        ///// <summary>
        ///// Gets the series collection logging string with sepecified parameters.
        ///// </summary>
        ///// <param name="header">The header of the string.</param>
        ///// <param name="tabOrder">The number of tabulation strings to insert in the log string.</param>
        ///// <param name="separator">The separator between the collection values.</param>
        ///// <returns>The string thats represents the series in the logger.</returns>
        //string ToLogString(string header, int tabOrder, string separator);

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
