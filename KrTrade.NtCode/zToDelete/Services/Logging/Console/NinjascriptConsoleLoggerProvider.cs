using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace KrTrade.Nt.Console
{
    [ProviderAlias("ColorConsole")]
    public sealed class NinjascriptConsoleLoggerProvider : ILoggerProvider
    {

        #region Private members

        private readonly IDisposable _onChangeToken;
        private NinjascriptConsoleLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, NinjascriptConsoleLoggerService> _loggers =
            new ConcurrentDictionary<string, NinjascriptConsoleLoggerService>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Constructors

        public NinjascriptConsoleLoggerProvider(
            IOptionsMonitor<NinjascriptConsoleLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        #endregion

        #region Implementation methods

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new NinjascriptConsoleLoggerService(name, GetCurrentConfig));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }

        #endregion

        #region Private methods

        private NinjascriptConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

        #endregion

    }
}
