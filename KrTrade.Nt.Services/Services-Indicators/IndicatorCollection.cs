using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services
{
    public class IndicatorCollection : BarUpdateServiceCollection<IIndicatorService>
    {
        public IndicatorCollection(IBarsService barsService, string name, BarUpdateServiceCollectionOptions options) : base(barsService, name, options)
        {
        }

        public IndicatorCollection(IBarsService barsService, string name, BarUpdateServiceCollectionOptions options, int capacity) : base(barsService, name, options, capacity)
        {
        }

        public override string Name { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

    }
}
