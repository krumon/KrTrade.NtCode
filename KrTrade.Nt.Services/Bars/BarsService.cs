using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Events;
using KrTrade.Nt.Core.Interfaces;
using KrTrade.Nt.Core.Print;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services.Bars
{
    public class BarsService : 
        IConfigureService,
        IDataLoadedService,
        IOnBarUpdateService
    {

        #region Private members

        private readonly NinjaScriptBase _ninjascript;
        private readonly PrintService _printSvc;

        private bool _isInitialized;
        private bool _isConfigured;
        //private int[] _saveCurrentBars;
        //private double[] _saveCurrentPrices;

        private BarService[] _lastBars;
        private BarService[] _currentBars;

        private List<Dictionary<BarsState, bool>> _states;
        private List<string> _logLines;
        private readonly BarsLogOptions _logOptions;

        private List<Action> _onBarUpdateMethods;
        private List<Action> _onLastBarRemovedMethods;
        private List<Action> _onBarClosedMethods;
        private List<Action> _onPriceChangedMethods;
        private List<Action> _onEachTickMethods;
        private List<Action> _onFirstTickMethods;

        #endregion

        #region Public properties

        /// <summary>
        /// True, if service is configured, otherwise false.
        /// For the service to be configured, the 'Configure' and 'DataLoaded' methods must be executed.
        /// </summary>
        public bool IsConfigured => _isConfigured;

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public BarService LastBar => GetLastBar(_ninjascript.BarsInProgress);

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public BarService CurrentBar => GetLastBar(_ninjascript.BarsInProgress);

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseBars"/> new instance.
        /// </summary>
        /// <param name="printSvc">The <see cref="BasePrint"/> service injected.</param>
        public BarsService(NinjaScriptBase ninjascript, PrintService printSvc) : this(ninjascript, printSvc, null)
        {
        }

        /// <summary>
        /// Create <see cref="BaseBars"/> new instance.
        /// </summary>
        /// <param name="printSvc">The <see cref="BasePrint"/> service injected.</param>
        /// <param name="options">the bars logging options.</param>
        public BarsService(NinjaScriptBase ninjascript, PrintService printSvc, BarsLogOptions options)
        {
            _ninjascript = ninjascript ?? throw new System.NotImplementedException();

            if (_ninjascript.State != State.Configure)
                throw new Exception("The services instance must be created in 'NinjaScript.OnStateChanged' method when 'State = State.Configure'");

            _printSvc = printSvc;
            _logOptions = options ?? new BarsLogOptions();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// A method which is called we want configure the service.
        /// </summary>
        public void Configure()
        {
            if (_isConfigured || _isInitialized)
                return;

            _states = new List<Dictionary<BarsState, bool>>();
            _logLines = new List<string>();

            for (int i = 0; i < _ninjascript.BarsArray.Length; i++)
            {
                _states.Add(new Dictionary<BarsState, bool>()
                {
                    [BarsState.None] = false,
                    [BarsState.LastBarRemoved] = false,
                    [BarsState.BarClosed] = false,
                    [BarsState.FirstTick] = false,
                    [BarsState.PriceChanged] = false,
                    [BarsState.Tick] = false
                });
            }

            _isInitialized = true;

        }

        /// <summary>
        /// A method which is called we want configure the service when ninjascript data is loaded.
        /// </summary>
        public void DataLoaded()
        {
            if (_isConfigured)
                return;

            if (!_isInitialized)
                throw new Exception("The 'Configure' method must be executed before 'DataLoaded' method.");

            //_saveCurrentBars = new int[_ninjascript.BarsArray.Length];
            //_saveCurrentPrices = new double[_ninjascript.BarsArray.Length];
            //InitializeArray(_saveCurrentBars, -1);
            //InitializeArray(_saveCurrentPrices, 0);

            _lastBars = new BarService[_ninjascript.BarsArray.Length];
            _currentBars = new BarService[_ninjascript.BarsArray.Length];
            InitializeArray(_lastBars);
            InitializeArray(_currentBars);

            _isConfigured = true;
        }

        /// <summary>
        /// An event driven method which is called whenever a bar is updated.
        /// </summary>
        public void OnBarUpdate()
        {
            if (_ninjascript.State != State.Historical && _ninjascript.State != State.Realtime)
                return;

            if (!_isConfigured)
            {
                _printSvc.Write("The services must be configure in the 'NinjaScript.OnStateChanged' method, when the 'State==State.Configure' and 'State==State.DataLoaded'.");
                _printSvc.Write("We try configure it.");

                Init();
                //return;
            }

            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBar < 0)
                return;

            int barsInProgress = _ninjascript.BarsInProgress;
            
            _currentBars[barsInProgress].OnBarUpdate();
            
            //int currentBar = CurrentBar;
            //double currentPrice = CurrentPrice;

            BarService currentBar = GetCurrentBar(barsInProgress);
            BarService lastBar = GetLastBar(barsInProgress);

            ResetStates(barsInProgress);
            ExecuteMethods(_onBarUpdateMethods);

            // LasBarRemoved
            //if (IsRemoveLastBarSupported && currentBar < _saveCurrentBars[barsInProgress])
            if (_ninjascript.BarsArray[_ninjascript.BarsInProgress].BarsType.IsRemoveLastBarSupported && currentBar.Idx < lastBar.Idx)
            {
                SetStateValue(barsInProgress, BarsState.LastBarRemoved, true);
                OnLastBarRemoved();
                ExecuteMethods(_onLastBarRemovedMethods);
                //PrintState();
            }
            else
            {
                // BarClosed Or First tick success
                //if (currentBar != _saveCurrentBars[barsInProgress])
                if (currentBar.Idx != lastBar.Idx)
                {
                    SetStateValue(barsInProgress, BarsState.BarClosed, true);
                    OnBarClosed();
                    ExecuteMethods(_onBarClosedMethods);

                    if (_ninjascript.Calculate != Calculate.OnBarClose)
                    {
                        SetStateValue(barsInProgress, BarsState.FirstTick, true);
                        OnFirstTick();
                        ExecuteMethods(_onFirstTickMethods);

                        if (lastBar.Close.ApproxCompare(currentBar.Close) != 0)
                        {
                            SetStateValue(barsInProgress, BarsState.PriceChanged, true);
                            OnPriceChanged(new PriceChangedEventArgs(lastBar.Close, currentBar.Close));
                            ExecuteMethods(_onPriceChangedMethods);
                        }

                        if (_ninjascript.Calculate == Calculate.OnEachTick)
                        {
                            SetStateValue(barsInProgress, BarsState.Tick, true);
                            OnEachTick(new TickEventArgs(true));
                            ExecuteMethods(_onEachTickMethods);
                        }
                    }
                }

                // Tick Success
                else
                {
                    if (lastBar.Close.ApproxCompare(currentBar.Close) != 0)
                    {
                        SetStateValue(barsInProgress, BarsState.PriceChanged, true);
                        OnPriceChanged(new PriceChangedEventArgs(lastBar.Close, currentBar.Close));
                    }
                    if (_ninjascript.Calculate == Calculate.OnEachTick)
                    {
                        SetStateValue(barsInProgress, BarsState.Tick, true);
                        OnEachTick(new TickEventArgs(false));
                    }
                }
            }

            currentBar.CopyTo(lastBar);
            //_saveCurrentBars[barsInProgress] = currentBar;
            //_saveCurrentPrices[barsInProgress] = currentPrice;
            //PrintState();
        }

        /// <summary>
        /// Prints the states of the bars.
        /// </summary>
        public void PrintState()
        {
            if (_logLines == null || _logLines.Count == 0)
                return;
            string stateText = _logOptions.Label;
            for (int i = 0; i < _logLines.Count; i++)
            {
                stateText += _logLines[i];
                if (i != _logLines.Count - 1)
                    stateText += _logOptions.StatesSeparator;
            }
            _printSvc?.Write(stateText);
        }

        #endregion

        #region Internal and Protected methods

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
        /// Initialize the service.
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected void Init()
        {
            if (_isInitialized) return;

            if (_ninjascript.State != State.DataLoaded)
                throw new Exception("The service must be initialized when ninjascript data is loaded.");

            //_saveCurrentBars = new int[_ninjascript.BarsArray.Length];
            //_saveCurrentPrices = new double[_ninjascript.BarsArray.Length];
            //InitializeArray(_saveCurrentBars, -1);
            //InitializeArray(_saveCurrentPrices, 0);

            _states = new List<Dictionary<BarsState, bool>>();
            _logLines = new List<string>();

            for (int i = 0; i < _ninjascript.BarsArray.Length; i++)
            {
                _states.Add(new Dictionary<BarsState, bool>()
                {
                    [BarsState.None] = false,
                    [BarsState.LastBarRemoved] = false,
                    [BarsState.BarClosed] = false,
                    [BarsState.FirstTick] = false,
                    [BarsState.PriceChanged] = false,
                    [BarsState.Tick] = false
                });
            }

            _isInitialized = true;

            OnInit();
        }

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

        internal bool GetIsClosed(int barsInProgress) => !IsOutOfRange(barsInProgress) && barsInProgress == _ninjascript.BarsInProgress &&  _states[barsInProgress][BarsState.BarClosed];
        internal bool GetHasNewTick(int barsInProgress) => !IsOutOfRange(barsInProgress) && barsInProgress == _ninjascript.BarsInProgress && _ninjascript.Calculate == Calculate.OnEachTick && _states[barsInProgress][BarsState.Tick];
        internal bool GetIsRemoved(int barsInProgress) => !IsOutOfRange(barsInProgress) && barsInProgress == _ninjascript.BarsInProgress && _states[barsInProgress][BarsState.LastBarRemoved];
        internal bool GetHasNewPrice(int barsInProgress) => _ninjascript.Calculate != Calculate.OnBarClose && barsInProgress == _ninjascript.BarsInProgress && _states[barsInProgress][BarsState.PriceChanged];
        internal bool GetIsFirstTick(int barsInProgress) => _ninjascript.Calculate != Calculate.OnBarClose && barsInProgress == _ninjascript.BarsInProgress && _states[barsInProgress][BarsState.FirstTick];

        private BarService GetCurrentBar(int barsInProgress)
        {
            if (IsOutOfRange(barsInProgress))
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));

            return _currentBars[barsInProgress];
        }
        private BarService GetLastBar(int barsInProgress)
        {
            if (IsOutOfRange(barsInProgress))
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));

            return _lastBars[barsInProgress];
        }
        
        private void ExecuteMethods(List<Action> methods)
        {
            if (methods == null || methods.Count == 0)
                return;

            for (int i = 0; i < methods.Count; i++)
                methods[i]?.Invoke();
        }

        private void InitializeArray(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(double[] array, double value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(BarService[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = new BarService(_ninjascript, this, i);
        }

        private bool IsOutOfRange(int barsInProgress)
        {
            if (barsInProgress < 0 || barsInProgress >= _ninjascript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
            return false;
        }

        private void SetStateProperties(int barsInProgress, bool noneState, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _states[barsInProgress][BarsState.None] = noneState;
            _states[barsInProgress][BarsState.LastBarRemoved] = isLastBarRemoved;
            _states[barsInProgress][BarsState.BarClosed] = isBarClosed;
            _states[barsInProgress][BarsState.FirstTick] = isFirstTick;
            _states[barsInProgress][BarsState.PriceChanged] = isPriceChanged;
            _states[barsInProgress][BarsState.Tick] = isNewTick;
        }
        private void ResetStates(int barsInProgress)
        {
            SetStateProperties(
                barsInProgress: barsInProgress,
                noneState: false,
                isLastBarRemoved: false,
                isBarClosed: false,
                isFirstTick: false,
                isPriceChanged: false,
                isNewTick: false
                );
            _logLines.Clear();
        }
        private void SetStateValue(int barsInProgress, BarsState state, bool value)
        {
            _states[barsInProgress][state] = value;
            if (state.ToLogLevel() >= _logOptions.LogLevel)
                _logLines.Add(state.ToString());
        }

        #endregion

        #region ToDelete

        ///// <summary>
        ///// Indicates if the last bar of the bars in progress is closed.
        ///// </summary>
        //public bool IsClosed => GetIsClosed(_ninjascript.BarsInProgress);

        ///// <summary>
        ///// Indicates if the last bar of the bars in progress is removed.
        ///// </summary>
        //public bool IsRemoved => GetIsRemoved(_ninjascript.BarsInProgress);

        ///// <summary>
        ///// Indicates if success the first tick in the current bar of the bars in progress.
        ///// </summary>
        //public bool IsFirstTick => GetIsFirstTick(_ninjascript.BarsInProgress);

        ///// <summary>
        ///// Indicates if success a new tick in the current bar of the bars in progress.
        ///// </summary>
        //public bool IsNewTick => GetHasNewTick(_ninjascript.BarsInProgress);

        ///// <summary>
        ///// Indicates if the price changed in the current bar of the bars in progress.
        ///// </summary>
        //public bool IsNewPrice => GetHasNewPrice(_ninjascript.BarsInProgress);

        //protected State State => _ninjascript.State;
        //protected int BarsInProgress => _ninjascript.BarsInProgress;
        //protected Calculate Calculate => _ninjascript.Calculate;
        //protected int Count => _ninjascript.BarsArray.Length;
        //protected int CurrentBar => _ninjascript.CurrentBar;
        //protected double CurrentPrice => _ninjascript.Inputs[BarsInProgress][0];
        //protected bool IsRemoveLastBarSupported => _ninjascript.BarsArray[BarsInProgress].BarsType.IsRemoveLastBarSupported;
        //protected double TickSize => _ninjascript.BarsArray[BarsInProgress].Instrument.MasterInstrument.TickSize;

        ///// <summary>
        ///// Method that must be executed in the ninjascript event handler method: 'OnBarUpdate', for the service to work correctly.
        ///// This method must be executed first, in the 'OnBarUpdate' method and then use the bar service throughout it.
        ///// </summary>
        //public void Update()
        //{
        //    if (State != State.Historical && State != State.Realtime)
        //        return;

        //    if (!_isInitialized)
        //        Init();

        //    if (BarsInProgress < 0)
        //        return;

        //    int barsInProgress = BarsInProgress;
        //    int currentBar = CurrentBar;
        //    double currentPrice = CurrentPrice;

        //    ResetStates(barsInProgress);
        //    OnBarUpdate();
        //    ExecuteMethods(_onBarUpdateMethods);

        //    // LasBarRemoved
        //    if (IsRemoveLastBarSupported && currentBar < _saveCurrentBars[barsInProgress])
        //    {
        //        SetStateValue(barsInProgress, BarsState.LastBarRemoved, true);
        //        OnLastBarRemoved();
        //        ExecuteMethods(_onLastBarRemovedMethods);
        //        _saveCurrentBars[barsInProgress] = currentBar;
        //        _saveCurrentPrices[barsInProgress] = currentPrice;
        //        PrintState();
        //        return;
        //    }

        //    // BarClosed
        //    if (currentBar != _saveCurrentBars[barsInProgress])
        //    {
        //        SetStateValue(barsInProgress, BarsState.BarClosed, true);
        //        OnBarClosed();
        //        ExecuteMethods(_onBarClosedMethods);

        //        if (Calculate != Calculate.OnBarClose)
        //        {
        //            SetStateValue(barsInProgress, BarsState.FirstTick, true);
        //            OnFirstTick();
        //            ExecuteMethods(_onFirstTickMethods);

        //            if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
        //            {
        //                SetStateValue(barsInProgress, BarsState.PriceChanged, true);
        //                OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
        //                ExecuteMethods(_onPriceChangedMethods);
        //            }

        //            if (Calculate == Calculate.OnEachTick)
        //            {
        //                SetStateValue(barsInProgress, BarsState.Tick, true);
        //                OnEachTick(new TickEventArgs(true));
        //                ExecuteMethods(_onEachTickMethods);
        //            }
        //        }
        //    }

        //    // Tick Success
        //    else
        //    {
        //        if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
        //        {
        //            SetStateValue(barsInProgress, BarsState.PriceChanged, true);
        //            OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
        //        }
        //        if (Calculate == Calculate.OnEachTick)
        //        {
        //            SetStateValue(barsInProgress, BarsState.Tick, true);
        //            OnEachTick(new TickEventArgs(false));
        //        }
        //    }

        //    _saveCurrentBars[barsInProgress] = currentBar;
        //    _saveCurrentPrices[barsInProgress] = currentPrice;
        //    PrintState();

        //}

        #endregion
    }
}
