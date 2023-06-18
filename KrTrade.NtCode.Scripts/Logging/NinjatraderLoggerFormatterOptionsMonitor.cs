using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.Scripts.Logging
{
    internal sealed class NinjatraderLoggerFormatterOptionsMonitor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TOptions> : IOptionsMonitor<TOptions>
        where TOptions : NinjatraderLoggerFormatterOptions
    {
        private readonly TOptions _options;

        public NinjatraderLoggerFormatterOptionsMonitor(TOptions options)
        {
            _options = options;
        }

        public TOptions Get(string name) => _options;

        public IDisposable OnChange(Action<TOptions, string> listener)
        {
            return null;
        }

        public TOptions CurrentValue => _options;
    }
}