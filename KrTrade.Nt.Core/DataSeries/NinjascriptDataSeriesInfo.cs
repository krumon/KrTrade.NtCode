using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Data;

namespace KrTrade.Nt.Core.DataSeries
{
    public class NinjascriptDataSeriesInfo
    {

        /// <summary>
        /// Gets the key of data series.
        /// </summary>
        public string Key => ToLongString();

        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// Gets or sets data series trading hours code.
        /// </summary>
        public string TradingHoursName { get; set; }

        /// <summary>
        /// Gets or sets data series time frame.
        /// </summary>
        public BarsPeriod BarsPeriod { get; set; }

        public override string ToString() => $"{InstrumentName},{BarsPeriod}";
        public string ToLongString() => $"{InstrumentName},{BarsPeriod},{BarsPeriod.MarketDataType},{TradingHoursName}";

        /// <summary>
        /// Converts to <see cref="DataSeriesInfo"/>
        /// </summary>
        /// <returns>The <see cref="DataSeriesInfo"/> enum.</returns>
        public DataSeriesInfo ToDataSeriesInfo()
        {
            return new DataSeriesInfo
            {
                InstrumentCode = InstrumentName.ToInstrumentCode(),
                TimeFrame = BarsPeriod.ToTimeFrame(),
                TradingHoursCode = TradingHoursName.ToTradingHoursCode(),
                MarketDataType = BarsPeriod.MarketDataType.ToKrMarketDataType(),
            };
        }
    }
}
