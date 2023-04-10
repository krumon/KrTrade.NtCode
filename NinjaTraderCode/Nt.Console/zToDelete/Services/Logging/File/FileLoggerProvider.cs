using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace ConsoleApp
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider, IDisposable
    {
        #region Protected Members

        /// <summary>
        /// The path to log to
        /// </summary>
        protected string filePath;

        /// <summary>
        /// The configuration to use when creating loggers
        /// </summary>
        protected readonly FileLoggerConfiguration configuration;

        /// <summary>
        /// Keeps track of the loggers already created
        /// </summary>
        protected readonly ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">The path to log to</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
        {
            // Set the configuration
            this.configuration = configuration;

            // Set the path
            filePath = path;
        }

        #endregion

        #region ILoggerProvider Implementation

        /// <summary>
        /// Creates a file logger based on the category name
        /// </summary>
        /// <param name="categoryName">The category name of this logger</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            // Get or create the logger for this category
            return loggers.GetOrAdd(categoryName, name => new FileLogger(name, filePath, configuration));
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Clear the list of loggers
            loggers.Clear();
        }

        #endregion
    }
}
