using System;

namespace KrTrade.Nt.Services
{
    public class DataSeriesCollection : BarUpdateServiceCollection<IDataSeriesService, DataSeriesCollectionOptions>
    {
        public DataSeriesCollection(IBarsService barsService) : base(barsService)
        {
        }

        public DataSeriesCollection(IBarsService barsService, Action<DataSeriesCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public DataSeriesCollection(IBarsService barsService, DataSeriesCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
