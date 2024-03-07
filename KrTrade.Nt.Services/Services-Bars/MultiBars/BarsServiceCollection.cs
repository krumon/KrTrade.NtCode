using System;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BarUpdateServiceCollection<IBarsService, BarsServiceCollectionOptions>
    {
        public BarsServiceCollection(IBarsMaster barsService) : base(barsService)
        {
        }

        public BarsServiceCollection(IBarsMaster barsService, Action<BarsServiceCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public BarsServiceCollection(IBarsMaster barsService, BarsServiceCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
