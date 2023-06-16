using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.FileProviders;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Hosting
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
