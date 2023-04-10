using Nt.Core.Options;
using System;

namespace Nt.Scripts.Ninjascripts.Internal
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
