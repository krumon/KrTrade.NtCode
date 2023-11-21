using KrTrade.Nt.Core.Logging;
using NinjaTrader.NinjaScript;
using System;
using System.IO;

namespace KrTrade.Nt.Services
{
    public abstract class BaseLogService<TOptions, TFormatter> : BaseService<TOptions>
        where TOptions : BaseLogOptions<TFormatter>, new()
        where TFormatter : BaseLogFormatter, new()
    {
        [ThreadStatic]
        private static StringWriter stringWriter;

        /// <summary>
        /// Gets last log length.
        /// </summary>
        protected int LastLength { get; set; }

        protected abstract Action<object> WriteMethod {  get; }
        protected abstract Action ClearMethod {  get; }

        protected BaseLogService(NinjaScriptBase ninjascript) : this(ninjascript, new TOptions())
        {
        }
        protected BaseLogService(NinjaScriptBase ninjascript, TOptions options) : base(ninjascript, options)
        {
        }
        protected BaseLogService(NinjaScriptBase ninjascript, Action<TOptions> delegateOptions) : base(ninjascript, delegateOptions)
        {
        }

        public void Clear() => ClearMethod();
        public void LogTrace(string message) => Log(LogLevel.Trace, message, null);
        public void LogDebug(string message) => Log(LogLevel.Debug, message, null);
        public void LogInformation(string message) => Log(LogLevel.Information, message, null);
        public void LogWarning(string message) => Log(LogLevel.Warning, message, null);
        public void LogError(string message) => Log(LogLevel.Error, message, null);
        public void LogError(Exception exception) => Log(LogLevel.Error, null, exception);
        public void LogError(string message, Exception exception) => Log(LogLevel.Error, message, exception);
        public void LogCritical(string message) => Log(LogLevel.Critical, message, null);
        public void LogCritical(string message, Exception exception) => Log(LogLevel.Critical, message, exception);
        public void LogText(LogLevel logLevel, string message) => Log(logLevel, message, null);
        public void LogException(LogLevel logLevel, Exception exception) => Log(logLevel, null, exception);

        protected void Log(LogLevel logLevel, object value, Exception exception)
        {
            if (!Options.IsEnable)
                return;

            if (logLevel < Options.LogLevel)
                return;

            LogEntry logEntry = CreateLogEntry(logLevel, value, exception);

            if (stringWriter == null) stringWriter = new StringWriter();
            Options.Formatter.Write(in logEntry, stringWriter);
            var sb = stringWriter.GetStringBuilder();
            if (sb.Length == 0)
                return;

            string computedAnsiString = sb.ToString();
            LastLength = computedAnsiString.Length;
            sb.Clear();
            if (sb.Capacity > 1024)
            {
                sb.Capacity = 1024;
            }

            WriteMethod(computedAnsiString);
        }
        protected void Write(object value)
        {
            if (!Options.IsEnable)
                return;

            if (value == null)
                return;

            var message = value.ToString();
            LastLength = message.Length;
            WriteMethod(message);
        }
        protected void WriteText(string message)
        {
            if (!Options.IsEnable)
                return;

            if (string.IsNullOrEmpty(message))
                return;

            LastLength = message.Length;
            WriteMethod(message);
        }
        protected void WriteUpperText(string message)
        {
            if (!Options.IsEnable)
                return;

            if (string.IsNullOrEmpty(message))
                return;

            LastLength = message.Length;
            WriteMethod(message);
        }

        private LogEntry CreateLogEntry(LogLevel logLevel, object value, Exception exception)
        {
            return new LogEntry(
                logLevel,
                Ninjascript.State,
                Ninjascript.BarsInProgress,
                Ninjascript.CurrentBars[Ninjascript.BarsInProgress],
                Ninjascript.BarsArray[Ninjascript.BarsInProgress].Instrument.MasterInstrument.Name,
                Ninjascript.BarsArray[Ninjascript.BarsInProgress].BarsPeriod,
                IsInRunningState() ? Ninjascript.Times[Ninjascript.BarsInProgress][0] : DateTime.Now,
                value,
                exception
                );
        }

    }
}
