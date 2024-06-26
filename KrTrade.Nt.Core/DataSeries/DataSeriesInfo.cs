using KrTrade.Nt.Core.Information;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.DataSeries
{
    public class DataSeriesInfo : Info
    {

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
        /// Create <see cref="DataSeriesInfo"/> default instance.
        /// </summary>
        public DataSeriesInfo()
        {
        }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> instance with specified properties.
        /// </summary>
        /// <param name="instrumentName">The date series instrument name.</param>
        /// <param name="tradingHoursName">The data series trading hours name.</param>
        /// <param name="barsPeriod">The data series bars period.</param>
        public DataSeriesInfo(string instrumentName, string tradingHoursName, BarsPeriod barsPeriod)
        {
            InstrumentName = instrumentName;
            TradingHoursName = tradingHoursName;
            BarsPeriod = barsPeriod;
        }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> of the ninjascript promary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public DataSeriesInfo(NinjaScriptBase ninjascript)
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
        protected string ToUniqueString() => $"{InstrumentName},{BarsPeriod},{BarsPeriod.MarketDataType},{TradingHoursName}";

        ///// <summary>
        ///// Converts tha actual object to <see cref="DataSeriesInfo"/> object.
        ///// </summary>
        ///// <returns>The <see cref="DataSeriesInfo"/> object.</returns>
        //public DataSeriesInfo ToDataSeriesInfo()
        //{
        //    return new DataSeriesInfo
        //    {
        //        InstrumentCode = InstrumentName.ToInstrumentCode(),
        //        TimeFrame = BarsPeriod.ToTimeFrame(),
        //        TradingHoursCode = TradingHoursName.ToTradingHoursCode(),
        //        MarketDataType = BarsPeriod.MarketDataType.ToKrMarketDataType(),
        //    };
        //}
    }
}
