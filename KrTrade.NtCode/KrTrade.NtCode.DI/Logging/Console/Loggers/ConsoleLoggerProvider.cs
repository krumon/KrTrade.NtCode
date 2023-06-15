using KrTrade.NtCode.Logging.Internal;
using KrTrade.NtCode.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KrTrade.NtCode.Logging.Console
{
    /// <summary>
    /// A provider of <see cref="ConsoleLogger"/> instances.
    /// </summary>
    [ProviderAlias("Console")]
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        // Does the console support ansi in windows
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        private readonly IOptionsMonitor<ConsoleLoggerOptions> _options;
        private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers;
        private ConcurrentDictionary<string, ConsoleFormatter> _formatters;
        private readonly ConsoleLoggerProcessor _messageQueue;
        private readonly IDisposable _optionsReloadToken;
        private IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public ConsoleLoggerProvider(IOptionsMonitor<ConsoleLoggerOptions> options)
            : this(options, Array.Empty<ConsoleFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        /// <param name="formatters">Log formatters added for <see cref="ConsoleLogger"/> insteaces.</param>
        public ConsoleLoggerProvider(IOptionsMonitor<ConsoleLoggerOptions> options, IEnumerable<ConsoleFormatter> formatters)
        {
            _options = options;
            _loggers = new ConcurrentDictionary<string, ConsoleLogger>();
            SetFormatters(formatters);

            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);

            _messageQueue = new ConsoleLoggerProcessor();

            if (DoesConsoleSupportAnsi())
            {
                _messageQueue.Console = new AnsiLogConsole();
                _messageQueue.ErrorConsole = new AnsiLogConsole(stdErr: true);
            }
            else
            {
                _messageQueue.Console = new AnsiParsingLogConsole();
                _messageQueue.ErrorConsole = new AnsiParsingLogConsole(stdErr: true);
            }
        }

        private static bool DoesConsoleSupportAnsi()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }

            // for Windows, check the console mode
            var stdOutHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(stdOutHandle, out uint consoleMode))
            {
                return false;
            }
            consoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            if (!SetConsoleMode(stdOutHandle, consoleMode))
            {
                return false;
            }
            return (consoleMode & ENABLE_VIRTUAL_TERMINAL_PROCESSING) == ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        }
        private void SetFormatters(IEnumerable<ConsoleFormatter> formatters = null)
        {
            var cd = new ConcurrentDictionary<string, ConsoleFormatter>(StringComparer.OrdinalIgnoreCase);

            bool added = false;
            if (formatters != null)
            {
                foreach (ConsoleFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                    added = true;
                }
            }

            if (!added)
            {
                cd.TryAdd(ConsoleFormatterNames.Simple, new SimpleConsoleFormatter(new FormatterOptionsMonitor<SimpleConsoleFormatterOptions>(new SimpleConsoleFormatterOptions())));
                cd.TryAdd(ConsoleFormatterNames.Systemd, new SystemdConsoleFormatter(new FormatterOptionsMonitor<ConsoleFormatterOptions>(new ConsoleFormatterOptions())));
                //cd.TryAdd(ConsoleFormatterNames.Json, new JsonConsoleFormatter(new FormatterOptionsMonitor<JsonConsoleFormatterOptions>(new JsonConsoleFormatterOptions())));
            }

            _formatters = cd;
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadLoggerOptions(ConsoleLoggerOptions options, string text = null)
        {
            if (options.FormatterName == null || !_formatters.TryGetValue(options.FormatterName, out ConsoleFormatter logFormatter))
            {
#pragma warning disable CS0618
                switch (options.Format)
                {
                    case ConsoleLoggerFormat.Systemd:
                        logFormatter = _formatters[ConsoleFormatterNames.Systemd];
                        break;
                    default:
                        logFormatter = _formatters[ConsoleFormatterNames.Simple];
                        break;
                }
                if (options.FormatterName == null)
                {
                    UpdateFormatterOptions(logFormatter, options);
                }
#pragma warning restore CS0618
            }

            foreach (KeyValuePair<string, ConsoleLogger> logger in _loggers)
            {
                logger.Value.Options = options;
                logger.Value.Formatter = logFormatter;
            }
        }

        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            if (_options.CurrentValue.FormatterName == null || !_formatters.TryGetValue(_options.CurrentValue.FormatterName, out ConsoleFormatter logFormatter))
            {
#pragma warning disable CS0618
                switch (_options.CurrentValue.Format)
                {
                    case ConsoleLoggerFormat.Systemd:
                        logFormatter = _formatters[ConsoleFormatterNames.Systemd];
                        break;
                    default:
                        logFormatter = _formatters[ConsoleFormatterNames.Simple];
                        break;
                }
#pragma warning restore CS0618

                if (_options.CurrentValue.FormatterName == null)
                {
                    UpdateFormatterOptions(logFormatter, _options.CurrentValue);
                }
            }

            return _loggers.TryGetValue(name, out ConsoleLogger logger) ?
                logger :
                _loggers.GetOrAdd(name, new ConsoleLogger(name, _messageQueue)
                {
                    Options = _options.CurrentValue,
                    ScopeProvider = _scopeProvider,
                    Formatter = logFormatter,
                });
        }

#pragma warning disable CS0618
        private void UpdateFormatterOptions(ConsoleFormatter formatter, ConsoleLoggerOptions deprecatedFromOptions)
        {
            // kept for deprecated apis:
            if (formatter is SimpleConsoleFormatter defaultFormatter)
            {
                defaultFormatter.FormatterOptions = new SimpleConsoleFormatterOptions()
                {
                    ColorBehavior = deprecatedFromOptions.DisableColors ? LoggerColorBehavior.Disabled : LoggerColorBehavior.Enabled,
                    IncludeScopes = deprecatedFromOptions.IncludeScopes,
                    TimestampFormat = deprecatedFromOptions.TimestampFormat,
                    UseUtcTimestamp = deprecatedFromOptions.UseUtcTimestamp,
                };
            }
            else if (formatter is SystemdConsoleFormatter systemdFormatter)
            {
                systemdFormatter.FormatterOptions = new ConsoleFormatterOptions()
                {
                    IncludeScopes = deprecatedFromOptions.IncludeScopes,
                    TimestampFormat = deprecatedFromOptions.TimestampFormat,
                    UseUtcTimestamp = deprecatedFromOptions.UseUtcTimestamp,
                };
            }
        }
#pragma warning restore CS0618

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _messageQueue.Dispose();
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;

            foreach (KeyValuePair<string, ConsoleLogger> logger in _loggers)
            {
                logger.Value.ScopeProvider = _scopeProvider;
            }
        }
    }
}
