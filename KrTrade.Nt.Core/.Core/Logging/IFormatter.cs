using System.IO;

namespace KrTrade.Nt.Core
{
    /// <summary>
    /// Represents properties and methods for all logging formatters.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="textWriter">The string writer.</param>
        void Write(in LogEntry logEntry, TextWriter textWriter);

    }
}
