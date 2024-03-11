using System;

namespace KrTrade.Nt.Services
{
    public class IndicatorsCollection : BarUpdateServiceCollection<IIndicatorService, IndicatorsOptions>
    {
        public IndicatorsCollection(IBarsManager barsService) : base(barsService)
        {
        }

        public IndicatorsCollection(IBarsManager barsService, Action<IndicatorsOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public IndicatorsCollection(IBarsManager barsService, IndicatorsOptions options) : base(barsService, options)
        {
        }
    }
}
