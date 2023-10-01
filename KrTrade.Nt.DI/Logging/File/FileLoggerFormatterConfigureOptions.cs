using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.File
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class FileLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TFormatter : BaseFileFormatter
        where TOptions : BaseFileFormatterOptions
    {
        [RequiresUnreferencedCode(ConsoleLoggerFormatterExtensions.TrimmingRequiresUnreferencedCodeMessage)]
        public FileLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration) :
            base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
