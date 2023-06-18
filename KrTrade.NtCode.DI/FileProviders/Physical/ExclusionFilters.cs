using System;

namespace KrTrade.NtCode.FileProviders.Physical
{
    /// <summary>
    /// Specifies filtering behavior for files or directories.
    /// </summary>
    [Flags]
    public enum ExclusionFilters
    {
        /// <summary>
        ///  Equivalent to DotPrefixed | Hidden | System. Exclude files and directories when
        ///  the name begins with a period, or has either System.IO.FileAttributes.Hidden
        ///  or System.IO.FileAttributes.System is set on System.IO.FileSystemInfo.Attributes.
        /// </summary>
        Sensitive = 0x7,
        /// <summary>
        /// Exclude files and directories when the name begins with period.
        /// </summary>
        DotPrefixed = 0x1,
        /// <summary>
        /// Exclude files and directories when System.IO.FileAttributes.Hidden is set on
        /// System.IO.FileSystemInfo.Attributes.
        /// </summary>
        Hidden = 0x2,
        /// <summary>
        /// Exclude files and directories when System.IO.FileAttributes.System is set on
        /// System.IO.FileSystemInfo.Attributes.
        /// </summary>
        System = 0x4,
        /// <summary>
        /// Do not exclude any files.
        /// </summary>
        None = 0x0
    }
}
