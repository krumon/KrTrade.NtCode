using Nt.Core.Configuration;

namespace Nt.Scripts.Ninjascripts.Configuration
{
    /// <summary>
    /// Allows access to configuration section associated with ninjascript provider
    /// </summary>
    /// <typeparam name="T">Type of ninjascript provider to get configuration for</typeparam>
    public interface INinjascriptProviderConfiguration<T>
    {
        /// <summary>
        /// Configuration section for requested ninjascript provider
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
