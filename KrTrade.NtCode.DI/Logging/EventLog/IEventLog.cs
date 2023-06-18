using System.Diagnostics;

namespace KrTrade.Nt.DI.Logging.EventLog
{
    internal interface IEventLog
    {
        int? DefaultEventId { get; }

        int MaxMessageSize { get; }

        void WriteEntry(string message, EventLogEntryType type, int eventID, short category);
    }
}
