using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Console
{
    internal sealed class DefaultConsoleFormatterConfigureOptions : ConfigureOptions<SimpleConsoleFormatterOptions>
    {
        public DefaultConsoleFormatterConfigureOptions() : base(options => { options.ColorBehavior = LoggerColorBehavior.Default; })
        {
        }
    }
}
