using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public interface INinjaHostBuilder
    {

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureServicesDelegate">The delegate for configuring the <see cref="IServiceCollection"/> that will be used
        /// to construct the <see cref="IServiceProvider"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        INinjaHostBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjaHost"/></returns>
        INinjaHost Build(NinjaScriptBase ninjascript);

    }
}
