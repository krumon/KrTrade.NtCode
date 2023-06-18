using System;

namespace KrTrade.NtCode.DependencyInjection.ServiceLookup
{
    internal abstract class ServiceProviderEngine
    {
        public abstract Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite);
    }
}
