using System;

namespace KrTrade.Nt.Services
{
    public class StatsCollection : BarUpdateServiceCollection<IStatsService, StatsCollectionOptions>
    {
        public StatsCollection(IBarsService barsService) : base(barsService)
        {
        }

        public StatsCollection(IBarsService barsService, Action<StatsCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public StatsCollection(IBarsService barsService, StatsCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
