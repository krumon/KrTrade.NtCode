using KrTrade.NtCode.Primitives;
using System.Collections.Generic;

namespace KrTrade.NtCode.Configuration
{
    /// <summary>
    ///  Provides configuration key/values for an application.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Tries to get a configuration value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if a value for the specified key was found, otherwise false.</returns>
        bool TryGet(string key, out string value);

        /// <summary>
        /// Sets a configuration value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, string value);

        /// <summary>
        /// Returns a change token if this provider supports change tracking, null otherwise.
        /// </summary>
        /// <returns>The change token.</returns>
        IChangeToken GetReloadToken();

        /// <summary>
        /// Loads configuration values from the source represented by this <see cref="IConfigurationProvider"/>.
        /// </summary>
        void Load();

        /// <summary>
        /// Returns the immediate descendant configuration keys for a given parent path based
        /// on this IConfigurationProviders data and the set of keys returned by all the preceding IConfigurationProviders.
        /// </summary>
        /// <param name="earlierKeys">The child keys returned by the preceding providers for the same parent path.</param>
        /// <param name="parentPath">The parent path.</param>
        /// <returns>The child keys.</returns>
        IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath);
    }
}