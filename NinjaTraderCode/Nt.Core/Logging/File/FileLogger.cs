using Nt.Core.Logging.Abstractions;
using Nt.Core.Logging.Internal;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    internal class FileLogger : ILogger
    {
        private readonly string _name;
        private string _normalizePath = string.Empty;
        private object _lock = default;

        internal FileLoggerOptions Options { get; set; }
        internal BaseFileFormatter Formatter { get; set; }
        internal IExternalScopeProvider ScopeProvider { get; set; }
        [ThreadStatic]
        private static StringWriter t_stringWriter;

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// The lock to lock the list of locks
        /// </summary>
        protected static object FileLockLock = new object();

        /// <summary>
        /// Creates <see cref="FileLogger"/> default instance.
        /// </summary>
        /// <param name="name">The category name of the logger.</param>
        public FileLogger(string name) => _name = name;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            if (t_stringWriter == null) t_stringWriter = new StringWriter();
            LogEntry<TState> logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);
            Formatter.Write(in logEntry, ScopeProvider, t_stringWriter);

            var sb = t_stringWriter.GetStringBuilder();
            if (sb.Length == 0)
            {
                return;
            }
            string computedAnsiString = sb.ToString();
            sb.Clear();
            if (sb.Capacity > 1024)
            {
                sb.Capacity = 1024;
            }

            _normalizePath = GetPath(Options.Directory, Options.FileName);

            // Double safety even though the FileLocks should be thread safe
            lock (FileLockLock)
            {
                // Get the file lock based on the absolute path
                _lock = FileLocks.GetOrAdd(_normalizePath, path => new object());
            }

            // Lock the file
            lock (_lock)
            {
                // Open the file
                using (var writer = new StreamWriter(System.IO.File.Open(_normalizePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    WriteToFile(writer, computedAnsiString);
                }
            }
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel != LogLevel.None;
        }

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => ScopeProvider?.Push(state) ?? NullScope.Instance;

        private string NormalizeDirectoryPath(string directoryPath)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(
                 new string(Path.GetInvalidPathChars())
            );
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            string dir = System.Text.RegularExpressions.Regex.Replace(directoryPath, invalidRegStr, "_");

            return dir; //TryCreateDirectory(dir);
        }
        private string NormalizeFileName(string fileName)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(
                 new string(System.IO.Path.GetInvalidFileNameChars())
            );
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(fileName, invalidRegStr, "_");
        }
        private string GetPath(string directory, string fileName)
        {
            var normalizeDirectoryPath = NormalizeDirectoryPath(directory);
            if (!TryCreateDirectory(normalizeDirectoryPath))
            {
                if (!Options.EnsureDirectoryExists)
                    return string.Empty;

                //TODO: Crear un directorio standard para que en cualquier equipo se cree un directorio.
                normalizeDirectoryPath = AppContext.BaseDirectory;
                int ancestorFolder = 3;
                for (int i = 0; i <= ancestorFolder; i++)
                {
                    int idx = normalizeDirectoryPath.LastIndexOf('\\');
                    if (idx == -1)
                        break;
                    if (i == ancestorFolder)
                        idx += 1;
                    normalizeDirectoryPath = normalizeDirectoryPath.Substring(0, idx);
                }
            }
            var normalizeFileName = NormalizeFileName(fileName);

            return Path.Combine(normalizeDirectoryPath, normalizeFileName);
        }
        private bool TryCreateDirectory(string normalizeDirectoryPath)
        {
            if (Directory.Exists(normalizeDirectoryPath))
                return true;

            try
            {
                Directory.CreateDirectory(normalizeDirectoryPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteToFile(StreamWriter writer, string message)
        {
            // Go to end
            writer.BaseStream.Seek(0, SeekOrigin.End);

            // NOTE: Ignore logToTop in configuration as not efficient for files on OS

            // Write the message to the file
            writer.Write(message);
        }
    }
}
