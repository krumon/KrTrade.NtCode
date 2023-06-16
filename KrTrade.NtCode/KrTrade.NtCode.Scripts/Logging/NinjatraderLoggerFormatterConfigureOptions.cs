using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging
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
