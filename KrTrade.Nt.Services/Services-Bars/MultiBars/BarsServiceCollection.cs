using System;

namespace KrTrade.Nt.Services
{
    public class BarsServiceCollection : BarUpdateServiceCollection<IBarsService, BarsServiceCollectionOptions>
    {
        public BarsServiceCollection(IBarsManager barsService) : base(barsService)
        {
        }

        public BarsServiceCollection(IBarsManager barsService, Action<BarsServiceCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public BarsServiceCollection(IBarsManager barsService, BarsServiceCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
