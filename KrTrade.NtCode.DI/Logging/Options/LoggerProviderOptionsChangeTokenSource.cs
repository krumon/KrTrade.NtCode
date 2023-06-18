using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.Options
{
    /// <inheritdoc />
    public class LoggerProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public LoggerProviderOptionsChangeTokenSource(ILoggerProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
