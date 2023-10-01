using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Logging.Abstractions;
using KrTrade.Nt.Scripts.NinjatraderObjects;
using KrTrade.Nt.Scripts.Services;
using System;
using System.IO;

namespace KrTrade.Nt.Scripts.Logging
{
    /// <summary>
    /// A logger that writes the logs to ninjatrader windows (Output or Log)
    /// </summary>
    internal class NinjatraderLogger : ILogger
    {
        private readonly string _name;
        private readonly Action<object> _ninjascriptPrintMethod;
        private readonly Action _ninjascriptClearMethod;

        /// <summary>
        /// Creates <see cref="NinjatraderLogger"/> default instance.
        /// </summary>
        /// <param name="name">The category name of the logger.</param>
        /// <param name="ninjascriptPrintMethod">The ninjascript method to log a message in the output window.</param>
        /// <param name="ninjascriptClearMethod">The ninjascript method to clear the output window.</param>
        internal NinjatraderLogger(string name, Action<object> ninjascriptPrintMethod, Action ninjascriptClearMethod)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            // TODO: Make available the exception conditions.
            _ninjascriptPrintMethod = ninjascriptPrintMethod; // ?? throw new ArgumentNullException(nameof(ninjascriptPrintMethod));
            _ninjascriptClearMethod = ninjascriptClearMethod; // ?? throw new ArgumentNullException(nameof(ninjascriptClearMethod));
        }

        /// <summary>
        /// Creates <see cref="NinjatraderLogger"/> default instance.
        /// </summary>
        /// <param name="name">The category name of the logger.</param>
        /// <param name="ninjascript">The <see cref="INinjaScriptBase"/> instance.</param>
        internal NinjatraderLogger(string name, INinjaScriptBase ninjascript)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            _ninjascriptPrintMethod = ninjascript.Print;
            _ninjascriptClearMethod = ninjascript.ClearOutputWindow;
        }

        internal NinjatraderLoggerFormatter Formatter { get; set; }
        internal NinjatraderLoggerOptions Options { get; set; }

        [ThreadStatic]
        private static StringWriter t_stringWriter;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            LogEntry<TState> logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);

            if(eventId != null && eventId.Id == NinjascriptLoggingEventIds.ClearOutputWindow)
            {
                _ninjascriptClearMethod?.Invoke();
                return;
            }

            if (t_stringWriter == null) t_stringWriter = new StringWriter();
            Formatter.Write(in logEntry, t_stringWriter);
            var sb = t_stringWriter.GetStringBuilder();
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

            Write(Formatter.Name, computedAnsiString);
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel != LogLevel.None;
        }

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public System.IDisposable BeginScope<TState>(TState state) => null;

        private void Write(string name, string message)
        {
            if (name == NinjatraderLoggerFormatterNames.Output)
                _ninjascriptPrintMethod?.Invoke(message);
        }
    }
}
