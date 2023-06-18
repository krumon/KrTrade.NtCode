using KrTrade.NtCode.DependencyInjection;
using System.Threading;

namespace KrTrade.NtCode.Options.Internal
{
    internal sealed class UnnamedOptionsManager<[DynamicallyAccessedMembers(Options.DynamicallyAccessedMembers)] TOptions> : IOptions<TOptions>
        where TOptions : class
    {
        private readonly IOptionsFactory<TOptions> _factory;
        private volatile object _syncObj;
        private volatile TOptions _value;

        public UnnamedOptionsManager(IOptionsFactory<TOptions> factory) => _factory = factory;

        public TOptions Value
        {
            get
            {
                if (_value is TOptions value)
                {
                    return value;
                }

                lock (_syncObj ?? Interlocked.CompareExchange(ref _syncObj, new object(), null) ?? _syncObj)
                {
                    return _value ?? _factory.Create(Options.DefaultName);
                }
            }
        }
    }
}
