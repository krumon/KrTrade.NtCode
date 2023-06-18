using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="NinjatraderLogger"/>.
    /// </summary>
    public static class NinjatraderLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a new ninjatrader output window logger.
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddOutputWindowLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.AddNinjatraderLoggerFormatter<OutputWindowFormatter, OutputWindowFormatterOptions>();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NinjatraderLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<NinjatraderLoggerOptions, NinjatraderLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a new ninjatrader output window logger.
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="options">The ninjatrader output window logger options.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddOutputWindowLogger(this ILoggingBuilder builder, Action<NinjatraderLoggerOptions> options)
        {
            builder.AddOutputWindowLogger();
            builder.Services.Configure(options);

            return builder;
        }

        internal static ILoggingBuilder AddNinjatraderLoggerWithFormatter<TOptions>(this ILoggingBuilder builder, string name, Action<TOptions> configure)
            where TOptions : NinjatraderLoggerFormatterOptions
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddNinjatraderFormatterWithName(name);
            builder.Services.Configure(configure);

            return builder;
        }
        private static ILoggingBuilder AddNinjatraderFormatterWithName(this ILoggingBuilder builder, string name) =>
            builder.AddOutputWindowLogger((NinjatraderLoggerOptions options) => options.FormatterName = name);

    }
}
