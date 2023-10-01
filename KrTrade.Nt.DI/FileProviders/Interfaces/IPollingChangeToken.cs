using KrTrade.Nt.DI.Primitives;
using System.Threading;

namespace KrTrade.Nt.DI.FileProviders
{
    internal interface IPollingChangeToken : IChangeToken
    {
        CancellationTokenSource CancellationTokenSource { get; }
    }
}
