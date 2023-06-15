using Kr.Core.Helpers;
using Nt.Core.FileProviders;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;

namespace Nt.Scripts.Hosting.Internal
{
    /// <summary>
    /// Represents the Ninjatrader host.
    /// </summary>
    internal class NinjaHost : BaseHost, INinjaHost
    {
        //private IMasterScript masterScript;

        public NinjaHost(
            IServiceProvider services,
            IHostEnvironment hostEnvironment,
            PhysicalFileProvider defaultProvider,
            IHostApplicationLifetime applicationLifetime,
            ILogger<NinjaHost> logger,
            IOptions<HostOptions> options)
            : base(services, hostEnvironment, defaultProvider, applicationLifetime, logger, options)
        {
        }

        public void Configure()
        {
            //masterScript = Services.GetService<IMasterScriptFactory>().CreateMasterScript(TypeNameHelper.GetTypeDisplayName(typeof(KrTradeStats), fullName: false, includeGenericParameterNames: false, nestedTypeDelimiter: '.'));
        }

        public void DataLoaded()
        {
        }

        public void OnBarUpdate()
        {
        }

        public void OnMarketData()
        {
        }

        public void OnSessionUpdate()
        {
        }
    }

}
