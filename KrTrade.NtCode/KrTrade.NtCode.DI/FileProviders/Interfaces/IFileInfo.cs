using System;
using System.IO;

namespace Nt.Core.FileProviders
{
    //
    // Resumen:
    //     Represents a file in the given file provider.
    public interface IFileInfo
    {
        //
        // Resumen:
        //     True if resource exists in the underlying storage system.
        bool Exists { get; }

        //
        // Resumen:
        //     The length of the file in bytes, or -1 for a directory or non-existing files.
        long Length { get; }

        //
        // Resumen:
        //     The path to the file, including the file name. Return null if the file is not
        //     directly accessible.
        string PhysicalPath { get; }

        //
        // Resumen:
        //     The name of the file or directory, not including any path.
        string Name { get; }

        //
        // Resumen:
        //     When the file was last modified
        DateTimeOffset LastModified { get; }

        //
        // Resumen:
        //     True for the case TryGetDirectoryContents has enumerated a sub-directory
        bool IsDirectory { get; }

        //
        // Resumen:
        //     Return file contents as readonly stream. Caller should dispose stream when complete.
        //
        // Devuelve:
        //     The file stream
        Stream CreateReadStream();
    }
}
