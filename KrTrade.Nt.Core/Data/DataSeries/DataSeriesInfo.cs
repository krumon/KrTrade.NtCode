using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Data
{
    public class DataSeriesInfo
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
        /// Create <see cref="DataSeriesInfo"/> default instance.
        /// </summary>
        public DataSeriesInfo()
        {
            InstrumentCode = InstrumentCode.Default;
            TimeFrame = TimeFrame.Default;
            TradingHoursCode = TradingHoursCode.Default;
            MarketDataType = Data.MarketDataType.Last;
        }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> instance with specified properties.
        /// </summary>
        /// <param name="instrumentCode">The instrument unique code.</param>
        /// <param name="tradingHoursCode">The data series <see cref="Data.TradingHoursCode"/>.</param>
        /// <param name="timeFrame">The data series <see cref="Data.TimeFrame"/>.</param>
        /// <param name="marketDataType">The data series <see cref="Data.MarketDataType"/>.</param>
        public DataSeriesInfo(InstrumentCode instrumentCode, TradingHoursCode tradingHoursCode, TimeFrame timeFrame, Data.MarketDataType marketDataType)
        {
            InstrumentCode = instrumentCode;
            TradingHoursCode = tradingHoursCode;
            TimeFrame = timeFrame;
            MarketDataType = marketDataType;
        }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> instance with specified name.
        /// </summary>
        /// <param name="name">the specified pseudoname of the data series.</param>
        public DataSeriesInfo(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> of the ninjascript primary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public DataSeriesInfo(NinjaScriptBase ninjascript) : this(ninjascript,null,0) { }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> of the ninjascript primary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        /// <param name="name">the specified pseudoname of the data series.</param>
        public DataSeriesInfo(NinjaScriptBase ninjascript, string name) : this(ninjascript,name,0) { }

        /// <summary>
        /// Create <see cref="DataSeriesInfo"/> of the ninjascript primary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        /// <param name="name">The specified pseudoname of the data series.</param>
        /// <param name="index">The index of ninjascript data series.</param>
        public DataSeriesInfo(NinjaScriptBase ninjascript, string name, int index)
        {
            Name = name;
            SetNinjascriptValues(ninjascript,index);
        }

        /// <summary>
        /// Converts the actual object to <see cref="NinjascriptDataSeriesInfo"/>
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

        /// <summary>
        /// Sets 'NinjaScript.DataSeries' values in the actual object.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' thats content the values.</param>
        /// <param name="index">The index of 'NinjaScript.DataSeries'.</param>
        public void SetNinjascriptValues(NinjaScriptBase ninjascript, int index)
        {
            InstrumentCode = ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode();
            TradingHoursCode = ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode();
            TimeFrame = ninjascript.BarsPeriods[index].ToTimeFrame();
            MarketDataType = ninjascript.BarsPeriods[index].MarketDataType.ToKrMarketDataType();
        }

        /// <summary>
        /// Gets 'NinjaScript.DataSeries' values in the actual object.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' thats content the values.</param>
        /// <param name="index">The index of 'NinjaScript.DataSeries'.</param>
        public DataSeriesInfo GetNinjascriptValues(NinjaScriptBase ninjascript, int index)
        {
            InstrumentCode = ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode();
            TradingHoursCode = ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode();
            TimeFrame = ninjascript.BarsPeriods[index].ToTimeFrame();
            MarketDataType = ninjascript.BarsPeriods[index].MarketDataType.ToKrMarketDataType();
            return this;
        }

        public static bool operator ==(DataSeriesInfo dataSeries1, DataSeriesInfo dataSeries2) =>
            (dataSeries1 is null && dataSeries2 is null) ||
            (
            !(dataSeries1 is null) && 
            !(dataSeries2 is null) &&
            dataSeries1.InstrumentCode == dataSeries2.InstrumentCode &&
            dataSeries1.TradingHoursCode == dataSeries2.TradingHoursCode &&
            dataSeries1.TimeFrame == dataSeries2.TimeFrame &&
            dataSeries1.MarketDataType == dataSeries2.MarketDataType
            );
        public static bool operator !=(DataSeriesInfo dataSeries1, DataSeriesInfo dataSeries2) => !(dataSeries1 == dataSeries2);

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
        public override bool Equals(object obj) => obj is DataSeriesInfo other && this == other;
        public override int GetHashCode() => ((int)InstrumentCode * 1000) + ((int)TimeFrame * 100) + ((int)TradingHoursCode * 10) + ((int)MarketDataType);

    }
}
