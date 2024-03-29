using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.DataSeries
{
    public class NinjascriptDataSeriesOptions
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

        /// <summary>
        /// Create <see cref="NinjascriptDataSeriesOptions"/> default instance.
        /// </summary>
        public NinjascriptDataSeriesOptions()
        {
        }

        /// <summary>
        /// Create <see cref="NinjascriptDataSeriesOptions"/> instance with specified properties.
        /// </summary>
        /// <param name="instrumentName">The date series instrument name.</param>
        /// <param name="tradingHoursName">The data series trading hours name.</param>
        /// <param name="barsPeriod">The data series bars period.</param>
        public NinjascriptDataSeriesOptions(string instrumentName, string tradingHoursName, BarsPeriod barsPeriod)
        {
            InstrumentName = instrumentName;
            TradingHoursName = tradingHoursName;
            BarsPeriod = barsPeriod;
        }

        /// <summary>
        /// Create <see cref="NinjascriptDataSeriesOptions"/> of the ninjascript promary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public NinjascriptDataSeriesOptions(NinjaScriptBase ninjascript)
        {
            InstrumentName = ninjascript.BarsArray[0].Instrument.MasterInstrument.Name;
            TradingHoursName = ninjascript.BarsArray[0].TradingHours.Name;
            BarsPeriod = ninjascript.BarsPeriods[0];
        }

        public override string ToString() => $"{InstrumentName},{BarsPeriod}";

        /// <summary>
        /// Converts the actual object to long string.
        /// </summary>
        /// <returns>Long string thats represents the actual object.</returns>
        public string ToLongString() => $"{InstrumentName},{BarsPeriod},{BarsPeriod.MarketDataType},{TradingHoursName}";

        /// <summary>
        /// Converts tha actual object to <see cref="DataSeriesOptions"/> object.
        /// </summary>
        /// <returns>The <see cref="DataSeriesOptions"/> object.</returns>
        public DataSeriesOptions ToDataSeriesInfo()
        {
            return new DataSeriesOptions
            {
                InstrumentCode = InstrumentName.ToInstrumentCode(),
                TimeFrame = BarsPeriod.ToTimeFrame(),
                TradingHoursCode = TradingHoursName.ToTradingHoursCode(),
                MarketDataType = BarsPeriod.MarketDataType.ToKrMarketDataType(),
            };
        }
    }
}
