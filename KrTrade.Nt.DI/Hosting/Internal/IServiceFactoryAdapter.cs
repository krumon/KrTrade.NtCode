using KrTrade.Nt.DI.DependencyInjection;
using System;

namespace KrTrade.Nt.DI.Hosting.Internal
{
    public interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
