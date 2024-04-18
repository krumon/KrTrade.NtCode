using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class IndicatorCollection : BarUpdateServiceCollection<IIndicatorService, NinjascriptServiceInfo>
    {
        public IndicatorCollection(IBarsService barsService) : base(barsService)
        {
        }

        public IndicatorCollection(IBarsService barsService, IEnumerable<IIndicatorService> elements) : base(barsService, elements)
        {
        }

        public IndicatorCollection(IBarsService barsService, int capacity) : base(barsService, capacity)
        {
        }

        public override void Add<TOptions>(NinjascriptServiceInfo elementInfo, TOptions elementOptions)
        {
            throw new System.NotImplementedException();
        }
    }
}
