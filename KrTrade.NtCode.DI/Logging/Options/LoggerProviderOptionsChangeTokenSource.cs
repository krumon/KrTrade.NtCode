using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Options
{
    /// <inheritdoc />
    public class LoggerProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public LoggerProviderOptionsChangeTokenSource(ILoggerProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
