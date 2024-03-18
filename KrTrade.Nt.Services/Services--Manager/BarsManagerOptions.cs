using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public class BarsManagerOptions : NinjascriptServiceOptions
    {

        //// BarsSeries options
        //public InstrumentCode InstrumentCode { get; set; } = InstrumentCode.Default;
        //public TradingHoursCode TradringHoursCode {  get; set; } = TradingHoursCode.Default;
        //public TimeFrame TimeFrame { get; set; } = TimeFrame.Default;
        //public MarketDataType MarketDataType { get; set; } = MarketDataType.Last;

        // BarsCache options
        public int DefaultCachesCapacity {  get; set; } = Cache.DEFAULT_CAPACITY;
        public int DefaultRemovedCachesCapacity {  get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;
        
        //public CacheServiceOptions CacheServiceOptions { get; set; } = new CacheServiceOptions();

    }
}
