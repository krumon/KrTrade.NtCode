using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
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
