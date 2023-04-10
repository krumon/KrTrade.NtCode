using Nt.Core.DependencyInjection;
using Nt.Core.FileProviders;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;

namespace Nt.Core.Hosting
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
