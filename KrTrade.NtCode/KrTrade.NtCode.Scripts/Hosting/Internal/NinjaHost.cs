using KrTrade.NtCode.FileProviders;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Hosting.Internal
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
