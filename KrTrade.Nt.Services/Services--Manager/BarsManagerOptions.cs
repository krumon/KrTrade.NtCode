using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public class BarsManagerOptions : NinjascriptServiceOptions
    {

        // BarsCache options
        public int DefaultCachesCapacity {  get; set; } = Cache.DEFAULT_CAPACITY;
        public int DefaultRemovedCachesCapacity {  get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;
        
        //public CacheServiceOptions CacheServiceOptions { get; set; } = new CacheServiceOptions();

    }
}
