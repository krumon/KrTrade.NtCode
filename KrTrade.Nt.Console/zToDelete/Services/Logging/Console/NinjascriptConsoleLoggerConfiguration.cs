using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
{
    /// <summary>
    /// Represents properties for any ninjascript logger.
    /// </summary>
    public class NinjascriptConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; }
            = new Dictionary<LogLevel, ConsoleColor>(); // { [LogLevel.Information] = ConsoleColor.Yellow };

    }
}
