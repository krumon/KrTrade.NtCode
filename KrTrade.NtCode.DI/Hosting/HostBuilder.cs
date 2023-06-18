using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.FileProviders;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Hosting
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
