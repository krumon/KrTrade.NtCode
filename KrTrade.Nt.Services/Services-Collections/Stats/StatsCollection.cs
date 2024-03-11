using System;

namespace KrTrade.Nt.Services
{
    public class StatsCollection : BarUpdateServiceCollection<IStatsService, StatsCollectionOptions>
    {
        public StatsCollection(IBarsManager barsService) : base(barsService)
        {
        }

        public StatsCollection(IBarsManager barsService, Action<StatsCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public StatsCollection(IBarsManager barsService, StatsCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
