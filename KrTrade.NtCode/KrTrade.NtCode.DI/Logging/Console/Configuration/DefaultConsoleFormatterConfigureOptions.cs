using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.Console
{
    internal sealed class DefaultConsoleFormatterConfigureOptions : ConfigureOptions<SimpleConsoleFormatterOptions>
    {
        public DefaultConsoleFormatterConfigureOptions() : base(options => { options.ColorBehavior = LoggerColorBehavior.Default; })
        {
        }
    }
}
