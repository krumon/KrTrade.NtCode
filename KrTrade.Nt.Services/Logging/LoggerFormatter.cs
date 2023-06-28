using KrTrade.Nt.Core.Data;
using System;
using System.IO;

namespace KrTrade.Nt.Services
{
    public abstract class LoggerFormatter
    {

        protected LoggerFormatter(string name)
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
        /// <typeparam name="T">The type of the object to be written.</typeparam>
        public abstract void Write<T>(in LogEntry<T> logEntry, TextWriter textWriter);
    }
}
