using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public abstract class BaseStats : BaseService<IServiceInfo, IServiceOptions>
    {
        public double Avg {  get; set; }
        public double DevStd {  get; set; }
        public double Median {  get; set; }
        public double Max {  get; set; }
        public double Min {  get; set; }
        //public double[] NormalizeValues {  get; set; }
        //public double[] NormalizeStdValues {  get; set; }

        protected BaseStats(NinjaScriptBase ninjascript) : base(ninjascript,null,null,null)
        {
        }

    }
}
