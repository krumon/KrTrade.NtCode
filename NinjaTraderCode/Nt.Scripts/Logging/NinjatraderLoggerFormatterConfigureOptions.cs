using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Options;

namespace Nt.Scripts.Logging
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
