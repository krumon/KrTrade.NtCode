using Nt.Core.DependencyInjection;
using Nt.Core.FileProviders;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;

namespace Nt.Scripts.Hosting
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
