using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.TradingHours;
using NinjaTrader.NinjaScript;
using System;
using System.Diagnostics;

namespace KrTrade.Nt.Services
{
    public class BarsServiceInfo : ServiceInfo, IBarsServiceInfo
    {

        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        public InstrumentCode InstrumentCode { get; set; }

        /// <summary>
        /// Future contracts expiry month.
        /// </summary>
        public int ContractMonth {  get; set; }

        /// <summary>
        /// Future contracts expiry year.
        /// </summary>
        public int ContractYear { get; set; }

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
        public MarketDataType MarketDataType { get; set; }

        /// <summary>
        /// Indicates the actual object is default instance.
        /// </summary>
        public bool IsDefault => InstrumentCode == InstrumentCode.Default && TradingHoursCode == TradingHoursCode.Default && TimeFrame == TimeFrame.Default && MarketDataType == MarketDataType.Last;

        /// <summary>
        /// Sets 'NinjaScript.DataSeries' values in the actual object.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' thats content the values.</param>
        /// <param name="index">The index of 'NinjaScript.DataSeries'.</param>
        public void SetNinjascriptValues(NinjaScriptBase ninjascript, int index)
        {
            DateTime expiry = ninjascript.BarsArray[index].Instrument.Expiry;
            ContractMonth = expiry.Month;
            ContractYear = expiry.Year;
            InstrumentCode = ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode();
            TradingHoursCode = ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode();
            TimeFrame = ninjascript.BarsPeriods[index].ToTimeFrame();
            MarketDataType = ninjascript.BarsPeriods[index].MarketDataType.ToKrMarketDataType();
        }

        public override string ToString() => $"{InstrumentCode}({TimeFrame})";

        public string ToInstrumentName() => (ContractMonth == 0 || ContractYear == 0) ? InstrumentCode.ToString() : $"{InstrumentCode} {ContractMonth.ToString("00")}-{ContractYear.ToString("00")}";

        /// <summary>
        /// Converts the actual object to long string.
        /// </summary>
        /// <returns>Long string thats represents the actual object.</returns>
        protected override string ToUniqueString() => $"{InstrumentCode}({TimeFrame},{MarketDataType},{TradingHoursCode})";

        /// <summary>
        /// Create <see cref="BarsServiceInfo"/> default instance.
        /// </summary>
        public BarsServiceInfo() : this(ServiceType.BARS)
        {
            InstrumentCode = InstrumentCode.Default;
            TimeFrame = TimeFrame.Default;
            TradingHoursCode = TradingHoursCode.Default;
            MarketDataType = MarketDataType.Last;
        }

        /// <summary>
        /// Create <see cref="BarsServiceInfo"/> instance with specified properties.
        /// </summary>
        /// <param name="instrumentCode">The instrument unique code.</param>
        /// <param name="contractMonth">The month of the contract. Necesary for future instruments.</param>
        /// <param name="contractYear">The year of the contract. Necesary for future instruments.</param>
        /// <param name="tradingHoursCode">The data series <see cref="Core.Data.TradingHoursCode"/>.</param>
        /// <param name="timeFrame">The data series <see cref="Core.Data.TimeFrame"/>.</param>
        /// <param name="marketDataType">The data series <see cref="Core.Data.MarketDataType"/>.</param>
        public BarsServiceInfo(InstrumentCode instrumentCode, int contractMonth, int contractYear, TradingHoursCode tradingHoursCode, TimeFrame timeFrame, MarketDataType marketDataType) : this(ServiceType.BARS)
        {
            ContractMonth = contractMonth;
            ContractYear = contractYear;
            InstrumentCode = instrumentCode;
            TradingHoursCode = tradingHoursCode;
            TimeFrame = timeFrame;
            MarketDataType = marketDataType;
        }

        /// <summary>
        /// Create <see cref="BarsServiceInfo"/> of the ninjascript primary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        public BarsServiceInfo(NinjaScriptBase ninjascript) : this(ninjascript,0) { }

        /// <summary>
        /// Create <see cref="BarsServiceInfo"/> of the ninjascript primary data series.
        /// </summary>
        /// <param name="ninjascript">The 'Ninjatrader.NinjaScript' where the primary series is housed.</param>
        /// <param name="index">The index of ninjascript data series.</param>
        public BarsServiceInfo(NinjaScriptBase ninjascript,int index) : this(ServiceType.BARS)
        {
            SetNinjascriptValues(ninjascript,index);
        }

        protected BarsServiceInfo(ServiceType type)
        {
            Type = type;
        }

        /// <summary>
        /// Converts the actual object to <see cref="DataSeriesInfo"/>
        /// </summary>
        /// <returns>The <see cref="DataSeriesInfo"/> object.</returns>
        internal DataSeriesInfo ToNinjascriptDataSeriesInfo()
        {
            return new DataSeriesInfo
            {
                InstrumentName = (ContractMonth == 0 || ContractYear == 0) ? InstrumentCode.ToString() : $"{InstrumentCode} {ContractMonth.ToString("00")}-{ContractYear.ToString("00")}",
                BarsPeriod = TimeFrame.ToBarsPeriod(),
                TradingHoursName = TradingHoursCode.ToName()
            };
        }

        /// <summary>
        /// Compare the actual object with 'NinjaScript' DataSeries.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool EqualsTo(NinjaScriptBase ninjascript, int index)
        {
            if (ninjascript != null &&
                index < ninjascript.BarsArray.Length &&
                InstrumentCode == ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode() &&
                TradingHoursCode == ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode() &&
                TimeFrame == ninjascript.BarsArray[index].BarsPeriod.ToTimeFrame() &&
                MarketDataType == ninjascript.BarsArray[index].BarsPeriod.MarketDataType.ToKrMarketDataType())
            {
                return true;
            }
            else return false;
            //return
            //ninjascript != null &&
            //index < ninjascript.BarsArray.Length &&
            //InstrumentCode == ninjascript.BarsArray[index].Instrument.MasterInstrument.Name.ToInstrumentCode() &&
            //TradingHoursCode == ninjascript.BarsArray[index].TradingHours.Name.ToTradingHoursCode() &&
            //TimeFrame == ninjascript.BarsArray[index].BarsPeriod.ToTimeFrame() &&
            //MarketDataType == ninjascript.BarsArray[index].BarsPeriod.MarketDataType.ToKrMarketDataType()
            //;
        }
        public override bool Equals(object obj) => obj is BarsServiceInfo other && this == other;
        public override int GetHashCode() => ((int)InstrumentCode * 1000) + ((int)TimeFrame * 100) + ((int)TradingHoursCode * 10) + ((int)MarketDataType);

    }
}
