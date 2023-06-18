﻿using KrTrade.NtCode.Configuration;
using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Logging.Configuration;
using KrTrade.NtCode.Logging.Internal;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Extension methods for setting up logging services in an <see cref="ILoggingBuilder" />.
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// Sets a minimum <see cref="LogLevel"/> requirement for log messages to be logged.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to set the minimum level on.</param>
        /// <param name="level">The <see cref="LogLevel"/> to set as the minimum.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder SetMinimumLevel(this ILoggingBuilder builder, LogLevel level)
        {
            builder.Services.Add(ServiceDescriptor.Singleton<IConfigureOptions<LoggerFilterOptions>>(
                new DefaultLoggerLevelConfigureOptions(level)));
            return builder;
        }

        /// <summary>
        /// Adds the given <see cref="ILoggerProvider"/> to the <see cref="ILoggingBuilder"/>
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to add the <paramref name="provider"/> to.</param>
        /// <param name="provider">The <see cref="ILoggerProvider"/> to add to the <paramref name="builder"/>.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder AddProvider(this ILoggingBuilder builder, ILoggerProvider provider)
        {
            builder.Services.AddSingleton(provider);
            return builder;
        }

        /// <summary>
        /// Removes all <see cref="ILoggerProvider"/>s from <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to remove <see cref="ILoggerProvider"/>s from.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder ClearProviders(this ILoggingBuilder builder)
        {
            builder.Services.RemoveAll<ILoggerProvider>();
            return builder;
        }

        /// <summary>
        /// Configure the <paramref name="builder"/> with the <see cref="LoggerFactoryOptions"/>.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to be configured with <see cref="LoggerFactoryOptions"/></param>
        /// <param name="action">The action used to configure the logger factory</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder Configure(this ILoggingBuilder builder, Action<LoggerFactoryOptions> action)
        {
            builder.Services.Configure(action);
            return builder;
        }

        /// <summary>
        /// Configures <see cref="LoggerFilterOptions" /> from an instance of <see cref="IConfiguration" />.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to add.</param>
        /// <returns>The builder.</returns>
        public static ILoggingBuilder AddConfiguration(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();

            builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>>(new LoggerFilterConfigureOptions(configuration));
            builder.Services.AddSingleton<IOptionsChangeTokenSource<LoggerFilterOptions>>(new ConfigurationChangeTokenSource<LoggerFilterOptions>(configuration));

            builder.Services.AddSingleton(new LoggingConfiguration(configuration));

            return builder;
        }
    }
}