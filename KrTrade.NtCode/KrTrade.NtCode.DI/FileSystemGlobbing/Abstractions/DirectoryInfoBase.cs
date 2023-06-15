using System.Collections.Generic;

namespace KrTrade.NtCode.FileSystemGlobbing.Abstractions
{
    //
    // Resumen:
    //     Represents a directory
    public abstract class DirectoryInfoBase : FileSystemInfoBase
    {
        //
        // Resumen:
        //     Enumerates all files and directories in the directory.
        //
        // Devuelve:
        //     Collection of files and directories
        public abstract IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos();

        //
        // Resumen:
        //     Returns an instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase
        //     that represents a subdirectory
        //
        // Parámetros:
        //   path:
        //     The directory name
        //
        // Devuelve:
        //     Instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase
        //     even if directory does not exist
        public abstract DirectoryInfoBase GetDirectory(string path);

        //
        // Resumen:
        //     Returns an instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileInfoBase
        //     that represents a file in the directory
        //
        // Parámetros:
        //   path:
        //     The file name
        //
        // Devuelve:
        //     Instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileInfoBase
        //     even if file does not exist
        public abstract FileInfoBase GetFile(string path);
    }
}
