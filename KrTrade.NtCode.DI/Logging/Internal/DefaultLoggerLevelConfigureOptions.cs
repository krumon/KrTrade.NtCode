using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Internal
{
    internal sealed class DefaultLoggerLevelConfigureOptions : ConfigureOptions<LoggerFilterOptions>
    {
        public DefaultLoggerLevelConfigureOptions(LogLevel level) : base(options => options.MinLevel = level)
        {
        }
    }
}
