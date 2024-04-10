using System;

namespace KrTrade.Nt.Services
{
    public class SeriesServiceCollection<TSeries> : BarUpdateServiceCollection<ISeriesService<TSeries>, SeriesServiceCollectionOptions>
    {

        public SeriesServiceCollection(IBarsService barsService) : base(barsService)
        {
        }

        public SeriesServiceCollection(IBarsService barsService, Action<SeriesServiceCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public SeriesServiceCollection(IBarsService barsService, SeriesServiceCollectionOptions options) : base(barsService, options)
        {
        }

        public override string Key => throw new NotImplementedException();
    }
}
