using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Implemets methods to plot in the ninjatrader charts.
    /// </summary>
    public class PlotService : BaseService
    {

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(PlotService);

        #region Constructors

        public PlotService(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        public PlotService(NinjaScriptBase ninjascript, PrintService printService) : base(ninjascript, printService)
        {
        }

        #endregion
    }
}
