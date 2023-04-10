using Nt.Core.Logging.EventSource;
using Nt.Core.Options;
using Nt.Core.Primitives;

namespace Nt.Core.Logging.Internal
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
