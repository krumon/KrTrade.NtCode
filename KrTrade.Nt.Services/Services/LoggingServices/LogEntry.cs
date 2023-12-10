using KrTrade.Nt.Core.Logging;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Holds the information for a single log entry.
    /// </summary>
    public readonly struct LogEntry
    {
        /// <summary>
        /// Initializes an instance of the LogEntry struct.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="ninjaScriptState">The 'State' property of the 'NinjaScript' at the time the login has been executed.</param>
        /// <param name="ninjaScriptBarsInProgress">The 'BarsInProgress' property of the 'NinjaScript' at the time the login has been executed.</param>
        /// <param name="ninjaScriptCurrentBar">The 'CurrentBar' property of the 'NinjaScript' at the time the login has been executed.</param>
        /// <param name="instrumentName">The Instrument Name of the bars in progress in the 'NinjaScript' at the time the login has been executed.</param>
        /// <param name="barsPeriod">The time frame of the bars in progress in the 'NinjaScript' at the time the login has been executed.</param>
        /// <param name="time0">The 'Time[0]' value of the 'NinjaScript' at the time the login has been executed</param>
        /// <param name="message">The message for which log is being written.</param>
        /// <param name="exception">The log exception.</param>
        public LogEntry(LogLevel logLevel, State ninjaScriptState, int ninjaScriptBarsInProgress, int ninjaScriptCurrentBar, string instrumentName, BarsPeriod barsPeriod, DateTime time0, object message, Exception exception)
        {
            LogLevel = logLevel;
            State = ninjaScriptState;
            BarsInProgress = ninjaScriptBarsInProgress;
            CurrentBar = ninjaScriptCurrentBar;
            InstrumentName = instrumentName;
            BarsPeriod = barsPeriod;
            Time = time0;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Gets the LogLevel
        /// </summary>
        public LogLevel LogLevel { get; }

        /// <summary>
        /// Gets log State.
        /// </summary>
        public State State { get; }

        /// <summary>
        /// Gets log barsInProgress.
        /// </summary>
        public int BarsInProgress { get; }

        /// <summary>
        /// Gets log currentBar.
        /// </summary>
        public int CurrentBar { get; }

        /// <summary>
        /// Gets log instrument name.
        /// </summary>
        public string InstrumentName { get; }

        /// <summary>
        /// Gets log time frame.
        /// </summary>
        public BarsPeriod BarsPeriod { get; }

        /// <summary>
        /// Gets log time.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Gets the log message.
        /// </summary>
        public object Message { get; }

        /// <summary>
        /// Gets the log exception
        /// </summary>
        public Exception Exception { get; }

    }
}
