using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the service key.
        /// </summary>
        string Key { get; }

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
