using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Logging.Debug;

namespace KrTrade.Nt.DI.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="ILoggerFactory"/> class.
    /// </summary>
    public static class DebugLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a debug logger named 'Debug' to the factory.
        /// </summary>
        /// <param name="builder">The extension method argument.</param>
        public static ILoggingBuilder AddDebug(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DebugLoggerProvider>());

            return builder;
        }
    }
}