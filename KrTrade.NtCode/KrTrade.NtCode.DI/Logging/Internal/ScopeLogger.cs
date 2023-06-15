using System;

namespace KrTrade.NtCode.Logging.Internal
{
    internal readonly struct ScopeLogger
    {
        public ScopeLogger(ILogger logger, IExternalScopeProvider externalScopeProvider)
        {
            Logger = logger;
            ExternalScopeProvider = externalScopeProvider;
        }

        public ILogger Logger { get; }

        public IExternalScopeProvider ExternalScopeProvider { get; }

        public IDisposable CreateScope<TState>(TState state)
        {
            if (ExternalScopeProvider != null)
            {
                return ExternalScopeProvider.Push(state);
            }
            return Logger.BeginScope<TState>(state);
        }
    }
}
