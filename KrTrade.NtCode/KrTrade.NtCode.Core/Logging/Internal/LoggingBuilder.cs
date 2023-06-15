using Nt.Core.DependencyInjection;

namespace Nt.Core.Logging.Internal
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
