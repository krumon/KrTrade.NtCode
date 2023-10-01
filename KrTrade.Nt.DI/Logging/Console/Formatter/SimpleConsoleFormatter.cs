using KrTrade.Nt.DI.Logging.Abstractions;
using KrTrade.Nt.DI.Options;
using System;
using System.IO;

namespace KrTrade.Nt.DI.Logging.Console
{
    internal sealed class SimpleConsoleFormatter : ConsoleFormatter, IDisposable
    {
        private const string LoglevelPadding = ": ";
        private static readonly string _messagePadding = new string(' ', GetLogLevelString(LogLevel.Information).Length + LoglevelPadding.Length);
        private static readonly string _newLineWithMessagePadding = Environment.NewLine + _messagePadding;
        private readonly IDisposable _optionsReloadToken;

        public SimpleConsoleFormatter(IOptionsMonitor<SimpleConsoleFormatterOptions> options)
            : base(ConsoleFormatterNames.Simple)
        {
            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadLoggerOptions);
        }

        private void ReloadLoggerOptions(SimpleConsoleFormatterOptions options)
        {
            FormatterOptions = options;
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

        internal SimpleConsoleFormatterOptions FormatterOptions { get; set; }

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
                source = messages.Length > 1 ? messages[0] : null;
                message = messages.Length > 1 ? messages[1] : messages[0];
            }
            
            LogLevel logLevel = logEntry.LogLevel;
            ConsoleColors logLevelColors = GetLogLevelConsoleColors(logLevel);
            string logLevelString = GetLogLevelString(logLevel);

            string timestamp = null;
            string timestampFormat = FormatterOptions.TimestampFormat;
            if (timestampFormat != null)
            {
                DateTimeOffset dateTimeOffset = GetCurrentDateTime();
                timestamp = dateTimeOffset.ToString(timestampFormat);
            }
            if (timestamp != null)
            {
                textWriter.Write(timestamp);
            }
            if (logLevelString != null)
            {
                textWriter.WriteColoredMessage(logLevelString, logLevelColors.Background, logLevelColors.Foreground);
            }
            CreateDefaultLogMessage(textWriter, logEntry, message, source, scopeProvider);
        }

        private void CreateDefaultLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, string source, IExternalScopeProvider scopeProvider)
        {
            bool singleLine = FormatterOptions.SingleLine;
            int eventId = logEntry.EventId.Id;
            Exception exception = logEntry.Exception;

            // Example:
            // info: ConsoleApp.Program[10]
            //       Request received

            // category and event id
            textWriter.Write(LoglevelPadding);
            if(source != null)
                textWriter.Write(source);
            else
                textWriter.Write(logEntry.Category);

            textWriter.Write('[');

#if NETCOREAPP
            Span<char> span = stackalloc char[10];
            if (eventId.TryFormat(span, out int charsWritten))
                textWriter.Write(span.Slice(0, charsWritten));
            else
#endif
            textWriter.Write(eventId.ToString());

            textWriter.Write(']');
            if (!singleLine)
            {
                textWriter.Write(Environment.NewLine);
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
        private static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }

        private DateTimeOffset GetCurrentDateTime()
        {
            return FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
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

        private ConsoleColors GetLogLevelConsoleColors(LogLevel logLevel)
        {
            bool disableColors = (FormatterOptions.ColorBehavior == LoggerColorBehavior.Disabled) ||
                (FormatterOptions.ColorBehavior == LoggerColorBehavior.Default && System.Console.IsOutputRedirected);
            if (disableColors)
            {
                return new ConsoleColors(null, null);
            }
            // We must explicitly set the background color if we are setting the foreground color,
            // since just setting one can look bad on the users console.
            switch (logLevel)
            {
                case LogLevel.Trace: return new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black);
                case LogLevel.Debug: return new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black);
                case LogLevel.Information: return new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
                case LogLevel.Warning: return new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black);
                case LogLevel.Error: return new ConsoleColors(ConsoleColor.Black, ConsoleColor.DarkRed);
                case LogLevel.Critical: return new ConsoleColors(ConsoleColor.White, ConsoleColor.DarkRed);
                default: return new ConsoleColors(null, null);
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

        private readonly struct ConsoleColors
        {
            public ConsoleColors(ConsoleColor? foreground, ConsoleColor? background)
            {
                Foreground = foreground;
                Background = background;
            }

            public ConsoleColor? Foreground { get; }

            public ConsoleColor? Background { get; }
        }
    }
}
