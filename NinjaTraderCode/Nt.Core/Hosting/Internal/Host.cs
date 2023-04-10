using Nt.Core.FileProviders;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;

namespace Nt.Core.Hosting.Internal
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
