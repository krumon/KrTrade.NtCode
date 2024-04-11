using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class StatsCollection : BarUpdateServiceCollection<IStatsService>
    {
        public StatsCollection(IBarsService barsService) : base(barsService)
        {
        }

        public StatsCollection(IBarsService barsService, IEnumerable<IStatsService> elements) : base(barsService, elements)
        {
        }

        public StatsCollection(IBarsService barsService, int capacity) : base(barsService, capacity)
        {
        }
    }
}
