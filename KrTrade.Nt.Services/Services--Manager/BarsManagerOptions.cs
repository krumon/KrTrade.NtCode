using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services
{
    public class BarsManagerOptions : NinjascriptServiceOptions
    {

        // BarsCache options
        public int DefaultCachesCapacity {  get; set; } = Core.Series.Series.DEFAULT_CAPACITY;
        public int DefaultRemovedCachesCapacity {  get; set; } = Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY;
        
        //public CacheServiceOptions CacheServiceOptions { get; set; } = new CacheServiceOptions();

    }
}
