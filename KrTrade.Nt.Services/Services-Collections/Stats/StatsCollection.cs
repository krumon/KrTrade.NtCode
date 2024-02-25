using System;

namespace KrTrade.Nt.Services
{
    public class StatsCollection : BarUpdateServiceCollection<IStatisticsService, StatsOptions>
    {
        public StatsCollection(IBarsService barsService) : base(barsService)
        {
        }

        public StatsCollection(IBarsService barsService, Action<StatsOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public StatsCollection(IBarsService barsService, StatsOptions options) : base(barsService, options)
        {
        }
    }
}
