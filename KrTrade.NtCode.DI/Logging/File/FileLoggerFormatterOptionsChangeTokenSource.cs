using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.File
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class FileLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions> : ConfigurationChangeTokenSource<TOptions>
        where TFormatter : BaseFileFormatter
        where TOptions : BaseFileFormatterOptions
    {
        public FileLoggerFormatterOptionsChangeTokenSource(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
