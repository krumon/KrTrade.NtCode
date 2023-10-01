namespace KrTrade.Nt.DI.FileSystemGlobbing.Abstractions
{
    //
    // Resumen:
    //     Shared abstraction for files and directories
    public abstract class FileSystemInfoBase
    {
        //
        // Resumen:
        //     A string containing the name of the file or directory
        public abstract string Name { get; }

        //
        // Resumen:
        //     A string containing the full path of the file or directory
        public abstract string FullName { get; }

        //
        // Resumen:
        //     The parent directory for the current file or directory
        public abstract DirectoryInfoBase ParentDirectory { get; }
    }
}
