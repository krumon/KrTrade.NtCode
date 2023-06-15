using System;

namespace Nt.Core.FileProviders.Physical
{
    internal interface IClock
    {
        DateTime UtcNow { get; }
    }
}
