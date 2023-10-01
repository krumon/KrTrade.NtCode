using KrTrade.Nt.DI.Logging.EventSource;
using KrTrade.Nt.DI.Options;
using KrTrade.Nt.DI.Primitives;

namespace KrTrade.Nt.DI.Logging.Internal
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
