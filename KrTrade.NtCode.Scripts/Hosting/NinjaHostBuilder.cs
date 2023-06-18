using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.FileProviders;
using KrTrade.Nt.DI.Hosting;
using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.Scripts.Hosting
{
    /// <summary>
    /// Default services host builder.
    /// </summary>
    public class NinjaHostBuilder : BaseHostBuilder<INinjaHost>
    {
        public override INinjaHost GetHostImplementation(IServiceProvider serviceProvider, ServiceCollection serviceCollection, PhysicalFileProvider defaultFileProvider)
        {
            return new Internal.NinjaHost(
                serviceProvider
                , serviceProvider.GetRequiredService<IHostEnvironment>()
                , defaultFileProvider
                , serviceProvider.GetRequiredService<IHostApplicationLifetime>()
                , serviceProvider.GetRequiredService<ILogger<Internal.NinjaHost>>()
                , serviceProvider.GetRequiredService<IOptions<HostOptions>>()
                );
        }
    }
}
