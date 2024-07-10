using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Logging
{
    /// <summary>
    /// Represents properties and methods of the logging services.
    /// </summary>
    public interface ILogger<TOptions,TFormatter> : IOptionsScript<ServiceType,TOptions>
        where TOptions : ILoggerOptions<TFormatter>, new()
        where TFormatter : IFormatter, new()
    {

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Represents the minimum log level. 0:Trace, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Critical, 6:None
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// The minimum ninjascript log level. 0:Historical, 1:Configuration, 2:Realtime
        /// Historical level logs in all states.
        /// Configuration level not logs in Historical state.
        /// Realtime logs only in Realtime state.
        /// </summary>
        NinjascriptLogLevel NinjascriptLogLevel { get; set; }

        /// <summary>
        /// Indicates the log level are enable. The <see cref="LogLevel"/> and <see cref="NinjascriptLogLevel"/>.
        /// </summary>
        bool IsLogLevelsEnable(LogLevel logLevel, BarsLogLevel barsLogLevel);

        /// <summary>
        /// Logs any value with a specific <see cref="BaseFormatter"/>.
        /// </summary>
        /// <param name="logLevel">The minimum log level to log it.</param>
        /// <param name="barsLogLevel"></param>
        /// <param name="value">The value to log.</param>
        /// <param name="exception">The exception to log.</param>
        void Log(LogLevel logLevel, BarsLogLevel barsLogLevel, object value, Exception exception);

        /// <summary>
        /// Logs any value with a specific <see cref="BaseFormatter"/>.
        /// </summary>
        /// <param name="logLevel">The minimum log level to log it.</param>
        /// <param name="value">The value to log.</param>
        /// <param name="exception">The exception to log.</param>
        void Log(LogLevel logLevel, object value, Exception exception);

        /// <summary>
        /// Logs <see cref="LogLevel.Trace"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogTrace(string message);

        /// <summary>
        /// Logs <see cref="LogLevel.Debug"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs a <see cref="LogLevel.Information"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs <see cref="LogLevel.Warning"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs <see cref="LogLevel.Error"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogError(string message);

        /// <summary>
        /// Logs <see cref="LogLevel.Critical"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogCritical(string message);

        /// <summary>
        /// Logs <see cref="LogLevel.Error"/> with <see cref="Exception"/> message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        void LogError(Exception exception);

        /// <summary>
        /// Logs <see cref="LogLevel.Error"/> message with specific <see cref="Exception"/> message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        void LogError(string message, Exception exception);

        /// <summary>
        /// Logs <see cref="LogLevel.Critical"/> message with specific <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        void LogCritical(string message, Exception exception);
        
        /// <summary>
        /// Logs <see cref="Exception"/> message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        void LogException(Exception exception);

        /// <summary>
        /// Logs <paramref name="logLevel"/> with <paramref name="exception"/> message.
        /// </summary>
        /// <param name="logLevel">The <see cref="LogLevel"/> to log.</param>
        /// <param name="exception">The <see cref="Exception"/> to log.</param>
        void LogException(LogLevel logLevel, Exception exception);

        /// <summary>
        /// Logs <paramref name="logLevel"/> with specific message.
        /// </summary>
        /// <param name="logLevel">The <see cref="LogLevel"/> to log.</param>
        /// <param name="message">The message to log.</param>
        void LogText(LogLevel logLevel, string message);

    }
}
