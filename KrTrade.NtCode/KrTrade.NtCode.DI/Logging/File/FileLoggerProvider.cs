using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Nt.Core.Logging.Console;
using Nt.Core.Logging.Internal;
using Nt.Core.Options;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider, IDisposable
    {
        #region Private members

        private readonly ConcurrentDictionary<string, FileLogger> _loggers;
        private readonly IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        private readonly IDisposable _optionsReloadToken;
        private FileLoggerOptions _currentOptions;
        private ConcurrentDictionary<string, BaseFileFormatter> _formatters;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of <see cref="FileLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options) : this(options, Array.Empty<BaseFileFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatters">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options, IEnumerable<BaseFileFormatter> formatters)
        {
            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, FileLogger>();

            SetFormatters(formatters);
            ReloadFileLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadFileLoggerOptions);
        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FileLogger(name)
            {
                Options = GetCurrentOptions(),
                Formatter = GetCurrentFormatter(),
                ScopeProvider = _scopeProvider
            });

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _loggers.Clear();
        }

        #endregion

        #region Private methods

        private FileLoggerOptions GetCurrentOptions() => _currentOptions;
        // TODO: Devuelvo un valor por defecto. El usuario debería poder elegir el tipo de formato
        private BaseFileFormatter GetCurrentFormatter() => _formatters[FileFormatterNames.Normal];
        private void SetFormatters(IEnumerable<BaseFileFormatter> formatters)
        {
            var cd = new ConcurrentDictionary<string, BaseFileFormatter>(StringComparer.OrdinalIgnoreCase);

            bool added = false;
            if (formatters != null)
            {
                foreach (BaseFileFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                    added = true;
                }
            }

            if (!added)
            {
                cd.TryAdd(FileFormatterNames.Normal, new FileFormatter(new FileFormatterOptionsMonitor<FileFormatterOptions>(new FileFormatterOptions())));
            }

            _formatters = cd;
        }


        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadFileLoggerOptions(FileLoggerOptions currentOptions)
        {
            _currentOptions = currentOptions ?? new FileLoggerOptions();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
            {
                logger.Value.Options = GetCurrentOptions();
                logger.Value.Formatter = GetCurrentFormatter();
            }
        }

        #endregion

    }
}
