using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class FiltersCollection : BarUpdateServiceCollection<IFiltersService<FiltersOptions>>
    {
        public FiltersCollection(IBarsService barsService) : base(barsService)
        {
        }

        public FiltersCollection(IBarsService barsService, IEnumerable<IFiltersService<FiltersOptions>> elements) : base(barsService, elements)
        {
        }

        public FiltersCollection(IBarsService barsService, int capacity) : base(barsService, capacity)
        {
        }
    }
}
