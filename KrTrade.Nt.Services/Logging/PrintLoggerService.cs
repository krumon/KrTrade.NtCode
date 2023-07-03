using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;
using System.IO;

namespace KrTrade.Nt.Services
{
    public class PrintLoggerService : IPrintLoggerService
    {
        private const int maxCapacity = 10000;
        private const int minCapacity = 1;

        private readonly INinjaScriptService _nscript;
        private readonly int _capacity = 100;
        private readonly PrintLoggerOptions _options;

        // TODO: Create cache list for store the log messages.
        private int _count;

        internal PrintLoggerFormatter Formatter { get; set; }
        internal PrintLoggerOptions Options => _options;

        private PrintLoggerService(INinjaScriptService ninjascript) : this(ninjascript, new PrintLoggerOptions()) { }
        private PrintLoggerService(INinjaScriptService ninjascript, PrintLoggerOptions options)
        {
            _nscript = ninjascript ?? throw new Exception("The nscript argument cannot be null. The argument is necesary to configure the service.");
            _capacity = options.Capacity < minCapacity ? minCapacity : options.Capacity > maxCapacity ? maxCapacity : options.Capacity;
        }

        public static IPrintLoggerService Configure(INinjaScriptService nscript)
        {
            IPrintLoggerService service = new PrintLoggerService(nscript);
            service.Configure();
            return service;
        }
        public static IPrintLoggerService Configure(INinjaScriptService nscript, Action<PrintLoggerOptions> options)
        {
            PrintLoggerOptions delegateOptions = new PrintLoggerOptions();
            options?.Invoke(delegateOptions);
            IPrintLoggerService service = new PrintLoggerService(nscript,delegateOptions);
            service.Configure();
            return service;
        }

        public INinjaScriptService Ninjascript => _nscript;
        public LogLevel LogLevel => Options.LogLevel;
        public NinjaScriptState NsLogLevel => Options.NsLogLevel;
        public PriceState PriceLogLevel => Options.PriceLogLevel;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel < LogLevel) return false;
            if (_nscript.NinjaScript.State == State.Historical && NsLogLevel > NinjaScriptState.Historical) return false;
            if (_nscript.NinjaScript.State == State.Realtime && NsLogLevel > NinjaScriptState.Realtime) return false;
            if (NsLogLevel == NinjaScriptState.None) return false;

            //if ((_nscript.Event == NsEvent.Tick || _nscript.Event == NsEvent.Tick) && PriceLogLevel > PriceLogLevel.EachTick) return false;
            //if (_nscript.Event == NsEvent.PriceChanged && PriceLogLevel > PriceLogLevel.PriceChanged) return false;

            return true;
        }

        public void Configure()
        {
            _count = 0;
        }

        public void Open(int barsAgo = 0) => Print(OpenText(barsAgo));
        public void High(int barsAgo = 0) => Print(HighText(barsAgo));
        public void Low(int barsAgo = 0) => Print(LowText(barsAgo));
        public void Close(int barsAgo = 0) => Print(CloseText(barsAgo));
        public void Input(int barsAgo = 0) => Print(InputText(barsAgo));
        public void Text(object o) => Print(o);
        public void OHLC(int barsAgo = 0, char separator = '-') => Print(OhlcText(separator, barsAgo));

        private void Print(object o) 
        {
            //if (!IsEnabled)
            //    return;
            _nscript.NinjaScript.Print(o);
            _count++;
        }

        [ThreadStatic]
        private static StringWriter stringWriter;

        public void Log<T>(
            LogLevel logLevel,NinjaScriptState nsLevel, PriceState prLevel, 
            NinjaScriptName name, NinjaScriptType nsType, 
            T message, Exception exception, object data)
        {
            if (!IsEnabled(logLevel))
                return;

            //LogEntry<T> logEntry = new LogEntry<T>(logLevel, name, eventId, state, exception, formatter);

            if (stringWriter == null) stringWriter = new StringWriter();
            //Formatter.Write(in logEntry, stringWriter);
            var sb = stringWriter.GetStringBuilder();
            if (sb.Length == 0)
            {
                return;
            }
            string computedAnsiString = sb.ToString();
            sb.Clear();
            if (sb.Capacity > 1024)
            {
                sb.Capacity = 1024;
            }

            //Write(Formatter.Name, computedAnsiString);
        }

        private string OpenText(int barsAgo) => "Open: " + _nscript.NinjaScript.Open[barsAgo];
        private string HighText(int barsAgo) => "High: " + _nscript.NinjaScript.High[barsAgo];
        private string LowText(int barsAgo) => "Low: " + _nscript.NinjaScript.Low[barsAgo];
        private string CloseText(int barsAgo) => "Close: " + _nscript.NinjaScript.Close[barsAgo];
        private string InputText(int barsAgo) => "Input: " + _nscript.NinjaScript.Input[barsAgo];
        private string OhlcText(char separator = '-', int barsAgo = 0)
        {
            return
                OpenText(barsAgo) + separator +
                HighText(barsAgo) + separator +
                LowText(barsAgo) + separator +
                CloseText(barsAgo);
        }


//        private void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
//        {
//            // Create the message and make sure the message is created.
//            string[] messages = logEntry.Formatter(logEntry.State, logEntry.Exception).Split('|');
//            if (logEntry.Exception == null && messages == null)
//            {
//                return;
//            }
//            string source = null;
//            string message = null;
//            if (messages != null)
//            {
//                source = messages.Length > 1 ? messages[0] : null;
//                message = messages.Length > 1 ? messages[1] : messages[0];
//            }

//            LogLevel logLevel = logEntry.LogLevel;
//            string logLevelString = GetLogLevelString(logLevel);

//            string timestamp = null;
//            string timestampFormat = FormatterOptions.TimestampFormat;
//            if (timestampFormat != null)
//            {
//                DateTimeOffset dateTimeOffset = GetCurrentDateTime();
//                timestamp = dateTimeOffset.ToString(timestampFormat);
//            }
//            if (timestamp != null)
//            {
//                textWriter.Write(timestamp);
//            }
//            if (logLevelString != null)
//            {
//                textWriter.WriteColoredMessage(logLevelString, logLevelColors.Background, logLevelColors.Foreground);
//            }
//            CreateDefaultLogMessage(textWriter, logEntry, message, source, scopeProvider);
//        }

//        private void CreateDefaultLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, string source, IExternalScopeProvider scopeProvider)
//        {
//            bool singleLine = FormatterOptions.SingleLine;
//            int eventId = logEntry.EventId.Id;
//            Exception exception = logEntry.Exception;

//            // Example:
//            // info: ConsoleApp.Program[10]
//            //       Request received

//            // category and event id
//            textWriter.Write(LoglevelPadding);
//            if (source != null)
//                textWriter.Write(source);
//            else
//                textWriter.Write(logEntry.Category);

//            textWriter.Write('[');

//#if NETCOREAPP
//            Span<char> span = stackalloc char[10];
//            if (eventId.TryFormat(span, out int charsWritten))
//                textWriter.Write(span.Slice(0, charsWritten));
//            else
//#endif
//            textWriter.Write(eventId.ToString());

//            textWriter.Write(']');
//            if (!singleLine)
//            {
//                textWriter.Write(Environment.NewLine);
//            }

//            // scope information
//            WriteScopeInformation(textWriter, scopeProvider, singleLine);
//            WriteMessage(textWriter, message, singleLine);

//            // Example:
//            // System.InvalidOperationException
//            //    at Namespace.Class.Function() in File:line X
//            if (exception != null)
//            {
//                // exception message
//                WriteMessage(textWriter, exception.ToString(), singleLine);
//            }
//            if (singleLine)
//            {
//                textWriter.Write(Environment.NewLine);
//            }
//        }

//        private void WriteMessage(TextWriter textWriter, string message, bool singleLine)
//        {
//            if (!string.IsNullOrEmpty(message))
//            {
//                if (singleLine)
//                {
//                    textWriter.Write(' ');
//                    WriteReplacing(textWriter, Environment.NewLine, " ", message);
//                }
//                else
//                {
//                    textWriter.Write(_messagePadding);
//                    WriteReplacing(textWriter, Environment.NewLine, _newLineWithMessagePadding, message);
//                    textWriter.Write(Environment.NewLine);
//                }
//            }

//        }
//        private static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
//        {
//            string newMessage = message.Replace(oldValue, newValue);
//            writer.Write(newMessage);
//        }

//        private DateTimeOffset GetCurrentDateTime()
//        {
//            return FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
//        }
    }
}
