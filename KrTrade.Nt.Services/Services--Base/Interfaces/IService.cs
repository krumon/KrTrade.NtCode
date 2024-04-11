using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService : IHasKey
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        ServiceOptions Options { get; }

    }

    public interface IService<TOptions> : IService
        where TOptions : ServiceOptions
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }

    }
}
