using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.File;
using KrTrade.NtCode.Logging.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="FileLogger"/>.
    /// </summary>
    public static class FileLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.AddFileFormatter<FileFormatter, FileFormatterOptions>();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<FileLoggerOptions, FileLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="options">The file logger options.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> options)
        {
            builder.AddFile();
            builder.Services.Configure(options);

            return builder;
        }

        internal static ILoggingBuilder AddFileWithFormatter<TOptions>(this ILoggingBuilder builder, string name, Action<TOptions> configure)
            where TOptions : FileFormatterOptions
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddFormatterWithName(name);
            builder.Services.Configure(configure);

            return builder;
        }
        private static ILoggingBuilder AddFormatterWithName(this ILoggingBuilder builder, string name) =>
            builder.AddFile((FileLoggerOptions options) => options.FormatterName = name);

    }
}
