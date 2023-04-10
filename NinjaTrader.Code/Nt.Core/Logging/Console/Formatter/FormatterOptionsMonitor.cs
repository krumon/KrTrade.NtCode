using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using System;

namespace Nt.Core.Logging.Console
{
    internal sealed class FormatterOptionsMonitor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TOptions> : IOptionsMonitor<TOptions>
        where TOptions : ConsoleFormatterOptions
    {
        private readonly TOptions _options;
        public FormatterOptionsMonitor(TOptions options)
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
