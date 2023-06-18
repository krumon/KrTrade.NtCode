using KrTrade.NtCode.Logging.EventSource;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Logging.Internal
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
