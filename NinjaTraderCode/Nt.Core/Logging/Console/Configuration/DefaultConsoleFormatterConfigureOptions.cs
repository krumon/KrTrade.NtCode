using Nt.Core.Options;

namespace Nt.Core.Logging.Console
{
    internal sealed class DefaultConsoleFormatterConfigureOptions : ConfigureOptions<SimpleConsoleFormatterOptions>
    {
        public DefaultConsoleFormatterConfigureOptions() : base(options => { options.ColorBehavior = LoggerColorBehavior.Default; })
        {
        }
    }
}
