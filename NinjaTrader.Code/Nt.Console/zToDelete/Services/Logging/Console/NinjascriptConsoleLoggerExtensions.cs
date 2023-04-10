using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace ConsoleApp
{
    /// <summary>
    /// Builder extensions methods for <see cref="NinjascriptConsoleLoggerService"/>.
    /// </summary>
    public static class NinjascriptConsoleLoggerExtensions
    {

        public static ILoggingBuilder AddNinjascriptConsoleLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, NinjascriptConsoleLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <NinjascriptConsoleLoggerConfiguration, NinjascriptConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddNinjascriptConsoleLogger(
            this ILoggingBuilder builder,
            Action<NinjascriptConsoleLoggerConfiguration> configure)
        {
            builder.AddNinjascriptConsoleLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
