using Nt.Core.Logging;

namespace Nt.Scripts.Logging
{
    public class NinjatraderLoggerOptions
    {
        /// <summary>
        /// Gets or sets a specific log level to the file logger.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the formatter name.
        /// </summary>
        public string FormatterName { get; set; }

    }
}