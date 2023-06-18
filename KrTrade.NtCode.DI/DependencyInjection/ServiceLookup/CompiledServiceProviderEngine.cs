using System;

namespace KrTrade.Nt.DI.DependencyInjection.ServiceLookup
{
    internal abstract class CompiledServiceProviderEngine : ServiceProviderEngine
    {
#if IL_EMIT
                public ILEmitResolverBuilder ResolverBuilder { get; }
#else
        //public ExpressionResolverBuilder ResolverBuilder { get; }
#endif

        public CompiledServiceProviderEngine(ServiceProvider provider)
        {
            //ResolverBuilder = new(provider);
        }

        //public override Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite) => ResolverBuilder.Build(callSite);
    }
}
