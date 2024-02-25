using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;

namespace KrTrade.Nt.Services
{
    public class BarsOptions : NinjascriptServiceOptions
    {

        // BarsSeries options
        public InstrumentCode InstrumentCode { get; set; } = InstrumentCode.Default;
        public TradingHoursCode TradringHoursCode {  get; set; } = TradingHoursCode.Default;
        public TimeFrame TimeFrame { get; set; } = TimeFrame.Default;
        public MarketDataType MarketDataType { get; set; } = MarketDataType.Last;

        // BarsCache options
        public int Capacity {  get; set; } = Cache.DEFAULT_CAPACITY;
        public int RemovedCacheCapacity {  get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;
        
        //public CacheServiceOptions CacheServiceOptions { get; set; } = new CacheServiceOptions();

    }
}
