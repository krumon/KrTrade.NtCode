using System;

namespace KrTrade.Nt.Services
{
    public class FiltersCollection : BarUpdateServiceCollection<IFiltersService, FiltersCollectionOptions>
    {
        public FiltersCollection(IBarsManager barsService) : base(barsService)
        {
        }

        public FiltersCollection(IBarsManager barsService, Action<FiltersCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public FiltersCollection(IBarsManager barsService, FiltersCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
