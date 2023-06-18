using System;

namespace KrTrade.Nt.DI.DependencyInjection.ServiceLookup
{
    internal abstract class ServiceProviderEngine
    {
        public abstract Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite);
    }
}
