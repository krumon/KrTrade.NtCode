using KrTrade.Nt.Scripts.Ninjascripts;
using System;

namespace KrTrade.Nt.Scripts.Services
{
    /// <summary>
    /// Represents properties and methods of ninjascript instance in ninjatrader plattform.
    /// </summary>
    public interface ITradingScript : IConfigurableScript, IDisposable
    {

        /// <summary>
        /// Checks if the given ninjascriptLevel is enabled.
        /// </summary>
        /// <param name="ninjascriptLevel">Level to be checked.</param>
        /// <returns>true if enabled.</returns>
        bool IsEnabled(NinjascriptLevel ninjascriptLevel);

        /// <summary>
        /// Calculate the ninjascript.
        /// </summary>
        void Calculate();
    }

    /// <summary>
    /// A generic interface for ninjascripts where the category name is derived from the specified
    /// TCategoryName type name. Generally used to enable activation of a named INinjascriptProvider
    /// from dependency injection.
    /// </summary>
    /// <typeparam name="TCategoryName">The type whose name is used for the ninjascript category name.</typeparam>
    public interface ITradingScript<out TCategoryName> : ITradingScript
    {
    }
}
