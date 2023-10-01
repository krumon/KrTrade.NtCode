using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.FileProviders;
using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.DI.Hosting
{
    public class HostBuilder : BaseHostBuilder<IHost>
    {
        public override IHost GetHostImplementation(IServiceProvider serviceProvider, ServiceCollection serviceCollection, PhysicalFileProvider defaultFileProvider)
        {
            return new Internal.Host(
                serviceProvider
                , serviceProvider.GetRequiredService<IHostEnvironment>()
                , defaultFileProvider
                , serviceProvider.GetRequiredService<IHostApplicationLifetime>()
                , serviceProvider.GetRequiredService<ILogger<Internal.Host>>()
                //, _services.GetRequiredService<IHostLifetime>()
                , serviceProvider.GetRequiredService<IOptions<HostOptions>>()
                );
        }
    }
}
