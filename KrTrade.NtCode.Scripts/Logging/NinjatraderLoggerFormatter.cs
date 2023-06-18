using KrTrade.NtCode.Logging.Abstractions;
using System;
using System.IO;

namespace KrTrade.NtCode.Logging
{
    public abstract class NinjatraderLoggerFormatter
    {
        protected NinjatraderLoggerFormatter(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Gets the name associated with the ninjascript log formatter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="textWriter">The string writer.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public abstract void Write<TState>(in LogEntry<TState> logEntry, TextWriter textWriter);
    }
}