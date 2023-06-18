using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.Scripts.Logging
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
