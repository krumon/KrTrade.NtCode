using KrTrade.Nt.DI.FileProviders;
using KrTrade.Nt.DI.Hosting;
using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.Scripts.Hosting.Internal
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
