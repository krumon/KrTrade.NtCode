using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.Console;
using KrTrade.NtCode.Logging.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Builder extensions methods for <see cref="Console.Internal.ColorConsoleLogger"/>.
    /// </summary>
    public static class ColorConsoleLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a console logger named 'ColorConsole' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ColorConsoleLoggerOptions, ColorConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a console logger named 'ColorConsole' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure the <see cref="Console.Internal.ColorConsoleLogger"/>.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddColorConsoleLogger(
            this ILoggingBuilder builder,
            Action<ColorConsoleLoggerOptions> configure)
        {
            builder.AddColorConsoleLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
