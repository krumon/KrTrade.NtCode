using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.Scripts.Logging
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class NinjatraderLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TFormatter : NinjatraderLoggerFormatter
        where TOptions : NinjatraderLoggerFormatterOptions
    {
        public NinjatraderLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<NinjatraderLoggerProvider> providerConfiguration) :
            base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
