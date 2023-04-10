using System.Collections.Generic;

namespace Nt.Core.Configuration
{
    /// <summary>
    /// Represents the root of an <see cref="IConfiguration"/> hierarchy.
    /// </summary>
    public interface IConfigurationRoot : IConfiguration
    {
        /// <summary>
        /// The <see cref="IConfigurationProvider"/> collection for this configuration.
        /// </summary>
        IEnumerable<IConfigurationProvider> Providers { get; }

        /// <summary>
        /// Force the configuration values to be reloaded from the underlying <see cref="IConfigurationProvider"/> collection.
        /// </summary>
        void Reload();
    }
}
