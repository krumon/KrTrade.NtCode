using System.Collections;
using System.Collections.Generic;

namespace KrTrade.NtCode.FileProviders
{
    //
    // Resumen:
    //     Represents a directory's content in the file provider.
    public interface IDirectoryContents : IEnumerable<IFileInfo>, IEnumerable
    {
        //
        // Resumen:
        //     True if a directory was located at the given path.
        bool Exists { get; }
    }
}
