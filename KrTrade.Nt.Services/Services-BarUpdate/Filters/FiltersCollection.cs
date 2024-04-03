using System;

namespace KrTrade.Nt.Services
{
    public class FiltersCollection : BarUpdateServiceCollection<IFiltersService<FiltersOptions>, FiltersCollectionOptions>
    {
        public FiltersCollection(IBarsService barsService) : base(barsService)
        {
        }

        public FiltersCollection(IBarsService barsService, Action<FiltersCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public FiltersCollection(IBarsService barsService, FiltersCollectionOptions options) : base(barsService, options)
        {
        }

        public override string Key => throw new NotImplementedException();
    }
}
