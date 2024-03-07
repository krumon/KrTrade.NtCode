using System;

namespace KrTrade.Nt.Services
{
    internal abstract class ServiceProviderEngine
    {
        public abstract Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite);
    }
}
