using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the <see cref="IBarsService"/> options. 
    /// </summary>
    public class BarsServiceOptions : NinjascriptServiceOptions //: PrimaryBarsServiceOptions
    {

        /// <summary>
        /// Gets or sets the bars cache capacity.
        /// </summary>
        public int CacheCapacity { get; set; } = Core.Series.Series.DEFAULT_CAPACITY;

        /// <summary>
        /// Gets or sets the bars removed cache capacity.
        /// </summary>
        public int RemovedCacheCapacity { get; set; } = Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY;

        ///// <summary>
        ///// Gets or sets the Data Series instrument code.
        ///// </summary>
        ///// <remarks>
        ///// If the data series is the primary one, setting this option will have no effect on the data series.
        ///// </remarks>
        //public InstrumentCode InstrumentCode { get; set; } = InstrumentCode.Default;

        ///// <summary>
        ///// Gets or sets data series trading hours code.
        ///// </summary>
        ///// <remarks>
        ///// If the data series is the primary one, setting this option will have no effect on the data series.
        ///// </remarks>
        //public TradingHoursCode TradringHoursCode { get; set; } = TradingHoursCode.Default;

        ///// <summary>
        ///// Gets or sets data series time frame.
        ///// </summary>
        ///// <remarks>
        ///// If the data series is the primary one, setting this option will have no effect on the data series.
        ///// </remarks>
        //public TimeFrame TimeFrame { get; set; } = TimeFrame.Default;

        ///// <summary>
        ///// Gets or sets data series market data type.
        ///// </summary>
        ///// <remarks>
        ///// If the data series is the primary one, setting this option will have no effect on the data series.
        ///// </remarks>
        //public MarketDataType MarketDataType { get; set; } = MarketDataType.Last;

        /// <summary>
        /// Create <see cref="BarsServiceOptions"/> default instance.
        /// </summary>
        public BarsServiceOptions()
        {
            //IsPrimaryDataSeries = false;
        }
    }
}
