﻿using KrTrade.Nt.DI.Options;
using System.IO;

namespace KrTrade.Nt.DI.Logging.File
{
    internal sealed class DefaultFileLoggerConfigureOptions : ConfigureOptions<FileLoggerOptions>
    {
        public DefaultFileLoggerConfigureOptions() : base(options =>
        {
            options.LogLevel = LogLevel.Debug;
            options.LogAtTop = false;
            options.Directory = Directory.GetCurrentDirectory();
            options.FileName = "defaultlogfile.txt";
        })
        { }
    }
}
