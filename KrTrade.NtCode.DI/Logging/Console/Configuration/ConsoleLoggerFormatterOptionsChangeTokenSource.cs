using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Console
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class ConsoleLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions> : ConfigurationChangeTokenSource<TOptions>
        where TOptions : ConsoleFormatterOptions
        where TFormatter : ConsoleFormatter
    {
        public ConsoleLoggerFormatterOptionsChangeTokenSource(ILoggerProviderConfiguration<ConsoleLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
