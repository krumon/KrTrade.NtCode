using Nt.Core.Configuration;

namespace Nt.Core.Logging.Configuration
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
