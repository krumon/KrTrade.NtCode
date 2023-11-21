using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using KrTrade.Nt.Core.Extensions;

namespace KrTrade.Nt.Services
{
    public abstract class BaseBarsService : BaseService
    {

        #region Private members

        // Fields
        private readonly InstrumentCode _instrumentCode;
        private readonly TimeFrame _timeFrame;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the trading hours name of the bars series.
        /// </summary>
        public string TradingHourName { get; private set; }

        /// <summary>
        /// Gets or sets the market data type of the bars series.
        /// </summary>
        public NinjaTrader.Data.MarketDataType MarketDataType { get; private set; }

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public string InstrumentName {  get; private set; }

        /// <summary>
        /// The bars period of the DataSeries.
        /// </summary>
        public BarsPeriod BarsPeriod {  get; private set; }

        #endregion

        #region Constructors

        public BaseBarsService(NinjaScriptBase ninjascript) : this(ninjascript, InstrumentCode.Default, TimeFrame.Default)
        {
        }
        public BaseBarsService(NinjaScriptBase ninjascript, TimeFrame timeFrame) : this(ninjascript,InstrumentCode.Default, timeFrame)
        { 
        }
        public BaseBarsService(NinjaScriptBase ninjascript, InstrumentCode instrumentCode, TimeFrame timeFrame) : this(ninjascript, null,  instrumentCode, timeFrame)
        {
        }
        public BaseBarsService(NinjaScriptBase ninjascript, PrintService printService, InstrumentCode instrumentCode, TimeFrame timeFrame) : base(ninjascript, printService)
        {
            _instrumentCode = instrumentCode;
            _timeFrame = timeFrame;
            InstrumentName = _instrumentCode == InstrumentCode.Default ? Ninjascript.BarsArray[0].Instrument.MasterInstrument.Name : _instrumentCode.ToString();
            BarsPeriod = _timeFrame == TimeFrame.Default ? Ninjascript.BarsPeriods[0] : _timeFrame.ToBarsPeriod();
        }

        #endregion

    }
}
