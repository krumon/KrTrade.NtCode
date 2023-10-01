using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.DI.Logging.Internal
{
    internal sealed class StaticFilterOptionsMonitor : IOptionsMonitor<LoggerFilterOptions>
    {
        public StaticFilterOptionsMonitor(LoggerFilterOptions currentValue)
        {
            CurrentValue = currentValue;
        }

        public IDisposable OnChange(Action<LoggerFilterOptions, string> listener) => null;

        public LoggerFilterOptions Get(string name) => CurrentValue;

        public LoggerFilterOptions CurrentValue { get; }
    }
}
