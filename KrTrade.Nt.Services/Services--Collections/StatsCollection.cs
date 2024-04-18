//using KrTrade.Nt.Core.Elements;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Services
//{
//    public class StatsCollection<TInfo> : BarUpdateServiceCollection<IStatsService,TInfo>
//        where TInfo : StatsInfo
//    {
//        public StatsCollection(IBarsService barsService) : base(barsService)
//        {
//        }

//        public StatsCollection(IBarsService barsService, IEnumerable<IStatsService> elements) : base(barsService, elements)
//        {
//        }

//        public StatsCollection(IBarsService barsService, int capacity) : base(barsService, capacity)
//        {
//        }

//        public override void Add<TOptions>(TInfo elementInfo, TOptions elementOptions)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
