using Nt.Core.DependencyInjection;
using System;

namespace Nt.Core.Hosting.Internal
{
    public interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
