using KrTrade.Nt.DI.DependencyInjection;

namespace KrTrade.Nt.DI.Logging.Internal
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
