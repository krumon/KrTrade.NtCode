using Nt.Core.Primitives;
using System.Threading;

namespace Nt.Core.FileProviders
{
    internal interface IPollingChangeToken : IChangeToken
    {
        CancellationTokenSource CancellationTokenSource { get; }
    }
}
