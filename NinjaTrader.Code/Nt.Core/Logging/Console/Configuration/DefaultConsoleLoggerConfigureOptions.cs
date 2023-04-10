using Nt.Core.Options;

namespace Nt.Core.Logging.Console
{
    internal sealed class DefaultConsoleLoggerConfigureOptions : ConfigureOptions<ConsoleLoggerOptions>
    {
        public DefaultConsoleLoggerConfigureOptions() : base(options => { })
        {
        }
    }
}
