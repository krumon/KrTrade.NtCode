using Nt.Core.Hosting;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public static class NinjascriptHostBuilderExtensions
    {

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureNinjascripts">The delegate that configures the <see cref="INinjascriptBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureNinjascript(this IHostBuilder hostBuilder, Action<HostBuilderContext, INinjascriptBuilder> configureNinjascripts)
        {
            return hostBuilder.ConfigureServices((context, services) => services.AddNinjascript(builder => configureNinjascripts(context, builder)));
        }

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureNinjascript">The delegate that configures the <see cref="INinjascriptBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureNinjascript(this IHostBuilder hostBuilder, Action<INinjascriptBuilder> configureNinjascript)
        {
            return hostBuilder.ConfigureServices((context, services) => services.AddNinjascript(builder => configureNinjascript(builder)));
        }

    }
}
