using KrTrade.NtCode.Primitives;
using System.Threading;

namespace KrTrade.NtCode.FileProviders
{
    internal interface IPollingChangeToken : IChangeToken
    {
        CancellationTokenSource CancellationTokenSource { get; }
    }
}
