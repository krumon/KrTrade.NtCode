using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BaseNinjascriptServiceCollection<IBarsService, BarsServiceCollectionOptions>
    {
        public BarsServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, options)
        {
        }

        public BarsServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options)
        {
        }
    }
}
