using KrTrade.NtCode.DependencyInjection;
using System;

namespace KrTrade.NtCode.Hosting.Internal
{
    public interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
