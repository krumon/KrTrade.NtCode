namespace KrTrade.NtCode.Logging.File
{
    public class BaseFileFormatterOptions
    {

        /// <summary>
        /// Includes scopes when <see langword="true" />.
        /// </summary>
        public bool IncludeScopes { get; set; }

        /// <summary>
        /// When <see langword="true" />, the entire message gets logged in a single line.
        /// </summary>
        public bool Singleline { get; set; }

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool LogLevel { get; set; }

        /// <summary>
        /// Indicates if the datetime should be output as part of the log message.
        /// </summary>
        public bool LogDateTime { get;set; }
    }
}
