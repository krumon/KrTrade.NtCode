using System;

namespace KrTrade.Nt.Services
{
    public class IndicatorCollection : BarUpdateServiceCollection<IIndicatorService, IndicatorCollectionOptions>
    {
        public IndicatorCollection(IBarsService barsService) : base(barsService)
        {
        }

        public IndicatorCollection(IBarsService barsService, Action<IndicatorCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public IndicatorCollection(IBarsService barsService, IndicatorCollectionOptions options) : base(barsService, options)
        {
        }

        public override string Key => throw new NotImplementedException();
    }
}
