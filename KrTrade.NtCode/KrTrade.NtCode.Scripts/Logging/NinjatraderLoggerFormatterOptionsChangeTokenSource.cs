using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class NinjatraderLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions> : ConfigurationChangeTokenSource<TOptions>
        where TFormatter : NinjatraderLoggerFormatter
        where TOptions : NinjatraderLoggerFormatterOptions
    {
        public NinjatraderLoggerFormatterOptionsChangeTokenSource(ILoggerProviderConfiguration<NinjatraderLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
