using Nt.Core.Logging;
using Nt.Core.Logging.Console;
using Nt.Core.Logging.File;
using Nt.Core.Options;
using Nt.Scripts.NinjatraderObjects;
using Nt.Scripts.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nt.Scripts.Logging
{
    /// <summary>
    /// Provides the ability to log to ninjatrader output windows.
    /// </summary>
    [ProviderAlias("OutputWindow")]
    public class NinjatraderLoggerProvider : ILoggerProvider, IDisposable
    {
        private readonly ConcurrentDictionary<string, NinjatraderLogger> _loggers;
        private readonly System.IDisposable _optionsReloadToken;
        private NinjatraderLoggerOptions _currentOptions;
        private ConcurrentDictionary<string, NinjatraderLoggerFormatter> _formatters;
        readonly Action<object> _ninjascriptPrintMethod;
        readonly Action _ninjascriptClearMethod;

        /// <summary>
        /// Creates an instance of <see cref="NinjatraderLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public NinjatraderLoggerProvider(INinjaScriptBase ninjascript, IOptionsMonitor <NinjatraderLoggerOptions> options) : this(ninjascript, options, Array.Empty<NinjatraderLoggerFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="NinjatraderLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatters">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public NinjatraderLoggerProvider(INinjaScriptBase ninjascript, IOptionsMonitor<NinjatraderLoggerOptions> options, IEnumerable<NinjatraderLoggerFormatter> formatters)
        {
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));
            _ninjascriptPrintMethod = ninjascript.Print;
            _ninjascriptClearMethod = ninjascript.ClearOutputWindow;

            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, NinjatraderLogger>();

            SetFormatters(formatters);
            ReloadNinjascriptLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadNinjascriptLoggerOptions);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new NinjatraderLogger(name,_ninjascriptPrintMethod,_ninjascriptClearMethod)
            {
                Options = GetCurrentOptions(),
                Formatter = GetCurrentFormatter(),
            });

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _loggers.Clear();
        }

        private NinjatraderLoggerOptions GetCurrentOptions() => _currentOptions;
        private NinjatraderLoggerFormatter GetCurrentFormatter() 
        {
            if (_currentOptions.FormatterName == null || !_formatters.TryGetValue(_currentOptions.FormatterName, out NinjatraderLoggerFormatter ninjascriptFormatter))
                ninjascriptFormatter = _formatters[NinjatraderLoggerFormatterNames.Output];

            return ninjascriptFormatter;
        }

        private void SetFormatters(IEnumerable<NinjatraderLoggerFormatter> formatters)
        {
            var cd = new ConcurrentDictionary<string, NinjatraderLoggerFormatter>(StringComparer.OrdinalIgnoreCase);

            if (formatters != null)
            {
                foreach (NinjatraderLoggerFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                }
            }

            if (cd.Count == 0 || !cd.ContainsKey(NinjatraderLoggerFormatterNames.Output))
            {
                cd.TryAdd(NinjatraderLoggerFormatterNames.Output, new OutputWindowFormatter(new NinjatraderLoggerFormatterOptionsMonitor<OutputWindowFormatterOptions>(new OutputWindowFormatterOptions())));
            }

            _formatters = cd;
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadNinjascriptLoggerOptions(NinjatraderLoggerOptions currentOptions)
        {
            _currentOptions = currentOptions ?? new NinjatraderLoggerOptions();

            foreach (KeyValuePair<string, NinjatraderLogger> logger in _loggers)
            {
                logger.Value.Options = GetCurrentOptions();
                logger.Value.Formatter = GetCurrentFormatter();
            }
        }
    }
}
