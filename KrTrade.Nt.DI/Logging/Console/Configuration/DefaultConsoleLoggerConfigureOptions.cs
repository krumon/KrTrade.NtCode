using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Console
{
    internal sealed class DefaultConsoleLoggerConfigureOptions : ConfigureOptions<ConsoleLoggerOptions>
    {
        public DefaultConsoleLoggerConfigureOptions() : base(options => { })
        {
        }
    }
}
