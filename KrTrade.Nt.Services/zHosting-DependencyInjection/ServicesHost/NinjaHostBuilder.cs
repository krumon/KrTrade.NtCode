using KrTrade.Nt.Core;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.IO;

namespace KrTrade.Nt.Services
{
    public class NinjaHostBuilder : INinjaHostBuilder
    {

        private bool _hostBuilt;
        private List<Action<IServiceCollection>> _configureServicesActions = new List<Action<IServiceCollection>>();
        private IServiceProvider _services;

        //private PhysicalFileProvider _defaultProvider;
        //private Func<IServiceProvider, IServiceCollection, PhysicalFileProvider, T> _hostImplementation;

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public INinjaHostBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }


        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjaHost"/></returns>
        /// <exception cref="InvalidOperationException">The host can only be built once.</exception>
        public INinjaHost Build(NinjaScriptBase ninjascript)
        {
            if (_hostBuilt)
                throw new InvalidOperationException("The host can only be built once.");
            _hostBuilt = true;

            var services = new ServiceCollection();

            foreach (Action<IServiceCollection> configureServicesAction in _configureServicesActions)
                configureServicesAction(services);

            //_services = _serviceProviderFactory.CreateServiceProvider(containerBuilder);
            _services = new ServiceProvider(services);

            if (_services == null)
                throw new InvalidOperationException("Null IServiceProvider");

            // Obtenerlo con ServiceProvider
            PrintService printService = new PrintService(ninjascript);

            var script = new NinjaHost(_services, ninjascript, printService);

            return script;
        }

        private string ResolveContentRootPath(string contentRootPath, string basePath)
        {
            if (string.IsNullOrEmpty(contentRootPath))
            {
                return basePath;
            }
            if (Path.IsPathRooted(contentRootPath))
            {
                return contentRootPath;
            }
            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
        }

    }
}
