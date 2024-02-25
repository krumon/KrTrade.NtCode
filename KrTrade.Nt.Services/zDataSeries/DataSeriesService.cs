//using KrTrade.Nt.Core.Bars;
//using KrTrade.Nt.Core.Data;
//using KrTrade.Nt.Core.DataSeries;
//using KrTrade.Nt.Core.Extensions;
//using NinjaTrader.Data;
//using NinjaTrader.NinjaScript;
//using System;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Services
//{
//    public class DataSeriesService : BaseNinjascriptService<DataSeriesOptions> //, IDataSeriesService
//    {

//        #region Private members

//        ////// Last and current Bar
//        ////private LastBarService _lastBar;
//        ////private LastBarService _currentBar;
//        //// State
//        //private Dictionary<BarEvent, bool> _barsEvents;
//        //// Logging
//        //private List<string> _logLines;
//        //// Services
//        //private List<IBarUpdateService> _services;
//        //// Execute methods
//        //private List<Action> _updateMethods;
//        //private List<Action> _lastBarRemovedMethods;
//        //private List<Action> _barClosedMethods;
//        //private List<Action> _priceChangedMethods;
//        //private List<Action> _eachTickMethods;
//        //private List<Action> _firstTickMethods;

//        #endregion

//        #region Public properties

//        ///// <summary>
//        ///// Gets or sets the trading hours name of the bars series.
//        ///// </summary>
//        //public string TradingHoursName => this.Options.TradingHours == TradingHoursCode.Default ? Ninjascript.BarsArray[0].Instrument.MasterInstrument.TradingHours.Name : Options.TradingHours.ToString();

//        ///// <summary>
//        ///// Gets or sets the market data type of the bars series.
//        ///// </summary>
//        //public Core.Data.MarketDataType MarketDataType => Options.MarketDataType;

//        ///// <summary>
//        ///// Gets the instument name.
//        ///// </summary>
//        //public string InstrumentName => Options.InstrumentName == InstrumentCode.Default ? Ninjascript.BarsArray[0].Instrument.MasterInstrument.Name : Options.InstrumentName.ToString();

//        ///// <summary>
//        ///// The bars period of the DataSeries.
//        ///// </summary>
//        //public BarsPeriod BarsPeriod => Options.TimeFrame == TimeFrame.Default ? Ninjascript.BarsPeriods[0] : Options.TimeFrame.ToBarsPeriod();

//        ///// <summary>
//        ///// Gets the bars index in the 'NinjaScript.BarsArray'.
//        ///// </summary>
//        //public int Idx { get; private set; }

//        ///// <summary>
//        ///// Gets the last bar of the bars in progress.
//        ///// </summary>
//        //public LastBarService LastBar => _lastBar;

//        ///// <summary>
//        ///// Gets the last bar of the bars in progress.
//        ///// </summary>
//        //public LastBarService CurrentBar => _currentBar;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript) : this(ninjascript, null, InstrumentCode.Default, TimeFrame.Default, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="printService">The <see cref="IPrintService"/> to write in the 'NinjaScript.Output.Window'.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService, InstrumentCode.Default, TimeFrame.Default, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="configureOptions">The actions to configure the <see cref="DataSeriesService"/>.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, IConfigureOptions<DataSeriesOptions> configureOptions) : base(ninjascript, null, configureOptions)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, TimeFrame timeFrame) : this(ninjascript, null, InstrumentCode.Default, timeFrame, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="printService">The <see cref="IPrintService"/> to write in the 'NinjaScript.Output.Window'.</param>
//        /// <param name="configureOptions">The actions to configure the <see cref="DataSeriesService"/>.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<DataSeriesOptions> configureOptions) : base(ninjascript, printService, configureOptions)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="printService">The logging service to write in the NinjaScript output window.</param>
//        /// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, IPrintService printService, TimeFrame timeFrame) : this(ninjascript, printService, InstrumentCode.Default, timeFrame, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
//        {
//        }

//        /// <summary>
//        /// Create <see cref="DataSeriesService"/> instance.
//        /// </summary>
//        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
//        /// <param name="printService">The logging service to write in the NinjaScript output window.</param>
//        /// <param name="instrumentCode">The instrument code. Is necesary to construct a data serie of other instrument.</param>
//        /// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
//        /// <param name="marketDataType">The type of the market data. The posible values are Last, Ask or Bid.</param>
//        /// <param name="tradingHoursCode">The trading hours code. It's necesary to construct a data serie with another trading hours.</param>
//        public DataSeriesService(NinjaScriptBase ninjascript, IPrintService printService, InstrumentCode instrumentCode, TimeFrame timeFrame, Core.Data.MarketDataType marketDataType, TradingHoursCode tradingHoursCode) : base(ninjascript, printService, new ConfigureOptions<DataSeriesOptions>(op => 
//        {
//            op.TimeFrame = timeFrame;
//            op.InstrumentName = instrumentCode;
//            op.MarketDataType = marketDataType;
//            op.TradingHours = tradingHoursCode;
//        })){  }

//        #endregion

//        #region Implementation

//        public override string Name => InstrumentName + "(" + BarsPeriod.ToShortString() + ")";
//        public override string ToLogString() => Name;

//        public IList<IBarUpdateService> Services => throw new NotImplementedException();

//        internal override void Configure(out bool isConfigured)
//        {
//            _barsEvents = new Dictionary<BarEvent, bool>()
//            {
//                [BarEvent.Updated] = false,
//                [BarEvent.Removed] = false,
//                [BarEvent.Closed] = false,
//                [BarEvent.FirstTick] = false,
//                [BarEvent.PriceChanged] = false,
//                [BarEvent.Tick] = false
//            };

//            _logLines = new List<string>();

//            isConfigured = true;

//            OnConfigure();
//        }
//        internal override void DataLoaded(out bool isDataLoaded)
//        {
//            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
//                if (Ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == InstrumentName)
//                    if (Ninjascript.BarsPeriods[i] == BarsPeriod &&
//                        Ninjascript.BarsArray[i].TradingHours.Name == TradingHoursName
//                        )
//                    {
//                        Idx = i;
//                        break;
//                    }

//            if (Idx == -1)
//            {
//                isDataLoaded = false;
//                return;
//            }

//            //_lastBar = new LastBarService(this);
//            //_currentBar = new LastBarService(this);

//            isDataLoaded = true;

//            OnDataLoaded();
//        }
//        public void OnBarUpdate()
//        {
//            if (!Options.IsEnable)
//                return;

//            if (!IsConfigured)
//                LoggingHelpers.ThrowIsNotConfigureException(Name);

//            if (Ninjascript.BarsInProgress < 0 || Ninjascript.BarsInProgress != Idx)
//                return;

//            if (!IsInRunningStates())
//                LoggingHelpers.ThrowOutOfRunningStatesException(Name);

//            Update();
//        }

//        public void Update()
//        {
//            //_currentBar.Update();
//            ExecuteMethods(_updateMethods);

//            //if (_currentBar.IsRemove)
//            //{
//            //    OnLastBarRemoved();
//            //    ExecuteMethods(_lastBarRemovedMethods);
//            //    return;
//            //}
//            //if (_currentBar.IsClose)
//            //{
//            //    OnBarClosed();
//            //    ExecuteMethods(_barClosedMethods);
//            //}
//            //if (_currentBar.IsFirstTick)
//            //{
//            //    OnFirstTick();
//            //    ExecuteMethods(_firstTickMethods);
//            //}
//            //if (_currentBar.IsPriceChange)
//            //{
//            //    OnPriceChanged();
//            //    ExecuteMethods(_priceChangedMethods);
//            //}
//            //if (_currentBar.IsTick)
//            //{
//            //    OnEachTick();
//            //    ExecuteMethods(_eachTickMethods);
//            //}
//        }

//        #endregion

//        #region Public methods


//        /// <summary>
//        /// Add the <see cref="INeedBarsService"/> methods to execute when any changed is produced in the bars.
//        /// </summary>
//        /// <param name="services">The <see cref="INeedBarsService"/> objects.</param>
//        internal void AddServices(params IBarUpdateService[] services)
//        {
//            PrintService?.LogTrace($"{Name} entry in 'AddServices' method.");
//            if (services == null || services.Length == 0)
//                return;

//            for (int i = 0; i < services.Length; i++)
//                AddService(services[i]);
//        }

//        /// <summary>
//        /// Add the <see cref="INeedBarsService"/> method to execute when any changed produced in the bars.
//        /// </summary>
//        /// <param name="service">The <see cref="INeedBarsService"/> objects.</param>
//        internal void AddService(IBarUpdateService service)
//        {
//            PrintService?.LogTrace($"{Name} entry in 'AddService' method.");
//            if (_updateMethods == null)
//                _updateMethods = new List<Action>();
//            _updateMethods.Add(service.Update);
//            if (service is IBarClosedService barClosed)
//            {
//                if (_barClosedMethods == null)
//                    _barClosedMethods = new List<Action>();
//                _barClosedMethods.Add(barClosed.BarClosed);
//            }
//            if (service is IEachTickService barTick)
//            {
//                if (_eachTickMethods == null)
//                    _eachTickMethods = new List<Action>();
//                _eachTickMethods.Add(barTick.EachTick);
//            }
//            if (service is IPriceChangedService priceChanged)
//            {
//                if (_priceChangedMethods == null)
//                    _priceChangedMethods = new List<Action>();
//                _priceChangedMethods.Add(priceChanged.PriceChanged);
//            }
//            if (service is IFirstTickService barFirstTick)
//            {
//                if (_firstTickMethods == null)
//                    _firstTickMethods = new List<Action>();
//                _firstTickMethods.Add(barFirstTick.FirstTick);
//            }
//            if (service is ILastBarRemovedService lastBarRemoved)
//            {
//                if (_lastBarRemovedMethods == null)
//                    _lastBarRemovedMethods = new List<Action>();
//                _lastBarRemovedMethods.Add(lastBarRemoved.LastBarRemoved);
//            }
//        }

//        #endregion

//        #region Virtual methods

//        /// <summary>
//        /// An event driven method which is called whenever a series service is configure.
//        /// </summary>
//        protected virtual void OnConfigure() { }

//        /// <summary>
//        /// An event driven method which is called whenever 'Ninjatrader.NinjaScript' data is loaded and series service is configure.
//        /// </summary>
//        protected virtual void OnDataLoaded() { }

//        /// <summary>
//        /// An event driven method which is called whenever a bar is updated.
//        /// </summary>
//        protected virtual void OnBarUpdated() { }

//        /// <summary>
//        /// An event driven method which is called whenever a last bar is removed.
//        /// </summary>
//        protected virtual void OnLastBarRemoved() { }

//        /// <summary>
//        /// An event driven method which is called whenever a bar is closed.
//        /// </summary>
//        protected virtual void OnBarClosed() { }

//        /// <summary>
//        /// An event driven method which is called whenever price changed.
//        /// </summary>
//        /// <param name="args"></param>
//        protected virtual void OnPriceChanged() { }

//        /// <summary>
//        /// An event driven method which is called whenever a new tick success.
//        /// </summary>
//        /// <param name="args"></param>
//        protected virtual void OnEachTick() { }

//        /// <summary>
//        /// An event driven method which is called whenever a bar first tick success.
//        /// </summary>
//        protected virtual void OnFirstTick() { }

//        #endregion

//        #region Private methods

//        internal bool GetIsClosed(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _barsEvents[BarEvent.Closed];
//        internal bool GetHasNewTick(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick && _barsEvents[BarEvent.Tick];
//        internal bool GetIsRemoved(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _barsEvents[BarEvent.Removed];
//        internal bool GetHasNewPrice(int barsInProgress) => Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _barsEvents[BarEvent.PriceChanged];
//        internal bool GetIsFirstTick(int barsInProgress) => Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _barsEvents[BarEvent.FirstTick];

//        private bool IsBarsInProgress(int barsInProgress) => barsInProgress == Ninjascript.BarsInProgress && barsInProgress == Idx;
//        //private bool IsUpdated() => _lastBar.Idx == _currentBar.Idx;

//        private void SetBarsEvents(bool noneEvent, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
//        {
//            _barsEvents[BarEvent.Updated] = noneEvent;
//            _barsEvents[BarEvent.Removed] = isLastBarRemoved;
//            _barsEvents[BarEvent.Closed] = isBarClosed;
//            _barsEvents[BarEvent.FirstTick] = isFirstTick;
//            _barsEvents[BarEvent.PriceChanged] = isPriceChanged;
//            _barsEvents[BarEvent.Tick] = isNewTick;
//        }
//        private void ResetBarsEvents()
//        {
//            SetBarsEvents(
//                noneEvent: false,
//                isLastBarRemoved: false,
//                isBarClosed: false,
//                isFirstTick: false,
//                isPriceChanged: false,
//                isNewTick: false
//                );
//            _logLines.Clear();
//        }
//        private void SetBarsEventValue(BarEvent barsEvent, bool value)
//        {
//            _barsEvents[barsEvent] = value;

//            if (PrintService == null)
//                return;

//            if (PrintService.IsLogLevelsEnable(Core.Logging.LogLevel.Information) && Options.IsLogEnable)
//                _logLines.Add(barsEvent.ToString());
//        }

//        #endregion
//    }
//}
