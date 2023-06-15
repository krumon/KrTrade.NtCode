using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using KrTrade.NtCode.Ninjascripts.Internal;
using System;
using System.Collections.Generic;

namespace KrTrade.NtCode.Ninjascripts
{
    /// <summary>
    /// Produces instances of <see cref="INinjascript"/> classes based on the given providers.
    /// </summary>
    public class NinjascriptFactory : INinjascriptFactory
    {
        private readonly Dictionary<string, Ninjascript> _ninjascripts = new Dictionary<string, Ninjascript>(StringComparer.Ordinal);
        private readonly List<ProviderRegistration> _providerRegistrations = new List<ProviderRegistration>();
        private readonly object _sync = new object();
        private volatile bool _disposed;
        private readonly IDisposable _changeTokenRegistration;
        private NinjascriptFilterOptions _filterOptions;

        /// <summary>
        /// Creates a new <see cref="NinjascriptFactory"/> instance.
        /// </summary>
        public NinjascriptFactory() : this(Array.Empty<INinjascriptProvider>())
        {
        }

        /// <summary>
        /// Creates a new <see cref="NinjascriptFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="INinjascript"/> instances.</param>
        public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers) : this(providers, new StaticFilterOptionsMonitor(new NinjascriptFilterOptions()))
        {
        }

        /// <summary>
        /// Creates a new <see cref="NinjascriptFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        /// <param name="filterOptions">The filter options to use.</param>
        public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers, NinjascriptFilterOptions filterOptions) : this(providers, new StaticFilterOptionsMonitor(filterOptions))
        {
        }

        /// <summary>
        /// Creates a new <see cref="NinjascriptFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="INinjascript"/> instances.</param>
        /// <param name="filterOption">The filter option to use.</param>
        public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers, IOptionsMonitor<NinjascriptFilterOptions> filterOption)
        {
            // Register the indicators providers
            foreach (INinjascriptProvider provider in providers)
                AddProviderRegistration(provider, dispose: false);

            // Register OnChange method and refresh the filters in the registered indicators
            _changeTokenRegistration = filterOption.OnChange(RefreshFilters);
            RefreshFilters(filterOption.CurrentValue);
        }

        /// <summary>
        /// Creates new instance of <see cref="INinjascriptFactory"/> configured using provided <paramref name="configure"/> delegate.
        /// </summary>
        /// <param name="configure">A delegate to configure the <see cref="INinjascriptBuilder"/>.</param>
        /// <returns>The <see cref="INinjascriptFactory"/> that was created.</returns>
        public static INinjascriptFactory Create(Action<INinjascriptBuilder> configure)
        {
            // Create the service collection
            var serviceCollection = new ServiceCollection();
            // Add required services (INinjascriptFactory, INinjascript<> and IConfigureOptions<NinjascriptFiltersOptions>)
            // and create the INinjascriptBuilder with the configured services to add the services to the collection.
            serviceCollection.AddNinjascript(configure);
            // Create a service provider with the required and configure services.
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            // Gets the IIndicatorFactory.
            INinjascriptFactory ninjascriptsFactory = serviceProvider.GetService<INinjascriptFactory>();
            // Returns the disposing logger factory.
            return new DisposingNinjascriptFactory(ninjascriptsFactory, serviceProvider);
        }

        /// <summary>
        /// Creates an <see cref="INinjascript"/> with the given <paramref name="categoryName"/>.
        /// </summary>
        /// <param name="categoryName">The category name for indicators produced by the ninjascript.</param>
        /// <returns>The <see cref="INinjascript"/> that was created.</returns>
        public INinjascript CreateNinjascript(string categoryName)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(NinjascriptFactory));
            }

            lock (_sync)
            {
                if (!_ninjascripts.TryGetValue(categoryName, out Ninjascript ninjascript))
                {
                    ninjascript = new Ninjascript
                    {
                        Ninjascripts = CreateNinjascripts(categoryName),
                    };

                    ninjascript.ConfigureNinjascripts = ApplyFilters(ninjascript.Ninjascripts);

                    _ninjascripts[categoryName] = ninjascript;
                }

                return ninjascript;
            }
        }

        /// <summary>
        /// Adds the given provider to those used in creating <see cref="INinjascript"/> instances.
        /// </summary>
        /// <param name="provider">The <see cref="INinjascriptFactory"/> to add.</param>
        public void AddProvider(INinjascriptProvider provider)
        {
            if (CheckDisposed())
                throw new ObjectDisposedException(nameof(NinjascriptFactory));

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            lock (_sync)
            {
                AddProviderRegistration(provider, dispose: true);

                foreach (KeyValuePair<string, Ninjascript> existingNinjascript in _ninjascripts)
                {
                    Ninjascript ninjascript = existingNinjascript.Value;
                    NinjascriptInfo[] ninjascriptInformation = ninjascript.Ninjascripts;

                    int newIndicatorIndex = ninjascriptInformation.Length;
                    Array.Resize(ref ninjascriptInformation, ninjascriptInformation.Length + 1);
                    ninjascriptInformation[newIndicatorIndex] = new NinjascriptInfo(provider, existingNinjascript.Key);

                    ninjascript.Ninjascripts = ninjascriptInformation;
                    ninjascript.ConfigureNinjascripts = ApplyFilters(ninjascript.Ninjascripts);
                }
            }
        }

        private void RefreshFilters(NinjascriptFilterOptions filterOptions)
        {
            lock (_sync)
            {
                _filterOptions = filterOptions;
                foreach (KeyValuePair<string, Ninjascript> regiteredIndicator in _ninjascripts)
                {
                    Ninjascript ninjascript = regiteredIndicator.Value;
                    ninjascript.ConfigureNinjascripts = ApplyFilters(ninjascript.Ninjascripts);
                }
            }
        }

        private void RefreshFilters(IConfigureOptions<NinjascriptFilterOptions> filterOptions)
        {
            if (_filterOptions == null) _filterOptions = new NinjascriptFilterOptions();
            lock (_sync)
            {
                filterOptions.Configure(_filterOptions);
                foreach (KeyValuePair<string, Ninjascript> registeredNinjascript in _ninjascripts)
                {
                    Ninjascript ninjascript = registeredNinjascript.Value;
                    ninjascript.ConfigureNinjascripts = ApplyFilters(ninjascript.Ninjascripts);
                }
            }
        }

        private void AddProviderRegistration(INinjascriptProvider provider, bool dispose)
        {
            _providerRegistrations.Add(new ProviderRegistration
            {
                Provider = provider,
                ShouldDispose = dispose
            });
        }

        private NinjascriptInfo[] CreateNinjascripts(string indicatorName)
        {
            var ninjascripts = new NinjascriptInfo[_providerRegistrations.Count];
            for (int i = 0; i < _providerRegistrations.Count; i++)
            {
                ninjascripts[i] = new NinjascriptInfo(_providerRegistrations[i].Provider, indicatorName);
            }
            return ninjascripts;
        }

        private NinjascriptConfig[] ApplyFilters(NinjascriptInfo[] ninjascripts)
        {
            var ninjascriptsConfig = new List<NinjascriptConfig>();

            foreach (NinjascriptInfo ninjascriptInformation in ninjascripts)
            {
                NinjascriptRuleSelector.Select(_filterOptions,
                    ninjascriptInformation.ProviderType,
                    ninjascriptInformation.Category,
                    out NinjascriptLevel? minLevel,
                    out Func<string, string, NinjascriptLevel, bool> filter);

                if (minLevel != null && minLevel > NinjascriptLevel.Real)
                {
                    continue;
                }

                ninjascriptsConfig.Add(new NinjascriptConfig(ninjascriptInformation.Ninjascript, ninjascriptInformation.Category, ninjascriptInformation.ProviderType.FullName, minLevel, filter));

            }

            return ninjascriptsConfig.ToArray();
        }

        /// <summary>
        /// Check if the factory has been disposed.
        /// </summary>
        /// <returns>True when <see cref="Dispose()"/> as been called</returns>
        protected virtual bool CheckDisposed() => _disposed;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _changeTokenRegistration?.Dispose();

                foreach (ProviderRegistration registration in _providerRegistrations)
                {
                    try
                    {
                        if (registration.ShouldDispose)
                        {
                            registration.Provider.Dispose();
                        }
                    }
                    catch
                    {
                        // Swallow exceptions on dispose
                    }
                }
            }
        }

        private struct ProviderRegistration
        {
            public INinjascriptProvider Provider;
            public bool ShouldDispose;
        }
        private sealed class DisposingNinjascriptFactory : INinjascriptFactory
        {
            private readonly INinjascriptFactory _indicatorFactory;

            private readonly ServiceProvider _serviceProvider;

            public DisposingNinjascriptFactory(INinjascriptFactory ninjascriptsFactory, ServiceProvider serviceProvider)
            {
                _indicatorFactory = ninjascriptsFactory;
                _serviceProvider = serviceProvider;
            }

            public void Dispose()
            {
                _serviceProvider.Dispose();
            }

            public INinjascript CreateNinjascript(string categoryName)
            {
                return _indicatorFactory.CreateNinjascript(categoryName);
            }

            public void AddProvider(INinjascriptProvider provider)
            {
                _indicatorFactory.AddProvider(provider);
            }
        }
    }
}
