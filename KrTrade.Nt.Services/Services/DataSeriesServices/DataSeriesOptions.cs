using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the <see cref="DataSeriesCollectionService"/> options. 
    /// </summary>
    public class DataSeriesOptions : NinjascriptServiceOptions
    {

        /// <summary>
        /// Gets or sets the time frama of the data serie.
        /// </summary>
        public TimeFrame TimeFrame {  get; set; }

        /// <summary>
        /// Gets or sets the trading hours name of the bars series.
        /// </summary>
        public TradingHoursCode TradingHours { get; set; }

        /// <summary>
        /// Gets or sets the market data type of the bars series.
        /// </summary>
        public MarketDataType MarketDataType { get; set; }

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public InstrumentCode InstrumentName { get; set; }

    }
}
