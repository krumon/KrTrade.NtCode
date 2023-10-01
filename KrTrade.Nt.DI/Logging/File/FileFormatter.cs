using KrTrade.Nt.DI.Logging.Abstractions;
using KrTrade.Nt.DI.Options;
using System;
using System.IO;

namespace KrTrade.Nt.DI.Logging.File
{
    public class FileFormatter: BaseFileFormatter, IDisposable
    {
        private const string LoglevelPadding = ": ";
        private static string _messagePadding = new string(' ', GetLogLevelString(LogLevel.Information).Length + LoglevelPadding.Length);
        private static string _newLineWithMessagePadding = Environment.NewLine + _messagePadding;
        private readonly IDisposable _optionsReloadToken;
        internal FileFormatterOptions FormatterOptions { get; set; }

        public FileFormatter(IOptionsMonitor<FileFormatterOptions> options) : base(FileFormatterNames.Normal)
        {
            if (options== null) 
                FormatterOptions = new FileFormatterOptions();
            else
            {
                FormatterOptions = options.CurrentValue;
                ReloadFileFormatterOptions(options.CurrentValue);
                _optionsReloadToken = options.OnChange(ReloadFileFormatterOptions);
            }
        }

        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <remarks>
        /// if the formatter wants to write colors to the console, it can do so by embedding ANSI color codes into the string
        /// </remarks>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="scopeProvider">The provider of scope data.</param>
        /// <param name="textWriter">The string writer embedding ansi code for colors.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
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
                string timestampFormat = FormatterOptions.TimestampOptions.TimestampFormat;
                if (!string.IsNullOrEmpty(timestampFormat))
                {
                    DateTimeOffset dateTimeOffset = GetCurrentDateTime();
                    timestamp = dateTimeOffset.ToString(timestampFormat);
                }
            }
            if (!string.IsNullOrEmpty(timestamp))
            {
                textWriter.Write('[');
                textWriter.Write(timestamp);
                textWriter.Write(']');
                //string timePadding = new string(' ', timestamp.Length + 2);
                //_messagePadding += timePadding;
                //_newLineWithMessagePadding = Environment.NewLine + _messagePadding;
            }

            // Write the log level
            LogLevel logLevel = logEntry.LogLevel;
            string logLevelString = GetLogLevelString(logLevel);
            if (logLevelString != null && FormatterOptions.LogLevel)
            {
                textWriter.Write(logLevelString);
            }
            CreateDefaultLogMessage(textWriter, logEntry, message, source, scopeProvider);
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
            return FormatterOptions.TimestampOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
        }
        private void CreateDefaultLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, string source, IExternalScopeProvider scopeProvider)
        {
            bool singleLine = FormatterOptions.Singleline;
            int eventId = logEntry.EventId.Id;
            Exception exception = logEntry.Exception;

            // Example:
            // info: ConsoleApp.Program[10]
            //       Request received

            // Example (single line):
            // [2022-09-20 15:35:15]info: Request received

            // category and event id
            textWriter.Write(LoglevelPadding);

            if (!singleLine)
            {
                if (string.IsNullOrEmpty(source))
                    textWriter.Write(logEntry.Category);
                else
                    textWriter.Write(source);

                textWriter.Write('[');
                textWriter.Write(eventId.ToString());
                textWriter.Write(']');
                textWriter.Write(Environment.NewLine);
            }
            else
            {
                if (string.IsNullOrEmpty(source))
                    textWriter.Write(logEntry.Category);
                else
                    textWriter.Write(source);

                textWriter.Write('[');
                textWriter.Write(eventId.ToString());
                textWriter.Write(']');
            }

            // scope information
            WriteScopeInformation(textWriter, scopeProvider, singleLine);
            WriteMessage(textWriter, message, singleLine);

            // Example:
            // System.InvalidOperationException
            //    at Namespace.Class.Function() in File:line X
            if (exception != null)
            {
                // exception message
                WriteMessage(textWriter, exception.ToString(), singleLine);
            }
            if (singleLine)
            {
                textWriter.Write(Environment.NewLine);
            }
        }
        private void WriteMessage(TextWriter textWriter, string message, bool singleLine)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (singleLine)
                {
                    textWriter.Write(' ');
                    WriteReplacing(textWriter, Environment.NewLine, " ", message);
                }
                else
                {
                    textWriter.Write(_messagePadding);
                    WriteReplacing(textWriter, Environment.NewLine, _newLineWithMessagePadding, message);
                    textWriter.Write(Environment.NewLine);
                }
            }
        }
        private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider scopeProvider, bool singleLine)
        {
            if (FormatterOptions.IncludeScopes && scopeProvider != null)
            {
                bool paddingNeeded = !singleLine;
                scopeProvider.ForEachScope((scope, state) =>
                {
                    if (paddingNeeded)
                    {
                        paddingNeeded = false;
                        state.Write(_messagePadding);
                        state.Write("=> ");
                    }
                    else
                    {
                        state.Write(" => ");
                    }
                    state.Write(scope);
                }, textWriter);

                if (!paddingNeeded && !singleLine)
                {
                    textWriter.Write(Environment.NewLine);
                }
            }
        }
        private static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }
        private void ReloadFileFormatterOptions(FileFormatterOptions options)
        {
            FormatterOptions = options;
            string timestamp = string.Empty;
            if (FormatterOptions.LogDateTime)
            {
                string timestampFormat = FormatterOptions.TimestampOptions.TimestampFormat;
                if (!string.IsNullOrEmpty(timestampFormat))
                {
                    DateTimeOffset dateTimeOffset = GetCurrentDateTime();
                    timestamp = dateTimeOffset.ToString(timestampFormat);
                }
            }
            if (!string.IsNullOrEmpty(timestamp))
            {
                string timePadding = new string(' ', timestamp.Length + 2);
                _messagePadding = new string(' ', timePadding.Length + GetLogLevelString(LogLevel.Information).Length + LoglevelPadding.Length);
                _newLineWithMessagePadding = Environment.NewLine + _messagePadding;
            }

        }
        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

    }
}
