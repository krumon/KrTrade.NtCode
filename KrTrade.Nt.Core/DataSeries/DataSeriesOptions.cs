using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Data;
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
        public bool IsDefault => InstrumentCode == InstrumentCode.Default && TradingHoursCode == TradingHoursCode.Default && TimeFrame == TimeFrame.Default && MarketDataType == Data.MarketDataType.Last;

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
            MarketDataType = Data.MarketDataType.Last;
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
            ninjascript.Print("DataSeriesOptions has been created.");
        }

        /// <summary>
        /// Create <see cref="DataSeriesOptions"/> of the ninjascript promary data series.
        /// </summary>
        /// <param name="name">the specified pseudoname of the data series.</param>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public DataSeriesOptions(string name,NinjaScriptBase ninjascript)
        {
            Name = name;
            SetNinjascriptValues(ninjascript);
            ninjascript.Print("DataSeriesOptions has been created.");
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
            ninjascript.Print("DataSeriesOptions is going to be created...");
            InstrumentCode = ninjascript.BarsArray[0].Instrument.MasterInstrument.Name.ToInstrumentCode();
            ninjascript.Print(string.Format("DataSeriesOptions sets instrument value: {0}.", InstrumentCode.ToString()));
            TradingHoursCode = ninjascript.BarsArray[0].TradingHours.Name.ToTradingHoursCode();
            ninjascript.Print(string.Format("DataSeriesOptions sets trading hours value: {0}.", TradingHoursCode.ToString()));
            TimeFrame = ninjascript.BarsPeriods[0].ToTimeFrame();
            ninjascript.Print(string.Format("DataSeriesOptions sets time frame value: {0}.", TimeFrame.ToString()));
            MarketDataType = ninjascript.BarsPeriods[0].MarketDataType.ToKrMarketDataType();
            ninjascript.Print(string.Format("DataSeriesOptions sets market data type value: {0}.", MarketDataType.ToString()));
        }

        public static bool operator ==(DataSeriesOptions dataSeries1, DataSeriesOptions dataSeries2) =>
            (dataSeries1 is null && dataSeries2 is null) ||
            (
            !(dataSeries1 is null) && 
            !(dataSeries2 is null) &&
            dataSeries1.InstrumentCode == dataSeries2.InstrumentCode &&
            dataSeries1.TradingHoursCode == dataSeries2.TradingHoursCode &&
            dataSeries1.TimeFrame == dataSeries2.TimeFrame &&
            dataSeries1.MarketDataType == dataSeries2.MarketDataType
            );
        public static bool operator !=(DataSeriesOptions dataSeries1, DataSeriesOptions dataSeries2) => !(dataSeries1 == dataSeries2);

        public bool EqualsTo(NinjaScriptBase ninjascript, int index)
        {
            return
            ninjascript != null &&
            index < ninjascript.BarsArray.Length &&
            InstrumentCode == ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode() &&
            TradingHoursCode == ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode() &&
            TimeFrame == ninjascript.BarsArray[index].BarsPeriod.ToTimeFrame() &&
            MarketDataType == ninjascript.BarsArray[index].BarsPeriod.MarketDataType.ToKrMarketDataType()
            ;
        }
        public override bool Equals(object obj) => obj is DataSeriesOptions other && this == other;
        public override int GetHashCode() => ((int)InstrumentCode * 1000) + ((int)TimeFrame * 100) + ((int)TradingHoursCode * 10) + ((int)MarketDataType);

    }
}
