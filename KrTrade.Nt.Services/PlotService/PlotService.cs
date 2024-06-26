using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Implemets methods to plot in the ninjatrader charts.
    /// </summary>
    public class PlotService : BaseService<IServiceInfo,IServiceOptions>
    {
        public PlotService(NinjaScriptBase ninjascript, IPrintService printService, IServiceInfo info, IServiceOptions options) : base(ninjascript, printService, info, options)
        {
        }

        public string ToString(int tabOrder)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetDescriptionString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetHeaderString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParentString()
        {
            throw new System.NotImplementedException();
        }

        protected override ServiceType ToElementType()
        {
            throw new System.NotImplementedException();
        }

        protected override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }

        protected override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;
        }
    }
}
