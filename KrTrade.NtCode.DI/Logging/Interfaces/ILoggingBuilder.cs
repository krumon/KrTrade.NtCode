using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Logging
{
    public interface ILoggingBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where logging services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
