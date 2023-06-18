using KrTrade.NtCode.Configuration;

namespace KrTrade.NtCode.Logging.Configuration
{
    internal sealed class LoggerProviderConfiguration<T> : ILoggerProviderConfiguration<T>
    {
        public LoggerProviderConfiguration(ILoggerProviderConfigurationFactory providerConfigurationFactory)
        {
            Configuration = providerConfigurationFactory.GetConfiguration(typeof(T));
        }

        public IConfiguration Configuration { get; }
    }
}
