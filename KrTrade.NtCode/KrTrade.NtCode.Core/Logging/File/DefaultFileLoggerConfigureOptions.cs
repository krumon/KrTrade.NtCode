using Nt.Core.Options;
using System.IO;

namespace Nt.Core.Logging.File
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
