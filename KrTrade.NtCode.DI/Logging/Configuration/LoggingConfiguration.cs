using KrTrade.Nt.DI.Configuration;

namespace KrTrade.Nt.DI.Logging.Configuration
{
    internal sealed class LoggingConfiguration
    {
        public IConfiguration Configuration { get; }

        public LoggingConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
