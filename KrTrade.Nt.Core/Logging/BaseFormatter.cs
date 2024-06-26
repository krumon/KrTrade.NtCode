using System.IO;

namespace KrTrade.Nt.Core.Logging
{
    public abstract class BaseFormatter : IFormatter
    {
        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="textWriter">The string writer.</param>
        public abstract void Write(in LogEntry logEntry, TextWriter textWriter);
    }
}
