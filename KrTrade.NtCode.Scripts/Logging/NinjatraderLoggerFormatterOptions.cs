namespace KrTrade.Nt.Scripts.Logging
{
    public class NinjatraderLoggerFormatterOptions
    {
        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool LogLogLevel { get; set; }

        /// <summary>
        /// Indicates if the datetime should be output as part of the log message.
        /// </summary>
        public bool LogDateTime { get; set; }

        /// <summary>
        /// Gets or sets indication whether or not UTC timezone should be used to for timestamps in logging messages. Defaults to <c>false</c>.
        /// </summary>
        public bool UseUtcTimestamp { get; set; }

        /// <summary>
        /// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
        /// </summary>
        public string TimestampFormat { get; set; }

    }
}