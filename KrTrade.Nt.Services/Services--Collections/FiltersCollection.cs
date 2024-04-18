//using KrTrade.Nt.Core.Elements;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Services
//{
//    public class FiltersCollection<TInfo> : BarUpdateServiceCollection<IFiltersService,TInfo>
//        where TInfo : IElementInfo, new()
//    {
//        public FiltersCollection(IBarsService barsService) : base(barsService)
//        {
//        }

//        public FiltersCollection(IBarsService barsService, IEnumerable<IFiltersService> elements) : base(barsService, elements)
//        {
//        }

//        public FiltersCollection(IBarsService barsService, int capacity) : base(barsService, capacity)
//        {
//        }

//        public override void Add<TOptions>(TInfo elementInfo, TOptions elementOptions)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
