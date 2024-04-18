using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Elements;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService : IKeyCollectionItem
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        ServiceOptions Options { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// Indicates that the logger service is enabled.
        /// </summary>
        bool IsLogEnable { get; set; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the key of the service.
        /// </summary>
        string Key { get; }

    }

    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService<TInfo> : IService, IKeyCollectionItem<TInfo>
        where TInfo : IElementInfo
    {
        /// <summary>
        /// Gets the information of the service.
        /// </summary>
        new TInfo Info { get; }
    }

    public interface IService<TInfo,TOptions> : IService<TInfo>
        where TInfo : IElementInfo
        where TOptions : ServiceOptions
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }

    }
}
