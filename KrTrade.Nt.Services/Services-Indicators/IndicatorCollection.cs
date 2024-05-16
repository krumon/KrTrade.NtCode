using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services
{
    public class IndicatorCollection : BarUpdateServiceCollection<IIndicatorService>
    {
        public IndicatorCollection(IBarsService barsService, NinjascriptServiceInfo info, BarUpdateServiceCollectionOptions options) : base(barsService, info, options)
        {
        }

        public IndicatorCollection(IBarsService barsService, NinjascriptServiceInfo info, BarUpdateServiceCollectionOptions options, int capacity) : base(barsService, info, options, capacity)
        {
        }

        protected override ServiceType GetServiceType()
        {
            throw new System.NotImplementedException();
        }
    }
}
