using System;

namespace KrTrade.Nt.Services
{
    public class FiltersCollection : BarUpdateServiceCollection<IFilterService, FiltersOptions>
    {
        public FiltersCollection(IBarsService barsService) : base(barsService)
        {
        }

        public FiltersCollection(IBarsService barsService, Action<FiltersOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public FiltersCollection(IBarsService barsService, FiltersOptions options) : base(barsService, options)
        {
        }
    }
}
