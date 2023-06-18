namespace KrTrade.Nt.DI.Logging.File
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerOptions
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a specific log level to the file logger.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the formatter name.
        /// </summary>
        public string FormatterName { get; set; }

        /// <summary>
        /// The directory to store the file.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// The name and extension of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        /// <summary>
        /// Indicates if the directory doesn't exist or cannot create, store the log file in the current directory.
        /// </summary>
        public bool EnsureDirectoryExists { get; set; }

        #endregion
    }
}
