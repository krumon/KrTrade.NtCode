using KrTrade.Nt.Core.Info;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Implemets methods to plot in the ninjatrader charts.
    /// </summary>
    public class PlotService : BaseNinjascriptService
    {
        public PlotService(NinjaScriptBase ninjascript, IPrintService printService, IServiceInfo info, NinjascriptServiceOptions options) : base(ninjascript, printService, info, options)
        {
        }

        protected override ServiceType GetServiceType() => ServiceType.PLOT;

        public override string ToLogString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetKey()
        {
            throw new System.NotImplementedException();
        }

        internal override void Configure(out bool isConfigured)
        {
            throw new System.NotImplementedException();
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            throw new System.NotImplementedException();
        }
    }
}
