using System;
using System.Collections.Generic;

namespace KrTrade.Nt.DI.Logging.Console
{
    /// <summary>
    /// Represents properties for any ninjascript logger.
    /// </summary>
    public class ColorConsoleLoggerOptions
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; }
            = new Dictionary<LogLevel, ConsoleColor>() 
            {
                [LogLevel.Information] = ConsoleColor.Green
            }; 
    }
}
