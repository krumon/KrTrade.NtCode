using Nt.Core.Configuration;

namespace Nt.Scripts.Ninjascripts.Configuration
{
    internal sealed class NinjascriptConfiguration
    {
        public IConfiguration Configuration { get; }

        public NinjascriptConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
