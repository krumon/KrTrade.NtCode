using Microsoft.Extensions.Logging;
using System;

namespace KrTrade.Nt.Console
{

    public sealed class NinjascriptConsoleLoggerService : ILogger
    {

        #region Private members

        /// <summary>
        /// Represents the name of the logger.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Represents the action to configure the logger.
        /// </summary>
        private readonly Func<NinjascriptConsoleLoggerConfiguration> _getCurrentConfig;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="NinjascriptConsoleLoggerService"/> default instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getCurrentConfig"></param>
        public NinjascriptConsoleLoggerService(
            string name,
            Func<NinjascriptConsoleLoggerConfiguration> getCurrentConfig) =>
            (_name, _getCurrentConfig) = (name, getCurrentConfig);

        #endregion

        #region Implementation methods

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            NinjascriptConsoleLoggerConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = System.Console.ForegroundColor;

                System.Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
                System.Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                System.Console.ForegroundColor = originalColor;
                System.Console.Write($"     {_name} - ");

                System.Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
                System.Console.Write($"{formatter(state, exception)}");

                System.Console.ForegroundColor = originalColor;
                System.Console.WriteLine();
            }
        }

        #endregion

    }
}
