using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// Holds the information for a single log entry.
    /// </summary>
    public readonly struct LogEntry<T>
    {
        /// <summary>
        /// Initializes an instance of the LogEntry struct.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="nsName">The category name for the log.</param>
        /// <param name="dataSerieInfo">The ninjascript state.</param>
        /// <param name="message">The message for which log is being written.</param>
        /// <param name="data">The data to log</param>
        /// <param name="exception">The log exception.</param>
        public LogEntry(LogLevel logLevel, NinjaScriptState ninjascriptLogLevel, PriceState priceLogLevel, string nsName, string callerMemberName, string dataSerieInfo, string message, T data, Exception exception)
        {
            LogLevel = logLevel;
            Category = nsName;
            State = dataSerieInfo;
            Message = message;
            Data = data;
            Exception = exception;
        }
        IndicatorBase
        /// <summary>
        /// Gets the LogLevel
        /// </summary>
        public LogLevel LogLevel { get; }

        /// <summary>
        /// Gets the log category
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Gets the log state.
        /// </summary>
        public string State { get; }

        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the T object.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Gets the log exception
        /// </summary>
        public Exception Exception { get; }

    }
}
