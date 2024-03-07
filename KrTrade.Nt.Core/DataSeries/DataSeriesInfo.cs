using KrTrade.Nt.Core.Data;
using NinjaTrader.Data;

namespace KrTrade.Nt.Core.DataSeries
{
    public class DataSeriesInfo
    {

        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        public InstrumentCode InstrumentCode { get; set; }

        /// <summary>
        /// Gets or sets data series trading hours code.
        /// </summary>
        public TradingHoursCode TradringHoursCode { get; set; }

        /// <summary>
        /// Gets or sets data series time frame.
        /// </summary>
        public TimeFrame TimeFrame { get; set; }

        /// <summary>
        /// Gets or sets data series market data type.
        /// </summary>
        public Data.MarketDataType MarketDataType { get; set; }

        //public string ToLogString()
        //{
        //    return "(" + Instrument + "." + PeriodType.ToLogString() + BarsPeriod.Value +")";
        //}

        public override string ToString() => $"{InstrumentCode}({TimeFrame})";
    }
}
