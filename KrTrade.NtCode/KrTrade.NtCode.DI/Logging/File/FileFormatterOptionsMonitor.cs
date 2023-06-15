using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Logging.File
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
