using KrTrade.Nt.Core.Elements;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Implemets methods to plot in the ninjatrader charts.
    /// </summary>
    public class PlotService : BaseNinjascriptService
    {
        public PlotService(NinjaScriptBase ninjascript, IPrintService printService, IElementInfo info, NinjascriptServiceOptions options) : base(ninjascript, printService, info, options)
        {
        }

        public override string ToLogString()
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
