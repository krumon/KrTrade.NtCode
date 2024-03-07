using System;

namespace KrTrade.Nt.Services
{
    public class FiltersCollection : BarUpdateServiceCollection<IFiltersService, FiltersCollectionOptions>
    {
        public FiltersCollection(IBarsMaster barsService) : base(barsService)
        {
        }

        public FiltersCollection(IBarsMaster barsService, Action<FiltersCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public FiltersCollection(IBarsMaster barsService, FiltersCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
