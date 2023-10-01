using KrTrade.Nt.DI.Logging.EventSource;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Internal
{
    internal sealed class EventLogFiltersConfigureOptions : IConfigureOptions<LoggerFilterOptions>
    {
        private readonly LoggingEventSource _eventSource;

        public EventLogFiltersConfigureOptions(LoggingEventSource eventSource)
        {
            _eventSource = eventSource;
        }

        public void Configure(LoggerFilterOptions options)
        {
            foreach (LoggerFilterRule rule in _eventSource.GetFilterRules())
            {
                options.Rules.Add(rule);
            }
        }
    }
}
