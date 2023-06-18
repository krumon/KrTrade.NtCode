using KrTrade.Nt.DI.DependencyInjection;

namespace KrTrade.Nt.DI.Logging
{
    public interface ILoggingBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where logging services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
