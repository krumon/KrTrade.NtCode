using Nt.Core.Logging;
using Nt.Core.Logging.Abstractions;
using Nt.Core.Options;
using System;
using System.IO;

namespace Nt.Scripts.Logging
{
    public class OutputWindowFormatter: NinjatraderLoggerFormatter, IDisposable
    {
        private const string LoglevelPadding = ": ";
        private readonly IDisposable _optionsReloadToken;
        internal OutputWindowFormatterOptions FormatterOptions { get; set; }

        public OutputWindowFormatter(IOptionsMonitor<OutputWindowFormatterOptions> options) : base(NinjatraderLoggerFormatterNames.Output)
        {
            if (options== null) 
                FormatterOptions = new OutputWindowFormatterOptions();
            else
            {
                FormatterOptions = options.CurrentValue;
                ReloadNinjascriptFormatterOptions(options.CurrentValue);
                _optionsReloadToken = options.OnChange(ReloadNinjascriptFormatterOptions);
            }
        }

        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="textWriter">The string writer.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public override void Write<TState>(in LogEntry<TState> logEntry, TextWriter textWriter)
        {
            // Create the message and make sure the message is created.
            string[] messages = logEntry.Formatter(logEntry.State, logEntry.Exception).Split('|');
            if (logEntry.Exception == null && messages == null)
            {
                return;
            }
            string source = null;
            string message = null;
            if (messages != null)
            {
                source = messages.Length > 1 ? messages[0] : string.Empty;
                message = messages.Length > 1 ? messages[1] : messages[0];
            }

            // Write the datetime
            string timestamp = null;
            if (FormatterOptions.LogDateTime)
            {
                string timestampFormat = FormatterOptions.TimestampFormat;
                if (!string.IsNullOrEmpty(timestampFormat))
                {
                    DateTimeOffset dateTimeOffset = GetCurrentDateTime();
                    timestamp = dateTimeOffset.ToString(timestampFormat);
                }
            }
            if (!string.IsNullOrEmpty(timestamp))
            {
                textWriter.Write(timestamp);
                if (FormatterOptions.LogLogLevel)
                    textWriter.Write(' ');
            }

            // Write the log level
            LogLevel logLevel = logEntry.LogLevel;
            string logLevelString = GetLogLevelString(logLevel);
            if (logLevelString != null && FormatterOptions.LogLogLevel)
            {
                textWriter.Write('[');
                textWriter.Write(logLevelString);
                textWriter.Write(']');

            }

            if (textWriter.ToString().Length > 0)
                textWriter.Write(LoglevelPadding);

            CreateDefaultLogMessage(textWriter, logEntry, message, source);
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace: return "trce";
                case LogLevel.Debug: return "dbug";
                case LogLevel.Information: return "info";
                case LogLevel.Warning: return "warn";
                case LogLevel.Error: return "fail";
                case LogLevel.Critical: return "crit";
                default: throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
        private DateTimeOffset GetCurrentDateTime()
        {
            return FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
        }
        private void CreateDefaultLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, string source)
        {
            int eventId = logEntry.EventId.Id;
            Exception exception = logEntry.Exception;

            // Example:
            // info: ConsoleApp.Program[10]
            //       Request received

            // Example (single line):
            // 2022-09-20 15:35:15 info[Config]: Request received

            //textWriter.Write(LoglevelPadding);

            // category and event id
            if (string.IsNullOrEmpty(source))
                textWriter.Write(logEntry.Category);
            else
                textWriter.Write(source);

            textWriter.Write('[');
            textWriter.Write(eventId.ToString());
            textWriter.Write(']');
            //textWriter.Write(Environment.NewLine);

            WriteMessage(textWriter, message);

            // Example:
            // System.InvalidOperationException
            //    at Namespace.Class.Function() in File:line X
            if (exception != null)
            {
                // exception message
                WriteMessage(textWriter, exception.ToString());
            }
        }
        private void WriteMessage(TextWriter textWriter, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                textWriter.Write(' ');
                WriteReplacing(textWriter, Environment.NewLine, " ", message);
            }
        }
        private static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }
        private void ReloadNinjascriptFormatterOptions(OutputWindowFormatterOptions options)
        {
            FormatterOptions = options;
        }
        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

    }
}
