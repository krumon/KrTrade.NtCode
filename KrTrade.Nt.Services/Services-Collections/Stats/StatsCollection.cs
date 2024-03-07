using System;

namespace KrTrade.Nt.Services
{
    public class StatsCollection : BarUpdateServiceCollection<IStatsService, StatsCollectionOptions>
    {
        public StatsCollection(IBarsMaster barsService) : base(barsService)
        {
        }

        public StatsCollection(IBarsMaster barsService, Action<StatsCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public StatsCollection(IBarsMaster barsService, StatsCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
