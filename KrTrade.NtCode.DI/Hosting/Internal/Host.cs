using KrTrade.NtCode.FileProviders;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Hosting.Internal
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
