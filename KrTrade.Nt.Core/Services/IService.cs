using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Info;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService : IHasInfo, IHasOptions, IKeyItem
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Indicates that the logger service is enabled.
        /// </summary>
        bool IsLogEnable { get; }

    }

    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService<TInfo> : IService
        where TInfo : IInfo
    {
        /// <summary>
        /// Gets the information of the service.
        /// </summary>
        new TInfo Info { get; }

    }

    public interface IService<TInfo,TOptions> : IService<TInfo>
        where TInfo : IInfo
        where TOptions : ServiceOptions
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }

    }
}
