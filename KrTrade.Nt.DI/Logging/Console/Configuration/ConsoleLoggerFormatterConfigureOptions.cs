using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Console
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class ConsoleLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TOptions : ConsoleFormatterOptions
        where TFormatter : ConsoleFormatter
    {
        [RequiresUnreferencedCode(ConsoleLoggerFormatterExtensions.TrimmingRequiresUnreferencedCodeMessage)]
        public ConsoleLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<ConsoleLoggerProvider> providerConfiguration) :
            base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
