using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Events;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : NinjascriptService<BarsOptions>, IBarsService
    {

        #region Private members

        // Last and current Bar
        private LastBarService _lastBar;
        private LastBarService _currentBar;
        // State
        private Dictionary<BarsEvent, bool> _barsEvents;
        // Logging
        private List<string> _logLines;
        //private BarsOptions _logOptions;
        // Execute methods
        private List<Action> _onBarUpdateMethods;
        private List<Action> _onLastBarRemovedMethods;
        private List<Action> _onBarClosedMethods;
        private List<Action> _onPriceChangedMethods;
        private List<Action> _onEachTickMethods;
        private List<Action> _onFirstTickMethods;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the trading hours name of the bars series.
        /// </summary>
        public string TradingHoursName => this.Options.TradingHours == TradingHoursCode.Default ? Ninjascript.BarsArray[0].Instrument.MasterInstrument.TradingHours.Name : Options.TradingHours.ToString();

        /// <summary>
        /// Gets or sets the market data type of the bars series.
        /// </summary>
        public Core.Data.MarketDataType MarketDataType => Options.MarketDataType;

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public string InstrumentName => Options.InstrumentName == InstrumentCode.Default ? Ninjascript.BarsArray[0].Instrument.MasterInstrument.Name : Options.InstrumentName.ToString();

        /// <summary>
        /// The bars period of the DataSeries.
        /// </summary>
        public BarsPeriod BarsPeriod => Options.TimeFrame == TimeFrame.Default ? Ninjascript.BarsPeriods[0] : Options.TimeFrame.ToBarsPeriod();

        ///// <summary>
        ///// Indicates the service is configured when the NinjaScript data has been loaded.
        ///// </summary>
        //public bool IsConfiguredWhenDataLoaded { get; private set; }

        ///// <summary>
        ///// Indicates the service is configured when the NinjaScript data has NOT been loaded.
        ///// </summary>
        //public bool IsConfigured {get; private set; }

        /// <summary>
        /// Gets the bars index in the 'NinjaScript.BarsArray'.
        /// </summary>
        public int Idx { get; private set; }

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public LastBarService LastBar => _lastBar;

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public LastBarService CurrentBar => _currentBar;

        ///// <summary>
        ///// Gets or sets the instrument point value.
        ///// </summary>
        //public double PointValue => IsInActiveStates() || Idx==0 ? Ninjascript.BarsArray[Idx].Instrument.MasterInstrument.PointValue : -1;

        ///// <summary>
        ///// Gets or sets the instrument tick size.
        ///// </summary>
        //public double TickSize => IsInActiveStates() || Idx==0 ? Ninjascript.BarsArray[Idx].Instrument.MasterInstrument.TickSize : -1;

        #endregion

        #region Constructors

        public BarsService()
        {
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        public BarsService(NinjaScriptBase ninjascript) : this(ninjascript, null, InstrumentCode.Default, TimeFrame.Default, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
        {
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        /// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
        public BarsService(NinjaScriptBase ninjascript, TimeFrame timeFrame) : this(ninjascript, null, InstrumentCode.Default, timeFrame, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
        {
        }

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="options">The options to configure the <see cref="BarsService"/>.</param>
        //public BarsService(NinjaScriptBase ninjascript, BarsOptions options) : base(ninjascript, options)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="configureOptions">The actions to configure the <see cref="BarsService"/>.</param>
        //public BarsService(NinjaScriptBase ninjascript, Action<BarsOptions> configureOptions) : base(ninjascript, configureOptions)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="printService">The logging service to write in the NinjaScript output window.</param>
        //public BarsService(NinjaScriptBase ninjascript, PrintService printService) : base(ninjascript, printService)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="printService">The logging service to write in the NinjaScript output window.</param>
        ///// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
        //public BarsService(NinjaScriptBase ninjascript, PrintService printService, TimeFrame timeFrame) : this(ninjascript, printService, InstrumentCode.Default, timeFrame, Core.Data.MarketDataType.Last, TradingHoursCode.Default)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="printService">The logging service to write in the NinjaScript output window.</param>
        ///// <param name="options">The options to configure the <see cref="BarsService"/>.</param>
        //public BarsService(NinjaScriptBase ninjascript, PrintService printService, BarsOptions options) : base(ninjascript, printService, options)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BarsService"/> instance.
        ///// </summary>
        ///// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        ///// <param name="printService">The logging service to write in the NinjaScript output window.</param>
        ///// <param name="configureOptions">The actions to configure the <see cref="BarsService"/>.</param>
        //public BarsService(NinjaScriptBase ninjascript, PrintService printService, Action<BarsOptions> configureOptions) : base(ninjascript, printService, configureOptions)
        //{
        //}

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The Ninjatrader.NinjaScript to inject in the service.</param>
        /// <param name="printService">The logging service to write in the NinjaScript output window.</param>
        /// <param name="instrumentCode">The instrument code. Is necesary to construct a data serie of other instrument.</param>
        /// <param name="timeFrame">The time frame of the bars service. It is necesary to construct <see cref="BarsPeriod"/>of the data serie.</param>
        /// <param name="marketDataType">The type of the market data. The posible values are Last, Ask or Bid.</param>
        /// <param name="tradingHoursCode">The trading hours code. It's necesary to construct a data serie with another trading hours.</param>
        public BarsService(NinjaScriptBase ninjascript, PrintService printService, InstrumentCode instrumentCode, TimeFrame timeFrame, Core.Data.MarketDataType marketDataType, TradingHoursCode tradingHoursCode) //: base(ninjascript)
        {
            Options.TimeFrame = timeFrame;
            Options.InstrumentName = instrumentCode;
            Options.MarketDataType = marketDataType;
            Options.TradingHours = tradingHoursCode;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => InstrumentName + "(" + BarsPeriod.ToShortString() + ")";

        internal override void Configure(out bool isConfigured)
        {
            _barsEvents = new Dictionary<BarsEvent, bool>()
            {
                [BarsEvent.None] = false,
                [BarsEvent.LastBarRemoved] = false,
                [BarsEvent.BarClosed] = false,
                [BarsEvent.FirstTick] = false,
                [BarsEvent.PriceChanged] = false,
                [BarsEvent.Tick] = false
            };

            _logLines = new List<string>();

            isConfigured = true;
        }

        internal override void DataLoaded(out bool isConfigureWhenDataLoaded)
        {
            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (Ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == InstrumentName)
                    if (Ninjascript.BarsPeriods[i] == BarsPeriod &&
                        Ninjascript.BarsArray[i].TradingHours.Name == TradingHoursName
                        )
                    {
                        Idx = i;
                        break;
                    }

            if (Idx == -1)
            {
                isConfigureWhenDataLoaded = false;
                return;
            }

            //_lastBar = new LastBarService(Ninjascript, this);
            //_currentBar = new LastBarService(Ninjascript, this);

            isConfigureWhenDataLoaded = true;

            OnInit();
        }

        public override void Calculate()
        {
            _currentBar.Update();

            ResetBarsEvents();
            ExecuteMethods(_onBarUpdateMethods);

            // LasBarRemoved
            if (Ninjascript.BarsArray[Idx].BarsType.IsRemoveLastBarSupported && _currentBar.Idx < _lastBar.Idx)
            {
                SetBarsEventValue(BarsEvent.LastBarRemoved, true);
                OnLastBarRemoved();
                ExecuteMethods(_onLastBarRemovedMethods);
            }
            else
            {
                // BarClosed Or First tick success
                if (_currentBar.Idx != _lastBar.Idx)
                {
                    SetBarsEventValue(BarsEvent.BarClosed, true);
                    OnBarClosed();
                    ExecuteMethods(_onBarClosedMethods);

                    if (Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose)
                    {
                        SetBarsEventValue(BarsEvent.FirstTick, true);
                        OnFirstTick();
                        ExecuteMethods(_onFirstTickMethods);

                        if (_lastBar.Close.ApproxCompare(_currentBar.Close) != 0)
                        {
                            SetBarsEventValue(BarsEvent.PriceChanged, true);
                            OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
                            ExecuteMethods(_onPriceChangedMethods);
                        }

                        if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                        {
                            SetBarsEventValue(BarsEvent.Tick, true);
                            OnEachTick(new TickEventArgs(true));
                            ExecuteMethods(_onEachTickMethods);
                        }
                    }
                }

                // Tick Success
                else
                {
                    if (_lastBar.Close.ApproxCompare(_currentBar.Close) != 0)
                    {
                        SetBarsEventValue(BarsEvent.PriceChanged, true);
                        OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
                    }
                    if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                    {
                        SetBarsEventValue(BarsEvent.Tick, true);
                        OnEachTick(new TickEventArgs(false));
                    }
                }
            }
            _currentBar.CopyTo(_lastBar);
        }

        public override void Log()
        {
            if (_logLines == null || _logLines.Count == 0)
                return;
            string stateText = string.Empty;
            for (int i = 0; i < _logLines.Count; i++)
                stateText += _logLines[i];

            PrintService?.LogValue(stateText);
        }

        ///// <summary>
        ///// <inheritdoc/>
        ///// </summary>
        ///// <exception cref="Exception">The 'NinjaScript.State' must be 'State.Configure' or 'State.DataLoaded'.</exception>
        //public void Configure()
        //{
        //    if (IsConfiguredWhenDataLoaded || IsConfigured)
        //        return;

        //    if (!IsInConfigurationStates())
        //        ServicesLogHelpers.OutOfConfigureStateException(Name);

        //    _barsEvents = new Dictionary<BarsEvent, bool>()
        //    {
        //        [BarsEvent.None] = false,
        //        [BarsEvent.LastBarRemoved] = false,
        //        [BarsEvent.BarClosed] = false,
        //        [BarsEvent.FirstTick] = false,
        //        [BarsEvent.PriceChanged] = false,
        //        [BarsEvent.Tick] = false
        //    };

        //    _logLines = new List<string>();

        //    IsConfigured = true;
        //}

        ///// <summary>
        ///// <inheritdoc/>
        ///// </summary>
        ///// <exception cref="Exception">The 'NinjaScript.State' must be 'State.DataLoaded'.</exception>
        //public void DataLoaded()
        //{
        //    if (IsConfiguredWhenDataLoaded)
        //        return;

        //    if (Ninjascript.State != State.DataLoaded)
        //        ServicesLogHelpers.OutOfDataLoadedStateException(Name);

        //    if (!IsConfigured)
        //        Configure();

        //    for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
        //        if (Ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == InstrumentName)
        //            if (Ninjascript.BarsPeriods[i] == BarsPeriod && 
        //                Ninjascript.BarsArray[i].TradingHours.Name == TradingHoursName
        //                )
        //            {
        //                Idx = i;
        //                break;
        //            }

        //    if (Idx == -1)
        //        return;

        //    _lastBar = new LastBarService(Ninjascript, this);
        //    _currentBar = new LastBarService(Ninjascript, this);

        //    IsConfiguredWhenDataLoaded = true;

        //    OnInit();
        //}

        ///// <summary>
        ///// <inheritdoc/>
        ///// </summary>
        ///// <exception cref="Exception">The 'NinjaScript.State' must be 'State.Historical' or 'State.Realtime'.</exception>
        //public void OnBarUpdate()
        //{
        //    if (!Options.IsEnable)
        //        return;

        //    if (Ninjascript.BarsInProgress < 0 || Ninjascript.BarsInProgress != Idx)
        //        return;

        //    if (!IsInRunningStates())
        //        ServicesLogHelpers.OutOfRunningStatesException(Name);

        //    if (!IsConfiguredWhenDataLoaded)
        //        ServicesLogHelpers.OutOfConfigurationStatesException(Name);

        //    _currentBar.OnBarUpdate();

        //    ResetBarsEvents();
        //    ExecuteMethods(_onBarUpdateMethods);

        //    // LasBarRemoved
        //    if (Ninjascript.BarsArray[Idx].BarsType.IsRemoveLastBarSupported && _currentBar.Idx < _lastBar.Idx)
        //    {
        //        SetBarsEventValue(BarsEvent.LastBarRemoved, true);
        //        OnLastBarRemoved();
        //        ExecuteMethods(_onLastBarRemovedMethods);
        //    }
        //    else
        //    {
        //        // BarClosed Or First tick success
        //        if (_currentBar.Idx != _lastBar.Idx)
        //        {
        //            SetBarsEventValue(BarsEvent.BarClosed, true);
        //            OnBarClosed();
        //            ExecuteMethods(_onBarClosedMethods);

        //            if (Ninjascript.Calculate != Calculate.OnBarClose)
        //            {
        //                SetBarsEventValue(BarsEvent.FirstTick, true);
        //                OnFirstTick();
        //                ExecuteMethods(_onFirstTickMethods);

        //                if (_lastBar.Close.ApproxCompare(_currentBar.Close) != 0)
        //                {
        //                    SetBarsEventValue(BarsEvent.PriceChanged, true);
        //                    OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
        //                    ExecuteMethods(_onPriceChangedMethods);
        //                }

        //                if (Ninjascript.Calculate == Calculate.OnEachTick)
        //                {
        //                    SetBarsEventValue(BarsEvent.Tick, true);
        //                    OnEachTick(new TickEventArgs(true));
        //                    ExecuteMethods(_onEachTickMethods);
        //                }
        //            }
        //        }

        //        // Tick Success
        //        else
        //        {
        //            if (_lastBar.Close.ApproxCompare(_currentBar.Close) != 0)
        //            {
        //                SetBarsEventValue(BarsEvent.PriceChanged, true);
        //                OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
        //            }
        //            if (Ninjascript.Calculate == Calculate.OnEachTick)
        //            {
        //                SetBarsEventValue(BarsEvent.Tick, true);
        //                OnEachTick(new TickEventArgs(false));
        //            }
        //        }
        //    }
        //    _currentBar.CopyTo(_lastBar);
        //    LogBarsEvents();
        //}

        #endregion

        #region Public methods


        /// <summary>
        /// Add the <see cref="INeedBarsService"/> methods to execute when any changed is produced in the bars.
        /// </summary>
        /// <param name="services">The <see cref="INeedBarsService"/> objects.</param>
        internal void AddServices(params IBarUpdateService[] services)
        {
            PrintService?.LogTrace($"{Name} entry in 'AddServices' method.");
            if (services == null || services.Length == 0)
                return;

            for (int i = 0; i < services.Length; i++)
                AddService(services[i]);
        }

        /// <summary>
        /// Add the <see cref="INeedBarsService"/> method to execute when any changed produced in the bars.
        /// </summary>
        /// <param name="service">The <see cref="INeedBarsService"/> objects.</param>
        internal void AddService(IBarUpdateService service)
        {
            PrintService?.LogTrace($"{Name} entry in 'AddService' method.");
            if (_onBarUpdateMethods == null)
                _onBarUpdateMethods = new List<Action>();
            _onBarUpdateMethods.Add(service.Update);
            if (service is IBarClosedService barClosed)
            {
                if (_onBarClosedMethods == null)
                    _onBarClosedMethods = new List<Action>();
                _onBarClosedMethods.Add(barClosed.BarClosed);
            }
            if (service is IEachTickService barTick)
            {
                if (_onEachTickMethods == null)
                    _onEachTickMethods = new List<Action>();
                _onEachTickMethods.Add(barTick.EachTick);
            }
            if (service is IPriceChangedService priceChanged)
            {
                if (_onPriceChangedMethods == null)
                    _onPriceChangedMethods = new List<Action>();
                _onPriceChangedMethods.Add(priceChanged.PriceChanged);
            }
            if (service is IFirstTickService barFirstTick)
            {
                if (_onFirstTickMethods == null)
                    _onFirstTickMethods = new List<Action>();
                _onFirstTickMethods.Add(barFirstTick.FirstTick);
            }
            if (service is ILastBarRemovedService lastBarRemoved)
            {
                if (_onLastBarRemovedMethods == null)
                    _onLastBarRemovedMethods = new List<Action>();
                _onLastBarRemovedMethods.Add(lastBarRemoved.LastBarRemoved);
            }
        }

        ///// <summary>
        ///// Add the print service for print in the NinjScript output window.
        ///// </summary>
        ///// <param name="printSvc">The print service.</param>
        //public void AddPrintService(PrintService printSvc) => AddPrintService(printSvc, null);

        ///// <summary>
        ///// Add the print service for print in the NinjScript output window.
        ///// </summary>
        ///// <param name="printSvc">The print service.</param>
        ///// <param name="options">The bars log options.</param>
        //public void AddPrintService(PrintService printSvc, BarsOptions options)
        //{
        //    //Print = printSvc;
        //    //_logOptions = options ?? new BarsOptions();
        //}

        ///// <summary>
        ///// Prints the states of the bars.
        ///// </summary>
        //public void LogBarsEvents()
        //{
        //    if (_logLines == null || _logLines.Count == 0)
        //        return;
        //    string stateText = string.Empty;
        //    for (int i = 0; i < _logLines.Count; i++)
        //        stateText += _logLines[i];

        //    PrintService?.LogValue(stateText);
        //}

        #endregion

        #region Virtual methods

        ///// <summary>
        ///// An event driven method which is called whenever a bar service is initialized.
        ///// </summary>
        //protected virtual void OnInit() { }

        /// <summary>
        /// An event driven method which is called whenever a last bar is removed.
        /// </summary>
        protected virtual void OnLastBarRemoved() { }

        /// <summary>
        /// An event driven method which is called whenever a bar is closed.
        /// </summary>
        protected virtual void OnBarClosed() { }

        /// <summary>
        /// An event driven method which is called whenever price changed.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPriceChanged(PriceChangedEventArgs args) { }

        /// <summary>
        /// An event driven method which is called whenever a new tick success.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnEachTick(TickEventArgs args) { }

        /// <summary>
        /// An event driven method which is called whenever a bar first tick success.
        /// </summary>
        protected virtual void OnFirstTick() { }

        #endregion

        #region Private methods

        internal bool GetIsClosed(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _barsEvents[BarsEvent.BarClosed];
        internal bool GetHasNewTick(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick && _barsEvents[BarsEvent.Tick];
        internal bool GetIsRemoved(int barsInProgress) => !IsBarsInProgressOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _barsEvents[BarsEvent.LastBarRemoved];
        internal bool GetHasNewPrice(int barsInProgress) => Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _barsEvents[BarsEvent.PriceChanged];
        internal bool GetIsFirstTick(int barsInProgress) => Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _barsEvents[BarsEvent.FirstTick];

        private bool IsBarsInProgress(int barsInProgress) => barsInProgress == Ninjascript.BarsInProgress && barsInProgress == Idx;
        private bool IsUpdated() => _lastBar.Idx == _currentBar.Idx;

        private void SetBarsEvents(bool noneEvent, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _barsEvents[BarsEvent.None] = noneEvent;
            _barsEvents[BarsEvent.LastBarRemoved] = isLastBarRemoved;
            _barsEvents[BarsEvent.BarClosed] = isBarClosed;
            _barsEvents[BarsEvent.FirstTick] = isFirstTick;
            _barsEvents[BarsEvent.PriceChanged] = isPriceChanged;
            _barsEvents[BarsEvent.Tick] = isNewTick;
        }
        private void ResetBarsEvents()
        {
            SetBarsEvents(
                noneEvent: false,
                isLastBarRemoved: false,
                isBarClosed: false,
                isFirstTick: false,
                isPriceChanged: false,
                isNewTick: false
                );
            _logLines.Clear();
        }
        private void SetBarsEventValue(BarsEvent barsEvent, bool value)
        {
            _barsEvents[barsEvent] = value;

            if (PrintService == null)
                return;

            if (PrintService.IsLogLevelsEnable(Core.Logging.LogLevel.Information) && Options.IsLogEnable)
                _logLines.Add(barsEvent.ToString());
        }

        #endregion

        ///// <summary>
        ///// Mthod thats update the service. This method must be executed in 'Ninjatrader.NinjaScript.OnBarUpdate' method.
        ///// </summary>
        //public void OnBarUpdate()
        //{
        //    if (!IsEnable)
        //        return;

        //    if (!IsConfigured)
        //        return;

        //    if (IsOutOfRunningStates())
        //        LoggingHelpers.OutOfRunningStatesException(_printService, Name);

        //    if (IsNotAvilableBarsInProgressIdx())
        //        LoggingHelpers.NotAvailableNinjaScriptIndexError(_printService, "BarsInProgress", Ninjascript.Instance.BarsInProgress, Name);

        //    if (IsNotAvailableFirstBarIdx())
        //        LoggingHelpers.NotAvailableNinjaScriptIndexError(_printService, "CurrentBar", Ninjascript.Instance.CurrentBars[Ninjascript.Instance.BarsInProgress], Name);

        //    BarUpdate();

        //}

        //public void BarUpdate()
        //{
        //    if (!Filters())
        //        return;

        //    _printService?.LogTrace($"{Name} passed 'OnBarUpdate' filters.");

        //    Calculate();
        //    _printService?.LogTrace($"{Name} has been updated succesfully.");

        //    if (_printService != null)
        //        Log();
        //}

        ///// <summary>
        ///// Filters that need to be passed to update the service 
        ///// </summary>
        //protected virtual bool Filters() => true;

        ///// <summary>
        ///// Method to execute for update the service.
        ///// </summary>
        //public abstract void Calculate();


    }
}
