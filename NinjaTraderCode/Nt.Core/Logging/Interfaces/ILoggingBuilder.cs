using Nt.Core.DependencyInjection;

namespace Nt.Core.Logging
{
    public interface ILoggingBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where logging services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
