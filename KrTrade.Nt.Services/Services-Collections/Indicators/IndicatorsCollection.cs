using System;

namespace KrTrade.Nt.Services
{
    public class IndicatorsCollection : BarUpdateServiceCollection<IIndicatorService, IndicatorsOptions>
    {
        public IndicatorsCollection(IBarsService barsService) : base(barsService)
        {
        }

        public IndicatorsCollection(IBarsService barsService, Action<IndicatorsOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public IndicatorsCollection(IBarsService barsService, IndicatorsOptions options) : base(barsService, options)
        {
        }
    }
}
