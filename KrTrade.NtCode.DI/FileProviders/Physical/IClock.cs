using System;

namespace KrTrade.NtCode.FileProviders.Physical
{
    internal interface IClock
    {
        DateTime UtcNow { get; }
    }
}
