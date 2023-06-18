using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.File;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    [UnsupportedOSPlatform("browser")]
    public static class FileLoggerFormatterExtensions
    {
        internal const string TrimmingRequiresUnreferencedCodeMessage = "TOptions's dependent types may have their members trimmed. Ensure all required members are preserved.";

        /// <summary>
        /// Adds a custom file logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddFileFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder)
            where TFormatter : BaseFileFormatter
            where TOptions : BaseFileFormatterOptions
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<BaseFileFormatter, TFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>, FileLoggerFormatterConfigureOptions<TFormatter, TOptions>>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, FileLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions>>());

            return builder;
        }

        /// <summary>
        /// Adds a custom file logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure options 'TOptions' for custom formatter 'TFormatter'.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddFileFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder, Action<TOptions> configure)
            where TOptions : FileFormatterOptions
            where TFormatter : FileFormatter
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddFileFormatter<TFormatter, TOptions>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
