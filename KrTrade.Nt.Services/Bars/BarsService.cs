using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Events;
using KrTrade.Nt.Core.Interfaces;
using KrTrade.Nt.Services.Bars;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseBarsService,
        INeedConfiguration,
        IConfigureService,
        IDataLoadedService,
        IOnBarUpdateService
    {

        #region Private members

        // Configuration
        private bool _isInitialized;
        private bool _isConfigured;
        // Last and current Bar
        private LastBarService _lastBar;
        private LastBarService _currentBar;
        // State
        private Dictionary<BarsState, bool> _states;
        // Logging
        private List<string> _logLines;
        private BarsLogOptions _logOptions;
        // Execute methods
        private List<Action> _onBarUpdateMethods;
        private List<Action> _onLastBarRemovedMethods;
        private List<Action> _onBarClosedMethods;
        private List<Action> _onPriceChangedMethods;
        private List<Action> _onEachTickMethods;
        private List<Action> _onFirstTickMethods;
        // Fields
        private BarsPeriod _barsPeriod;

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates the service is configured.
        /// </summary>
        public bool IsConfigured => _isConfigured;

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

        /// <summary>
        /// Gets or sets the instrument point value.
        /// </summary>
        public double PointValue => IsInActiveStates() ? _ninjascript.BarsArray[Idx].Instrument.MasterInstrument.PointValue : -1;

        /// <summary>
        /// Gets or sets the instrument tick size.
        /// </summary>
        public double TickSize => IsInActiveStates() ? _ninjascript.BarsArray[Idx].Instrument.MasterInstrument.TickSize : -1;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> argument cannot be null.</exception>
        public BarsService(NinjaScriptBase ninjascript) : this(ninjascript, InstrumentCode.Default, TimeFrame.Default)
        {
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <param name="timeFrame">The time frame of the DataSeries.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> argument cannot be null.</exception>
        public BarsService(NinjaScriptBase ninjascript, TimeFrame timeFrame) : this(ninjascript,InstrumentCode.Default, timeFrame)
        { 
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <param name="instrumentCode">The instrument of the DataSeries.</param>
        /// <param name="timeFrame">The time frame of the DataSeries.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> argument cannot be null.</exception>
        public BarsService(NinjaScriptBase ninjascript, InstrumentCode instrumentCode, TimeFrame timeFrame) : base(ninjascript, instrumentCode, timeFrame)
        {
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <param name="printService">The print service to write in the NinjaScript output window.</param>
        /// <param name="instrumentCode">The instrument of the DataSeries.</param>
        /// <param name="timeFrame">The time frame of the DataSeries.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> argument cannot be null.</exception>
        public BarsService(NinjaScriptBase ninjascript, PrintService printService, InstrumentCode instrumentCode, TimeFrame timeFrame) : base(ninjascript, printService, instrumentCode, timeFrame)
        {
        }

        /// <summary>
        /// Create <see cref="BarsService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <param name="printService">The print service to write in the NinjaScript output window.</param>
        /// <param name="instrumentCode">The instrument of the DataSeries.</param>
        /// <param name="timeFrame">The time frame of the DataSeries.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/> argument cannot be null.</exception>
        public BarsService(MultiTimeFrameService multiTimeFrameService, InstrumentCode instrumentCode, TimeFrame timeFrame) : base(multiTimeFrameService?._ninjascript, multiTimeFrameService?._printService, instrumentCode, timeFrame)
        {
            multiTimeFrameService.AddDataSeries(this);
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <exception cref="Exception">The 'NinjaScript.State' must be 'State.Configure' or 'State.DataLoaded'.</exception>
        public void Configure()
        {
            if (_isConfigured || _isInitialized)
                return;

            if (!IsInConfigurationStates())
                throw new Exception($"The configuration methods of {Name}, must be executed when 'NinjaScript.State' is 'Configure' or 'DataLoaded'.");

            _states = new Dictionary<BarsState, bool>()
            {
                [BarsState.None] = false,
                [BarsState.LastBarRemoved] = false,
                [BarsState.BarClosed] = false,
                [BarsState.FirstTick] = false,
                [BarsState.PriceChanged] = false,
                [BarsState.Tick] = false
            };

            _logLines = new List<string>();

            _isInitialized = true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <exception cref="Exception">The 'NinjaScript.State' must be 'State.DataLoaded'.</exception>
        public void DataLoaded()
        {
            if (_isConfigured)
                return;

            if (_ninjascript.State != State.DataLoaded)
                throw new Exception("The service must be configured when ninjascript data is loaded in 'OnStateChanged.State.DataLoaded'.");

            if (!_isInitialized)
                Configure();

            for (int i = 0; i<_ninjascript.BarsArray.Length; i++)
                if (_ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == InstrumentName)
                    if (_ninjascript.BarsPeriods[i] == BarsPeriod)
                    {
                        Idx = i;
                        break;
                    }

            if (Idx == -1)
                return;

            _lastBar = new LastBarService(_ninjascript,this,_printService);
            _currentBar = new LastBarService(_ninjascript, this,_printService);

            _isConfigured = true;

            OnInit();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <exception cref="Exception">The 'NinjaScript.State' must be 'State.Historical' or 'State.Realtime'.</exception>
        public void OnBarUpdate()
        {
            if (_ninjascript.BarsInProgress < 0 || _ninjascript.BarsInProgress != Idx)
                return;

            if (_ninjascript.State != State.Historical && _ninjascript.State != State.Realtime )
                throw new Exception("The Bars service must be executed in 'NinjaScript.OnBarUpdate' method.");

            if (!_isConfigured)
                throw new Exception("The Bars service must be configured before being executed in 'OnBarUpdate' method.");

            //int barsInProgress = _ninjascript.BarsInProgress;

            _currentBar.OnBarUpdate();
            ResetStates();
            ExecuteMethods(_onBarUpdateMethods);

            // LasBarRemoved
            if (_ninjascript.BarsArray[Idx].BarsType.IsRemoveLastBarSupported && _currentBar.Idx < _lastBar.Idx)
            {
                SetStateValue(BarsState.LastBarRemoved, true);
                OnLastBarRemoved();
                ExecuteMethods(_onLastBarRemovedMethods);
            }
            else
            {
                // BarClosed Or First tick success
                if (_currentBar.Idx != _lastBar.Idx)
                {
                    SetStateValue(BarsState.BarClosed, true);
                    OnBarClosed();
                    ExecuteMethods(_onBarClosedMethods);

                    if (_ninjascript.Calculate != Calculate.OnBarClose)
                    {
                        SetStateValue(BarsState.FirstTick, true);
                        OnFirstTick();
                        ExecuteMethods(_onFirstTickMethods);

                        if (_lastBar.Close.ApproxCompare(_currentBar.Close) != 0)
                        {
                            SetStateValue(BarsState.PriceChanged, true);
                            OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
                            ExecuteMethods(_onPriceChangedMethods);
                        }

                        if (_ninjascript.Calculate == Calculate.OnEachTick)
                        {
                            SetStateValue(BarsState.Tick, true);
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
                        SetStateValue(BarsState.PriceChanged, true);
                        OnPriceChanged(new PriceChangedEventArgs(_lastBar.Close, _currentBar.Close));
                    }
                    if (_ninjascript.Calculate == Calculate.OnEachTick)
                    {
                        SetStateValue(BarsState.Tick, true);
                        OnEachTick(new TickEventArgs(false));
                    }
                }
            }

            _currentBar.CopyTo(_lastBar);
            PrintState();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add the <see cref="INeedBarsService"/> methods to execute when any changed is produced in the bars.
        /// </summary>
        /// <param name="services">The <see cref="INeedBarsService"/> objects.</param>
        internal void AddServices(params INeedBarsService[] services)
        {
            if (services == null || services.Length == 0)
                return;

            for (int i = 0; i < services.Length; i++)
                AddService(services[i]);
        }

        /// <summary>
        /// Add the <see cref="INeedBarsService"/> method to execute when any changed produced in the bars.
        /// </summary>
        /// <param name="service">The <see cref="INeedBarsService"/> objects.</param>
        internal void AddService(INeedBarsService service)
        {
            if (service is IOnBarUpdateService barUpdate)
            {
                if (_onBarUpdateMethods == null)
                    _onBarUpdateMethods = new List<Action>();
                _onBarUpdateMethods.Add(barUpdate.OnBarUpdate);
            }
            if (service is IOnBarClosedService barClosed)
            {
                if (_onBarClosedMethods == null)
                    _onBarClosedMethods = new List<Action>();
                _onBarClosedMethods.Add(barClosed.OnBarClosed);
            }
            if (service is IOnEachTickService barTick)
            {
                if (_onEachTickMethods == null)
                    _onEachTickMethods = new List<Action>();
                _onEachTickMethods.Add(barTick.OnTick);
            }
            if (service is IOnPriceChangedService priceChanged)
            {
                if (_onPriceChangedMethods == null)
                    _onPriceChangedMethods = new List<Action>();
                _onPriceChangedMethods.Add(priceChanged.OnPriceChanged);
            }
            if (service is IOnFirstTickService barFirstTick)
            {
                if (_onFirstTickMethods == null)
                    _onFirstTickMethods = new List<Action>();
                _onFirstTickMethods.Add(barFirstTick.OnFirstTick);
            }
            if (service is IOnLastBarRemovedService lastBarRemoved)
            {
                if (_onLastBarRemovedMethods == null)
                    _onLastBarRemovedMethods = new List<Action>();
                _onLastBarRemovedMethods.Add(lastBarRemoved.OnLastBarRemoved);
            }
        }

        /// <summary>
        /// Add the print service for print in the NinjScript output window.
        /// </summary>
        /// <param name="printSvc">The print service.</param>
        public void AddPrintService(PrintService printSvc) => AddPrintService(printSvc, null);

        /// <summary>
        /// Add the print service for print in the NinjScript output window.
        /// </summary>
        /// <param name="printSvc">The print service.</param>
        /// <param name="options">The bars log options.</param>
        public void AddPrintService(PrintService printSvc, BarsLogOptions options)
        {
            _printService = printSvc;
            _logOptions = options ?? new BarsLogOptions();
        }

        /// <summary>
        /// Prints the states of the bars.
        /// </summary>
        public void PrintState()
        {
            if (_printService == null)
                return;

            if (_logLines == null || _logLines.Count == 0)
                return;
            string stateText = _logOptions.Label;
            for (int i = 0; i < _logLines.Count; i++)
            {
                stateText += _logLines[i];
                if (i != _logLines.Count - 1)
                    stateText += _logOptions.StatesSeparator;
            }
            _printService?.Write(stateText);
        }

        #endregion

        #region virtual methods

        /// <summary>
        /// An event driven method which is called whenever a bar service is initialized.
        /// </summary>
        protected virtual void OnInit() { }

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

        internal bool GetIsClosed(int barsInProgress) => !IsOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _states[BarsState.BarClosed];
        internal bool GetHasNewTick(int barsInProgress) => !IsOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _ninjascript.Calculate == Calculate.OnEachTick && _states[BarsState.Tick];
        internal bool GetIsRemoved(int barsInProgress) => !IsOutOfRange(barsInProgress) && IsBarsInProgress(barsInProgress) && _states[BarsState.LastBarRemoved];
        internal bool GetHasNewPrice(int barsInProgress) => _ninjascript.Calculate != Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _states[BarsState.PriceChanged];
        internal bool GetIsFirstTick(int barsInProgress) => _ninjascript.Calculate != Calculate.OnBarClose && IsBarsInProgress(barsInProgress) && _states[BarsState.FirstTick];

        private bool IsBarsInProgress(int barsInProgress) => barsInProgress == _ninjascript.BarsInProgress && barsInProgress == Idx;
        private bool IsUpdated() => _lastBar.Idx == _currentBar.Idx;

        private void SetStateProperties(bool noneState, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _states[BarsState.None] = noneState;
            _states[BarsState.LastBarRemoved] = isLastBarRemoved;
            _states[BarsState.BarClosed] = isBarClosed;
            _states[BarsState.FirstTick] = isFirstTick;
            _states[BarsState.PriceChanged] = isPriceChanged;
            _states[BarsState.Tick] = isNewTick;
        }
        private void ResetStates()
        {
            SetStateProperties(
                noneState: false,
                isLastBarRemoved: false,
                isBarClosed: false,
                isFirstTick: false,
                isPriceChanged: false,
                isNewTick: false
                );
            _logLines.Clear();
        }
        private void SetStateValue(BarsState state, bool value)
        {
            _states[state] = value;
            if (state.ToLogLevel() >= _logOptions.LogLevel)
                _logLines.Add(state.ToString());
        }

        #endregion
    }
}
