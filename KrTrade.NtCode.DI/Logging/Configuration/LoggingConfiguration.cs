using KrTrade.NtCode.Configuration;

namespace KrTrade.NtCode.Logging.Configuration
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
