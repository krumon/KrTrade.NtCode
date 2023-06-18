using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.File
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
