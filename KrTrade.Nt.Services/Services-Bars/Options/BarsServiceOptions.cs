using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the <see cref="IBarsService"/> options. 
    /// </summary>
    public class BarsServiceOptions : BarUpdateServiceOptions
    {      
        
        /// <summary>
        /// Gets or sets the bars cache capacity.
        /// </summary>
        public int CacheCapacity { get; set; } = Cache.DEFAULT_CAPACITY;

        /// <summary>
        /// Gets or sets the bars removed cache capacity.
        /// </summary>
        public int RemovedCacheCapacity { get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;

        ///// <summary>
        ///// Gets or sets the Data Series instrument code.
        ///// </summary>
        //public InstrumentCode InstrumentCode { get; set; } = InstrumentCode.Default;

        ///// <summary>
        ///// Gets or sets data series trading hours code.
        ///// </summary>
        //public TradingHoursCode TradringHoursCode { get; set; } = TradingHoursCode.Default;

        ///// <summary>
        ///// Gets or sets data series time frame.
        ///// </summary>
        //public TimeFrame TimeFrame { get; set; } = TimeFrame.Default;

        ///// <summary>
        ///// Gets or sets data series market data type.
        ///// </summary>
        //public MarketDataType MarketDataType { get; set; } = MarketDataType.Last;

    }
}
