using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.File
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
