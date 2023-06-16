using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// The interface for any ninjascript builder.
    /// </summary>
    public interface IBuilder
    {

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        INinjascript Build(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IBuilder Configure<Script, Options>(Action<Options> options);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        IBuilder Configure<Script, Options>(Options options)
            where Options : IConfiguration;

        #endregion

    }
}
