using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Data;

namespace KrTrade.Nt.Core.DataSeries
{
    public class DataSeriesInfo
    {

        /// <summary>
        /// Gets the key of data series.
        /// </summary>
        public string Key => ToLongString();

        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        public InstrumentCode InstrumentCode { get; set; }

        /// <summary>
        /// Gets or sets data series trading hours code.
        /// </summary>
        public TradingHoursCode TradingHoursCode { get; set; }

        /// <summary>
        /// Gets or sets data series time frame.
        /// </summary>
        public TimeFrame TimeFrame { get; set; }

        /// <summary>
        /// Gets or sets data series market data type.
        /// </summary>
        public Data.MarketDataType MarketDataType { get; set; }

        public override string ToString() => $"{InstrumentCode},{TimeFrame}";
        public string ToLongString() => $"{InstrumentCode},{TimeFrame},{MarketDataType},{TradingHoursCode}";

        /// <summary>
        /// Converts to <see cref="NinjascriptDataSeriesInfo"/>
        /// </summary>
        /// <returns>The <see cref="NinjascriptDataSeriesInfo"/> object.</returns>
        public NinjascriptDataSeriesInfo ToNinjascriptDataSeriesInfo()
        {
            return new NinjascriptDataSeriesInfo
            {
                InstrumentName = InstrumentCode.ToString(),
                BarsPeriod = TimeFrame.ToBarsPeriod(),
                TradingHoursName = TradingHoursCode.ToName()
            };
        }
    }
}
