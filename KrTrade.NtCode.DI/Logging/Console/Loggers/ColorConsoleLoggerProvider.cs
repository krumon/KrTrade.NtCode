using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.Options;
using System;
using System.Collections.Concurrent;

namespace KrTrade.NtCode.Logging.Console
{
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("ColorConsole")]
    public sealed class ColorConsoleLoggerProvider : ILoggerProvider
    {

        #region Private members

        private readonly IDisposable _onChangeToken;
        private ColorConsoleLoggerOptions _currentConfig;
        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers =
            new ConcurrentDictionary<string, ColorConsoleLogger>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Constructors

        public ColorConsoleLoggerProvider(
            IOptionsMonitor<ColorConsoleLoggerOptions> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        #endregion

        #region Implementation methods

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(name, GetCurrentConfig));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }

        #endregion

        #region Private methods

        private ColorConsoleLoggerOptions GetCurrentConfig() => _currentConfig;

        #endregion

    }
}
