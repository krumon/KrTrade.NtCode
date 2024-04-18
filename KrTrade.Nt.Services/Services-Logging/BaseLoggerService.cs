using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System;
using System.IO;

namespace KrTrade.Nt.Services
{
    public abstract class BaseLoggerService<TOptions, TFormatter> : BaseService<NinjascriptServiceInfo,TOptions>
        where TOptions : BaseLoggerOptions<TFormatter>, new()
        where TFormatter : BaseFormatter, new()
    {
        [ThreadStatic]
        private static StringWriter stringWriter;

        /// <summary>
        /// Create <see cref="BaseLoggerService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        protected BaseLoggerService(NinjaScriptBase ninjascript) : base(ninjascript,null,null)
        {
        }

        ///// <summary>
        ///// Create <see cref="BaseLoggerService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="configureOptions">The configure options of the service.</param>
        //protected BaseLoggerService(NinjaScriptBase ninjascript, Action<TOptions> configureOptions) : base(ninjascript,configureOptions)
        //{
        //}

        /// <summary>
        /// Create <see cref="BaseLoggerService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="options">The configure options of the service.</param>
        protected BaseLoggerService(NinjaScriptBase ninjascript, TOptions options) : base(ninjascript,null,options)
        {
        }

        /// <summary>
        /// Represents the minimum log level. 0:Trace, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Critical, 6:None
        /// </summary>
        public LogLevel LogLevel { get => Options.LogLevel; set { Options.LogLevel = value; } }

        /// <summary>
        /// The minimum <see cref="NinjascriptLogLevel"/> to be logged. 0:Historical, 1:Configuration, 2:Realtime
        /// Historical level logs in all states.
        /// Configuration level not logs in Historical state.
        /// Realtime logs only in Realtime state.
        /// </summary>
        public NinjascriptLogLevel NinjascriptLogLevel { get => Options.NinjascriptLogLevel; set { Options.NinjascriptLogLevel = value; } }

        /// <summary>
        /// The minimum <see cref="BarsLogLevel"/> to be logged. 0:Tick, 1:PriceChanged, 2:BarClosed, 3:None
        /// </summary>
        public BarsLogLevel BarsLogLevel { get => Options.BarsLogLevel; set { Options.BarsLogLevel = value; } }

        /// <summary>
        /// Gets last log length.
        /// </summary>
        protected int LastLength { get; set; }

        protected abstract Action<object> WriteMethod {  get; }
        protected abstract Action ClearMethod {  get; }

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
        public void LogException(Exception exception) => LogException(LogLevel.Error, exception);
        public void LogException(LogLevel logLevel, Exception exception) => Log(logLevel, null, exception);
        public void LogText(LogLevel logLevel, string message) => Log(logLevel, message, null);
        public void Log(LogLevel logLevel, object value, Exception exception)
        {
            if (!IsLogLevelsEnable(logLevel))
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
            if (!IsEnable)
                return;

            if (value == null)
                return;

            var message = value.ToString();
            LastLength = message.Length;
            WriteMethod(message);
        }
        protected void WriteText(string message)
        {
            if (!IsEnable)
                return;

            if (string.IsNullOrEmpty(message))
                return;

            LastLength = message.Length;
            WriteMethod(message);
        }
        protected void WriteUpperText(string message)
        {
            if (!IsEnable)
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
                IsInRunningStates() ? Ninjascript.Times[Ninjascript.BarsInProgress][0] : DateTime.Now,
                value,
                exception
                );
        }

        public bool IsLogLevelsEnable(LogLevel logLevel)
        {
            if (!Options.IsEnable)
                return false;
            if (logLevel < Options.LogLevel)
                return false;
            if (NinjascriptLogLevel == NinjascriptLogLevel.None)
                return false;
            if (NinjascriptLogLevel == NinjascriptLogLevel.Realtime && Ninjascript.State != State.Realtime)
                return false;
            if (NinjascriptLogLevel == NinjascriptLogLevel.Configuration && Ninjascript.State == State.Historical)
                return false;

            return true;
        }
    }
}
