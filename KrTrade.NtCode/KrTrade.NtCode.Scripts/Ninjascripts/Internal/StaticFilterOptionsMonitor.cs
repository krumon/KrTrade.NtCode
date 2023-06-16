using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Ninjascripts.Internal
{
    internal sealed class StaticFilterOptionsMonitor : IOptionsMonitor<NinjascriptFilterOptions>
    {
        public StaticFilterOptionsMonitor(NinjascriptFilterOptions currentValue)
        {
            CurrentValue = currentValue;
        }

        public IDisposable OnChange(Action<NinjascriptFilterOptions, string> listener) => null;

        public NinjascriptFilterOptions Get(string name) => CurrentValue;

        public NinjascriptFilterOptions CurrentValue { get; }
    }
}
