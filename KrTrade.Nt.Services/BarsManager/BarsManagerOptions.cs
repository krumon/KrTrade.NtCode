using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services
{
    public class BarsManagerOptions : ServiceOptions
    {

        // Default series options
        public int DefaultCachesCapacity {  get; set; } = Globals.SERIES_DEFAULT_CAPACITY;
        public int DefaultRemovedCachesCapacity {  get; set; } = Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY;

    }
}
