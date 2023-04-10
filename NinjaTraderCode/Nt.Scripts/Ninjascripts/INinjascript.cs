using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Represents properties and methods of ninjascript instance in ninjatrader plattform.
    /// </summary>
    public interface INinjascript : IConfigurable, IDisposable
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
    public interface INinjascript<out TCategoryName> : INinjascript
    {
    }
}
