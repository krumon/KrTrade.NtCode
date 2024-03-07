using System;

namespace KrTrade.Nt.Services
{
    public class IndicatorsCollection : BarUpdateServiceCollection<IIndicatorService, IndicatorsOptions>
    {
        public IndicatorsCollection(IBarsMaster barsService) : base(barsService)
        {
        }

        public IndicatorsCollection(IBarsMaster barsService, Action<IndicatorsOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public IndicatorsCollection(IBarsMaster barsService, IndicatorsOptions options) : base(barsService, options)
        {
        }
    }
}
