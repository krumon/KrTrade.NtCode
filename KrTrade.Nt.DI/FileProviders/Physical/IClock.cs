using System;

namespace KrTrade.Nt.DI.FileProviders.Physical
{
    internal interface IClock
    {
        DateTime UtcNow { get; }
    }
}
