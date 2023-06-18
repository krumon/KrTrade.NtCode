using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.Console;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    [UnsupportedOSPlatform("browser")]
    public static class ConsoleLoggerFormatterExtensions
    {
        internal const string TrimmingRequiresUnreferencedCodeMessage = "TOptions's dependent types may have their members trimmed. Ensure all required members are preserved.";

        /// <summary>
        /// Adds a custom console logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddConsoleFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder)
            where TOptions : ConsoleFormatterOptions
            where TFormatter : ConsoleFormatter
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ConsoleFormatter, TFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>, ConsoleLoggerFormatterConfigureOptions<TFormatter, TOptions>>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, ConsoleLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions>>());

            return builder;
        }

        /// <summary>
        /// Adds a custom console logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure options 'TOptions' for custom formatter 'TFormatter'.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddConsoleFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder, Action<TOptions> configure)
            where TOptions : ConsoleFormatterOptions
            where TFormatter : ConsoleFormatter
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddConsoleFormatter<TFormatter, TOptions>();
            builder.Services.Configure(configure);
            return builder;
        }
    }

}
