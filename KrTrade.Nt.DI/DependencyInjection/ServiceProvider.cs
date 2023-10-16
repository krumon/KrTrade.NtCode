using KrTrade.Nt.Core.Core;
using KrTrade.Nt.DI.DependencyInjection.ServiceLookup;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrTrade.Nt.DI.DependencyInjection
{
    /// <summary>
    /// Default <see cref="IServiceProvider"/>.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider, IDisposable
    {

        #region Members

        // Object to validate the services.
        private readonly CallSiteValidator _callSiteValidator;
        // Check the service provider is disposed.
        private bool _disposed;
        // Internal for testing... not use in this moment.
        //internal ServiceProviderEngine _engine;
        // Collection of realized scope services.
        private ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>> _realizedScopeServices;
        // The factory of the calls.
        internal CallSiteFactory CallSiteFactory { get; }
        // The scope engine to create services.
        internal ServiceProviderEngineScope Root { get; }
        // Delegate to create the service accesor by the service provider engine scope.
        private readonly Func<Type, Func<ServiceProviderEngineScope, object>> _createServiceAccessor;
        // Verify if the app sets the library name 'VerifyOpenGenericServiceTrimmability'.
        internal static bool VerifyOpenGenericServiceTrimmability { get; } =
            AppContext.TryGetSwitch("Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability", out bool verifyOpenGenerics) && verifyOpenGenerics;

        #endregion

        #region Constructor

        public ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors) : this(serviceDescriptors, ServiceProviderOptions.Default)
        {
        }

        public ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // note that Root needs to be set before calling GetEngine(), because the engine may need to access Root
            Root = new ServiceProviderEngineScope(this, isRootScope: true);
            //_engine = GetEngine();
            // Sets the delegate to create the service accesor.
            _createServiceAccessor = CreateServiceAccessor;
            //// Sets the delegate method to create the services.
            //_createService = CreateService;
            // Initialize the realized scope services collection.
            _realizedScopeServices = new ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>>();
            //// Initilize the realized services collection.
            //_realizedServices = new ConcurrentDictionary<Type, object>();
            // Initialize the call site factory.
            CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            // The list of built in services that aren't part of the list of service descriptors
            // keep this in sync with CallSiteFactory.IsService
            CallSiteFactory.Add(typeof(IServiceProvider), new ServiceProviderCallSite());
            CallSiteFactory.Add(typeof(IServiceScopeFactory), new ConstantCallSite(typeof(IServiceScopeFactory), Root));
            CallSiteFactory.Add(typeof(IServiceProviderIsService), new ConstantCallSite(typeof(IServiceProviderIsService), CallSiteFactory));

            if (options.ValidateScopes)
            {
                _callSiteValidator = new CallSiteValidator();
            }

            if (options.ValidateOnBuild)
            {
                List<Exception> exceptions = null;
                foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
                {
                    try
                    {
                        ValidateService(serviceDescriptor);
                    }
                    catch (Exception e)
                    {
                        exceptions = exceptions ?? new List<Exception>();
                        exceptions.Add(e);
                    }
                }

                if (exceptions != null)
                {
                    throw new AggregateException("Some services are not able to be constructed", exceptions.ToArray());
                }
            }

        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>The service that was produced.</returns>
        public object GetService(Type serviceType) => GetService(serviceType, Root);

        internal object GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope)
        {
            if (_disposed)
                ThrowHelper.ThrowObjectDisposedException();

            Func<ServiceProviderEngineScope, object> realizedService = _realizedScopeServices.GetOrAdd(serviceType, _createServiceAccessor);
            OnResolve(serviceType, serviceProviderEngineScope);
            //DependencyInjectionEventSource.Log.ServiceResolved(this, serviceType);
            var result = realizedService.Invoke(serviceProviderEngineScope);
            System.Diagnostics.Debug.Assert(result is null || CallSiteFactory.IsService(serviceType));
            return result;
        }

        internal void ReplaceServiceAccessor(ServiceCallSite callSite, Func<ServiceProviderEngineScope, object> accessor)
        {
            _realizedScopeServices[callSite.ServiceType] = accessor;
        }

        internal IServiceScope CreateScope()
        {
            if (_disposed)
            {
                ThrowHelper.ThrowObjectDisposedException();
            }

            return new ServiceProviderEngineScope(this, isRootScope: false);
        }

        #endregion

        #region Private methods

        private void ValidateService(ServiceDescriptor descriptor)
        {
            if (descriptor.ServiceType.IsGenericType && !descriptor.ServiceType.IsConstructedGenericType)
            {
                return;
            }

            try
            {
                ServiceCallSite callSite = CallSiteFactory.GetCallSite(descriptor, new CallSiteChain());
                if (callSite != null)
                {
                    OnCreate(callSite);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }
        private Func<ServiceProviderEngineScope, object> CreateServiceAccessor(Type serviceType)
        {
            ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
            if (callSite != null)
            {
                //DependencyInjectionEventSource.Log.CallSiteBuilt(this, serviceType, callSite);
                OnCreate(callSite);

                // Optimize singleton case
                if (callSite.Cache.Location == CallSiteResultCacheLocation.Root)
                {
                    object value = CallSiteRuntimeResolver.Instance.Resolve(callSite, Root);
                    return scope => value;
                }
                object value2 = CallSiteRuntimeResolver.Instance.Resolve(callSite, Root);
                return scope => value2;
                //return _engine.RealizeService(callSite);
            }
            return _ => null;
        }
        private void OnCreate(ServiceCallSite callSite)
        {
            _callSiteValidator?.ValidateCallSite(callSite);
        }
        private void OnResolve(Type serviceType, IServiceScope scope)
        {
            _callSiteValidator?.ValidateResolution(serviceType, scope, Root);
        }
//        private ServiceProviderEngine GetEngine()
//        {
//            ServiceProviderEngine engine;

//#if NETFRAMEWORK || NETSTANDARD2_0
//                    engine = new DynamicServiceProviderEngine(this);
//#else
//            if (RuntimeFeature.IsDynamicCodeCompiled)
//            {
//                engine = new DynamicServiceProviderEngine(this);
//            }
//            else
//            {
//                // Don't try to compile Expressions/IL if they are going to get interpreted
//                engine = RuntimeServiceProviderEngine.Instance;
//            }
//#endif
//            return engine;
//        }

        #endregion

        #region Dispose methods

        public void Dispose()
        {
            DisposeCore();
        }
        public ValueTask DisposeAsync()
        {
            DisposeCore();
            return Root.DisposeAsync();
        }
        private void DisposeCore()
        {
            _disposed = true;
        }

        #endregion

    }
}
