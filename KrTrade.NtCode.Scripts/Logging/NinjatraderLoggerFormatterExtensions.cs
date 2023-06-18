﻿using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    [UnsupportedOSPlatform("browser")]
    public static class NinjatraderLoggerFormatterExtensions
    {
        internal const string TrimmingRequiresUnreferencedCodeMessage = "TOptions's dependent types may have their members trimmed. Ensure all required members are preserved.";

        /// <summary>
        /// Adds a custom file logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddNinjatraderLoggerFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder)
            where TFormatter : NinjatraderLoggerFormatter
            where TOptions : NinjatraderLoggerFormatterOptions
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<NinjatraderLoggerFormatter, TFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>, NinjatraderLoggerFormatterConfigureOptions<TFormatter, TOptions>>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, NinjatraderLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions>>());

            return builder;
        }

        /// <summary>
        /// Adds a custom file logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure options 'TOptions' for custom formatter 'TFormatter'.</param>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddFileFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder, Action<TOptions> configure)
            where TOptions : NinjatraderLoggerFormatterOptions
            where TFormatter : NinjatraderLoggerFormatter
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddNinjatraderLoggerFormatter<TFormatter, TOptions>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}