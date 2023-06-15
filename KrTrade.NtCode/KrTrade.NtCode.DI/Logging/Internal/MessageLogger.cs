using System;

namespace KrTrade.NtCode.Logging.Internal
{
    internal readonly struct MessageLogger
    {
        public MessageLogger(ILogger logger, string category, string providerTypeFullName, LogLevel? minLevel, Func<string, string, LogLevel, bool> filter)
        {
            Logger = logger;
            Category = category;
            ProviderTypeFullName = providerTypeFullName;
            MinLevel = minLevel;
            Filter = filter;
        }

        public ILogger Logger { get; }

        public string Category { get; }

        private string ProviderTypeFullName { get; }

        public LogLevel? MinLevel { get; }

        public Func<string, string, LogLevel, bool> Filter { get; }

        public bool IsEnabled(LogLevel level)
        {
            var nLevel = (int)level;
            var nMinLevel = (int)MinLevel;
            if (MinLevel != null && (int)level < (int)MinLevel)
                return false;

            if (Filter != null)
                return Filter(ProviderTypeFullName, Category, level);

            return true;
        }
    }
}
