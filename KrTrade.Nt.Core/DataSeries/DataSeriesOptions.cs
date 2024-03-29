using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.DataSeries
{
    public class DataSeriesOptions
    {

        /// <summary>
        /// Gets the key of data series.
        /// </summary>
        public string Key => ToLongString();

        /// <summary>
        /// Gets or sets the pseudoname of the data series.
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// Indicates the actual object is default instance.
        /// </summary>
        public bool IsDefault => InstrumentCode == InstrumentCode.Default && TradingHoursCode == TradingHoursCode.Default && TimeFrame == TimeFrame.Default && MarketDataType == MarketDataType.Last;

        public override string ToString() => $"{InstrumentCode},{TimeFrame}";

        /// <summary>
        /// Converts the actual object to long string.
        /// </summary>
        /// <returns>Long string thats represents the actual object.</returns>
        public string ToLongString() => $"{InstrumentCode},{TimeFrame},{MarketDataType},{TradingHoursCode}";

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> default instance.
        /// </summary>
        public DataSeriesOptions()
        {
            InstrumentCode = InstrumentCode.Default;
            TimeFrame = TimeFrame.Default;
            TradingHoursCode = TradingHoursCode.Default;
            MarketDataType = MarketDataType.Last;
        }

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> instance with specified properties.
        /// </summary>
        /// <param name="instrumentCode">The instrument unique code.</param>
        /// <param name="tradingHoursCode">The data series <see cref="Data.TradingHoursCode"/>.</param>
        /// <param name="timeFrame">The data series <see cref="Data.TimeFrame"/>.</param>
        /// <param name="marketDataType">The data series <see cref="Data.MarketDataType"/>.</param>
        public DataSeriesOptions(InstrumentCode instrumentCode, TradingHoursCode tradingHoursCode, TimeFrame timeFrame, Data.MarketDataType marketDataType)
        {
            InstrumentCode = instrumentCode;
            TradingHoursCode = tradingHoursCode;
            TimeFrame = timeFrame;
            MarketDataType = marketDataType;
        }

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> instance with specified name.
        /// </summary>
        /// <param name="name">the specified pseudoname of the data series.</param>
        public DataSeriesOptions(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> of the ninjascript promary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public DataSeriesOptions(NinjaScriptBase ninjascript)
        {
            SetNinjascriptValues(ninjascript);
        }

        /// <summary>
        /// Converts the actual object to <see cref="NinjascriptDataSeriesOptions"/>
        /// </summary>
        /// <returns>The <see cref="NinjascriptDataSeriesOptions"/> object.</returns>
        public NinjascriptDataSeriesOptions ToNinjascriptDataSeriesInfo()
        {
            return new NinjascriptDataSeriesOptions
            {
                InstrumentName = InstrumentCode.ToString(),
                BarsPeriod = TimeFrame.ToBarsPeriod(),
                TradingHoursName = TradingHoursCode.ToName()
            };
        }

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> of the ninjascript promary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public void SetNinjascriptValues(NinjaScriptBase ninjascript)
        {
            InstrumentCode = ninjascript.BarsArray[0].Instrument.MasterInstrument.Name.ToInstrumentCode();
            TradingHoursCode = ninjascript.BarsArray[0].TradingHours.Name.ToTradingHoursCode();
            TimeFrame = ninjascript.BarsPeriods[0].ToTimeFrame();
            MarketDataType = ninjascript.BarsPeriods[0].MarketDataType.ToKrMarketDataType();
        }

        //public static bool operator ==(DataSeriesOptions dataSeries, NinjaTrader.Data.Bars bars) =>
        //    dataSeries.InstrumentCode == bars.Instrument.MasterInstrument.Name.ToInstrumentCode() &&
        //    dataSeries.TradingHoursCode == bars.TradingHours.Name.ToTradingHoursCode() &&
        //    dataSeries.TimeFrame == bars.BarsPeriod.ToTimeFrame() &&
        //    dataSeries.MarketDataType == bars.BarsPeriod.MarketDataType.ToKrMarketDataType()
        //    ;
        //public static bool operator !=(DataSeriesOptions dataSeries, NinjaTrader.Data.Bars bars) => !(dataSeries == bars);

        public static bool operator ==(DataSeriesOptions dataSeries1, DataSeriesOptions dataSeries2) =>
            (dataSeries1 == null && dataSeries2 == null) ||
            (dataSeries1 != null && 
            dataSeries2 != null &&
            dataSeries1.InstrumentCode == dataSeries2.InstrumentCode &&
            dataSeries1.TradingHoursCode == dataSeries2.TradingHoursCode &&
            dataSeries1.TimeFrame == dataSeries2.TimeFrame &&
            dataSeries1.MarketDataType == dataSeries2.MarketDataType)
            ;
        public static bool operator !=(DataSeriesOptions dataSeries1, DataSeriesOptions dataSeries2) => !(dataSeries1 == dataSeries2);

        public override bool Equals(object obj) => obj is DataSeriesOptions other && this == other;
        public override int GetHashCode() => ((int)InstrumentCode * 1000) + ((int)TimeFrame * 100) + ((int)TradingHoursCode * 10) + ((int)MarketDataType);

    }
}
