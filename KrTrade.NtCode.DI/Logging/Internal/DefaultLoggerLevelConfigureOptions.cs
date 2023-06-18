using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.Internal
{
    internal sealed class DefaultLoggerLevelConfigureOptions : ConfigureOptions<LoggerFilterOptions>
    {
        public DefaultLoggerLevelConfigureOptions(LogLevel level) : base(options => options.MinLevel = level)
        {
        }
    }
}
