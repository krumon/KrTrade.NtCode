
using Nt.Core.Logging;

namespace KrTrade.NtCode.Logging
{
    internal static class NinjascriptLoggingEventIds
    {
        public static readonly EventId PrintToOutputWindow = new EventId(1, nameof(PrintToOutputWindow));
        public static readonly EventId ClearOutputWindow = new EventId(2, nameof(ClearOutputWindow));
        public static readonly EventId PrintToFile = new EventId(3, nameof(PrintToFile));
    }
}
