using KrTrade.NtCode.Logging.EventSource;
using KrTrade.NtCode.Options;
using KrTrade.NtCode.Primitives;

namespace KrTrade.NtCode.Logging.Internal
{
    internal sealed class EventLogFiltersConfigureOptionsChangeSource : IOptionsChangeTokenSource<LoggerFilterOptions>
    {
        private readonly LoggingEventSource _eventSource;

        public EventLogFiltersConfigureOptionsChangeSource(LoggingEventSource eventSource)
        {
            _eventSource = eventSource;
        }

        public IChangeToken GetChangeToken() => _eventSource.GetFilterChangeToken();

        public string Name { get; }
    }
}
