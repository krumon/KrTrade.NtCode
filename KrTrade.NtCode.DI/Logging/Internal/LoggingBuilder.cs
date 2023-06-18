using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Logging.Internal
{
    internal sealed class LoggingBuilder : ILoggingBuilder
    {
        public LoggingBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
