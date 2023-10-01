using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.DI.Logging.File
{
    internal sealed class FileFormatterOptionsMonitor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TOptions> :
        IOptionsMonitor<TOptions>
        where TOptions : FileFormatterOptions
    {
        private TOptions _options;
        public FileFormatterOptionsMonitor(TOptions options)
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
