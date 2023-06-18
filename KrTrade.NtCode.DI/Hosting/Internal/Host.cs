using KrTrade.Nt.DI.FileProviders;
using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.DI.Hosting.Internal
{
    /// <summary>
    /// Represents the host.
    /// </summary>
    internal class Host : BaseHost
    {
        public Host(
            IServiceProvider services, 
            IHostEnvironment hostEnvironment, 
            PhysicalFileProvider defaultProvider, 
            IHostApplicationLifetime applicationLifetime, 
            ILogger<BaseHost> logger, 
            IOptions<HostOptions> options) 
            : base(services, hostEnvironment, defaultProvider, applicationLifetime, logger, options)
        {
        }
    }
}
