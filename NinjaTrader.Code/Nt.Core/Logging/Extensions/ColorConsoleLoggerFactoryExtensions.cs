using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Logging.Console;
using Nt.Core.Logging.Options;
using System;

namespace Nt.Core.Logging
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
