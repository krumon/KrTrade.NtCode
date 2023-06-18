using KrTrade.Nt.DI.Configuration;
using System;

namespace KrTrade.Nt.Scripts.Ninjascripts.Configuration
{
    /// <summary>
    /// Allows access to configuration section associated with ninjascript provider
    /// </summary>
    public interface INinjascriptProviderConfigurationFactory
    {
        /// <summary>
        /// Return configuration section associated with ninjascript provider
        /// </summary>
        /// <param name="providerType">The ninjascript provider type</param>
        /// <returns>The <see cref="IConfiguration"/> for the given providerType.</returns>
        IConfiguration GetConfiguration(Type providerType);
    }
}
