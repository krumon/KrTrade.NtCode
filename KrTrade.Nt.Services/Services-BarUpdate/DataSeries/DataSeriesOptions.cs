using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the <see cref="DataSeriesCollectionService"/> options. 
    /// </summary>
    public class DataSeriesOptions : BarUpdateServiceOptions
    {

        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        public InstrumentCode InstrumentCode { get; set; } = InstrumentCode.Default;

        /// <summary>
        /// Gets or sets data series trading hours code.
        /// </summary>
        public TradingHoursCode TradringHoursCode { get; set; } = TradingHoursCode.Default;
        
        /// <summary>
        /// Gets or sets data series time frame.
        /// </summary>
        public TimeFrame TimeFrame { get; set; } = TimeFrame.Default;
        
        /// <summary>
        /// Gets or sets data series market data type.
        /// </summary>
        public MarketDataType MarketDataType { get; set; } = MarketDataType.Last;

        /// <summary>
        /// Gets or sets the bars cache capacity.
        /// </summary>
        public int Capacity { get; set; } = Cache.DEFAULT_CAPACITY;
        
        /// <summary>
        /// Gets ot set the removed bars cache capacity.
        /// </summary>
        public int RemovedCacheCapacity { get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;

    }
}
