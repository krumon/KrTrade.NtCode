using System.Collections.Generic;

namespace KrTrade.NtCode.FileSystemGlobbing.Internal
{
    /// <summary>
    /// This API supports infrastructure and is not intended to be used directly from
    /// your code. This API may change or be removed in future releases.
    /// </summary>
    public interface ILinearPattern : IPattern
    {
        IList<IPathSegment> Segments { get; }
    }
}
