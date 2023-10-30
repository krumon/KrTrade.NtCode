//using KrTrade.Nt.Core.Interfaces;
//using KrTrade.Nt.Core.Print;
//using NinjaTrader.Core.FloatingPoint;
//using NinjaTrader.NinjaScript;
//using System;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Core.Bars
//{
//    /// <summary>
//    /// Base class for the Bars service
//    /// </summary>
//    public abstract class BaseBars : 
//        IConfigure,
//        IDataLoaded,
//        IBarUpdate
//    {
//        private readonly BasePrint _print;
        
//        private bool _isConfigured;
//        private bool _isInitialized;
//        private int[] _saveCurrentBars;
//        private double[] _saveCurrentPrices;

//        private List<Dictionary<BarsState, bool>> _states;
//        private List<string> _logLines;

//        private List<Action> _onBarUpdateMethods;
//        private List<Action> _onLastBarRemovedMethods;
//        private List<Action> _onBarClosedMethods;
//        private List<Action> _onPriceChangedMethods;
//        private List<Action> _onEachTickMethods;
//        private List<Action> _onFirstTickMethods;

//        /// <summary>
//        /// True, when the service is configured.
//        /// </summary>
//        public bool IsServiceConfigured => _isInitialized;

//        /// <summary>
//        /// Gets the ninjascript 'State'.
//        /// </summary>
//        public abstract State State { get; }

//        /// <summary>
//        /// Gets the ninjascript 'BarsInProgress'.
//        /// </summary>
//        public abstract int BarsInProgress { get; }

//        /// <summary>
//        /// Gets the ninjascript 'CalculateMode'.
//        /// </summary>
//        public abstract Calculate Calculate { get; }

//        /// <summary>
//        /// Gets the number of 'DataSeries' configured in the ninjascript.
//        /// </summary>
//        public abstract int Count { get; }

//        /// <summary>
//        /// Gets the 'CurrentBar' of the Bars in progress.
//        /// </summary>
//        public abstract int CurrentBar { get; }

//        /// <summary>
//        /// Gets the current price of the Bars in progress.
//        /// </summary>
//        public abstract double CurrentPrice { get; }

//        /// <summary>
//        /// Gets if the bars in progress support 'LastBarIsRemoved'.
//        /// </summary>
//        public abstract bool IsRemoveLastBarSupported { get; }

//        /// <summary>
//        /// Gets the 'TickSize' of the current 'DataSeries' in 'NinjaScript.OnBarUpdate' method.
//        /// </summary>
//        public abstract double TickSize { get; }    

//        /// <summary>
//        /// Indicates if the last bar of the bars in progress is closed.
//        /// </summary>
//        public bool IsLastBarClosed => GetIsClosed(BarsInProgress);

//        /// <summary>
//        /// Indicates if the last bar of the bars in progress is removed.
//        /// </summary>
//        public bool IsLastBarRemoved => GetIsRemoved(BarsInProgress);

//        /// <summary>
//        /// Indicates if success the first tick in the current bar of the bars in progress.
//        /// </summary>
//        public bool IsLastBarFirstTick => GetIsFirstTick(BarsInProgress);

//        /// <summary>
//        /// Indicates if success a new tick in the current bar of the bars in progress.
//        /// </summary>
//        public bool IsLastBarNewTick => GetHasNewTick(BarsInProgress);

//        /// <summary>
//        /// Indicates if the price changed in the current bar of the bars in progress.
//        /// </summary>
//        public bool IsLastBarNewPrice => GetHasNewPrice(BarsInProgress);

//        /// <summary>
//        /// the minimum level to be logged.
//        /// </summary>
//        public BarsLogLevel MinLogLevel { get; set; } = BarsLogLevel.BarClosed;

//        /// <summary>
//        /// The string separator for the diferent states in the log line text..
//        /// </summary>
//        public string LogStatesSeparator { get; set; } = " - ";

//        /// <summary>
//        /// True, if show the service label in the log lines.
//        /// </summary>
//        public bool ShowLogServiceLabel { get; set; }

//        /// <summary>
//        /// Represents the service label in the log lines.
//        /// </summary>
//        public string LogServiceLabel { get; set; } = "[Bars Service]: ";

//        /// <summary>
//        /// Create <see cref="BaseBars"/> new instance.
//        /// </summary>
//        /// <param name="printSvc">The <see cref="BasePrint"/> service injected.</param>
//        public BaseBars(BasePrint printSvc)
//        {
//            if (State != State.Configure)
//                throw new Exception("The services instance must be created in 'NinjaScript.OnStateChanged' method when 'State = State.Configure'");

//            if (printSvc == null)
//                return;
//            _print = printSvc;
//        }

//        /// <summary>
//        /// Add the <see cref="IBarsService"/> methods to execute when any changed is produced in the bars.
//        /// </summary>
//        /// <param name="objects">The <see cref="IBarsService"/> objects.</param>
//        public void AddServices(params IBarsService[] objects)
//        {
//            if (objects == null || objects.Length == 0)
//                return;

//            for (int i = 0; i < objects.Length; i++)
//            {
//                if (objects[i] is IBarUpdate barUpdate)
//                    _onBarUpdateMethods.Add(barUpdate.OnBarUpdate);
//                else if (objects[i] is IBarClosed barClosed)
//                    _onBarClosedMethods.Add(barClosed.OnBarClosed);
//                else if (objects[i] is IBarTick barTick)
//                    _onEachTickMethods.Add(barTick.OnTick);
//                else if (objects[i] is IBarPriceChanged priceChanged)
//                    _onPriceChangedMethods.Add(priceChanged.OnPriceChanged);
//                else if (objects[i] is IBarFirstTick barFirstTick)
//                    _onFirstTickMethods.Add(barFirstTick.OnFirstTick);
//                else if (objects[i] is IBarRemoved lastBarRemoved)
//                    _onLastBarRemovedMethods.Add(lastBarRemoved.OnLastBarRemoved);
//            }
//        }

//        /// <summary>
//        /// An event driven method which is called whenever a bar service is initialized.
//        /// </summary>
//        public virtual void OnInit() { }

//        /// <summary>
//        /// A method which is called we want configure the service.
//        /// </summary>
//        public void Configure() 
//        {
//            if (_isConfigured || _isInitialized) 
//                return;

//            _states = new List<Dictionary<BarsState, bool>>();
//            _logLines = new List<string>();

//            for (int i = 0; i < Count; i++)
//            {
//                _states.Add(new Dictionary<BarsState, bool>()
//                {
//                    [BarsState.None] = false,
//                    [BarsState.LastBarRemoved] = false,
//                    [BarsState.BarClosed] = false,
//                    [BarsState.FirstTick] = false,
//                    [BarsState.PriceChanged] = false,
//                    [BarsState.Tick] = false
//                });
//            }

//            if (ShowLogServiceLabel)
//                LogServiceLabel = "[" + LogServiceLabel + "]: ";
//            else
//                LogServiceLabel = string.Empty;

//            _isConfigured = true;

//        }

//        /// <summary>
//        /// A method which is called we want configure the service when ninjascript data is loaded.
//        /// </summary>
//        public void DataLoaded() 
//        {
//            if (_isInitialized) 
//                return;

//            if (!_isConfigured)
//                throw new Exception("The 'Configure' method must be executed before 'DataLoaded' method.");

//            _saveCurrentBars = new int[Count];
//            _saveCurrentPrices = new double[Count];
//            InitializeArray(_saveCurrentBars, -1);
//            InitializeArray(_saveCurrentPrices, 0);

//            _isInitialized = true;
//        }

//        /// <summary>
//        /// An event driven method which is called whenever a bar is updated.
//        /// </summary>
//        public void OnBarUpdate() 
//        {
//            if (State != State.Historical && State != State.Realtime)
//                return;

//            if (!_isInitialized)
//            {
//                _print.Write("The service is not configured. We try to configure.");
//                _print.Write("The services must be configure in the 'NinjaScript.OnStateChanged' method, when the 'State==State.Configure' and 'State==State.DataLoaded'.");
                    
//                Init();
//            }

//            if (BarsInProgress < 0)
//                return;

//            int barsInProgress = BarsInProgress;
//            int currentBar = CurrentBar;
//            double currentPrice = CurrentPrice;

//            ResetStates(barsInProgress);
//            OnBarUpdate();
//            ExecuteMethods(_onBarUpdateMethods);

//            // LasBarRemoved
//            if (IsRemoveLastBarSupported && currentBar < _saveCurrentBars[barsInProgress])
//            {
//                SetStateValue(barsInProgress, BarsState.LastBarRemoved, true);
//                OnLastBarRemoved();
//                ExecuteMethods(_onLastBarRemovedMethods);
//                _saveCurrentBars[barsInProgress] = currentBar;
//                _saveCurrentPrices[barsInProgress] = currentPrice;
//                PrintState();
//                return;
//            }

//            // BarClosed
//            if (currentBar != _saveCurrentBars[barsInProgress])
//            {
//                SetStateValue(barsInProgress, BarsState.BarClosed, true);
//                OnBarClosed();
//                ExecuteMethods(_onBarClosedMethods);

//                if (Calculate != Calculate.OnBarClose)
//                {
//                    SetStateValue(barsInProgress, BarsState.FirstTick, true);
//                    OnFirstTick();
//                    ExecuteMethods(_onFirstTickMethods);

//                    if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
//                    {
//                        SetStateValue(barsInProgress, BarsState.PriceChanged, true);
//                        OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
//                        ExecuteMethods(_onPriceChangedMethods);
//                    }

//                    if (Calculate == Calculate.OnEachTick)
//                    {
//                        SetStateValue(barsInProgress, BarsState.Tick, true);
//                        OnEachTick(new TickEventArgs(true));
//                        ExecuteMethods(_onEachTickMethods);
//                    }
//                }
//            }

//            // Tick Success
//            else
//            {
//                if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
//                {
//                    SetStateValue(barsInProgress, BarsState.PriceChanged, true);
//                    OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
//                }
//                if (Calculate == Calculate.OnEachTick)
//                {
//                    SetStateValue(barsInProgress, BarsState.Tick, true);
//                    OnEachTick(new TickEventArgs(false));
//                }
//            }

//            _saveCurrentBars[barsInProgress] = currentBar;
//            _saveCurrentPrices[barsInProgress] = currentPrice;
//            PrintState();
//        }

//        /// <summary>
//        /// An event driven method which is called whenever a last bar is removed.
//        /// </summary>
//        public virtual void OnLastBarRemoved() { }

//        /// <summary>
//        /// An event driven method which is called whenever a bar is closed.
//        /// </summary>
//        public virtual void OnBarClosed() { }

//        /// <summary>
//        /// An event driven method which is called whenever price changed.
//        /// </summary>
//        /// <param name="args"></param>
//        public virtual void OnPriceChanged(PriceChangedEventArgs args) { }

//        /// <summary>
//        /// An event driven method which is called whenever a new tick success.
//        /// </summary>
//        /// <param name="args"></param>
//        public virtual void OnEachTick(TickEventArgs args) { }

//        /// <summary>
//        /// An event driven method which is called whenever a bar first tick success.
//        /// </summary>
//        public virtual void OnFirstTick() { }

//        /// <summary>
//        /// Method that must be executed in the ninjascript event handler method: 'OnBarUpdate', for the service to work correctly.
//        /// This method must be executed first, in the 'OnBarUpdate' method and then use the bar service throughout it.
//        /// </summary>
//        public void Update()
//        {
//            if (State != State.Historical && State != State.Realtime)
//                return;

//            if (!_isInitialized)
//                Init();

//            if (BarsInProgress < 0)
//                return;

//            int barsInProgress = BarsInProgress;
//            int currentBar = CurrentBar;
//            double currentPrice = CurrentPrice;

//            ResetStates(barsInProgress);
//            OnBarUpdate();
//            ExecuteMethods(_onBarUpdateMethods);

//            // LasBarRemoved
//            if (IsRemoveLastBarSupported && currentBar < _saveCurrentBars[barsInProgress])
//            {
//                SetStateValue(barsInProgress, BarsState.LastBarRemoved, true);
//                OnLastBarRemoved();
//                ExecuteMethods(_onLastBarRemovedMethods);
//                _saveCurrentBars[barsInProgress] = currentBar;
//                _saveCurrentPrices[barsInProgress] = currentPrice;
//                PrintState();
//                return;
//            }

//            // BarClosed
//            if (currentBar != _saveCurrentBars[barsInProgress])
//            {
//                SetStateValue(barsInProgress, BarsState.BarClosed, true);
//                OnBarClosed();
//                ExecuteMethods(_onBarClosedMethods);

//                if (Calculate != Calculate.OnBarClose)
//                {
//                    SetStateValue(barsInProgress, BarsState.FirstTick, true);
//                    OnFirstTick();
//                    ExecuteMethods(_onFirstTickMethods);

//                    if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
//                    {
//                        SetStateValue(barsInProgress, BarsState.PriceChanged, true);
//                        OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
//                        ExecuteMethods(_onPriceChangedMethods);
//                    }

//                    if (Calculate == Calculate.OnEachTick)
//                    {
//                        SetStateValue(barsInProgress, BarsState.Tick, true);
//                        OnEachTick(new TickEventArgs(true));
//                        ExecuteMethods(_onEachTickMethods);
//                    }
//                }
//            }

//            // Tick Success
//            else
//            {
//                if (_saveCurrentPrices[barsInProgress].ApproxCompare(currentPrice) != 0)
//                {
//                    SetStateValue(barsInProgress, BarsState.PriceChanged, true);
//                    OnPriceChanged(new PriceChangedEventArgs(_saveCurrentPrices[barsInProgress], currentPrice));
//                }
//                if (Calculate == Calculate.OnEachTick)
//                {
//                    SetStateValue(barsInProgress, BarsState.Tick, true);
//                    OnEachTick(new TickEventArgs(false));
//                }
//            }

//            _saveCurrentBars[barsInProgress] = currentBar;
//            _saveCurrentPrices[barsInProgress] = currentPrice;
//            PrintState();

//        }

//        /// <summary>
//        /// Prints the states of the bars.
//        /// </summary>
//        public void PrintState()
//        {
//            if (_logLines == null || _logLines.Count == 0)
//                return;
//            string stateText = LogServiceLabel;
//            for (int i = 0; i < _logLines.Count; i++)
//            {
//                stateText += _logLines[i];
//                if (i != _logLines.Count - 1)
//                    stateText += LogStatesSeparator;
//            }
//            _print?.Write(stateText);
//        }

//        protected void Init()
//        {
//            if (_isInitialized) return;

//            _saveCurrentBars = new int[Count];
//            _saveCurrentPrices = new double[Count];
//            InitializeArray(_saveCurrentBars, -1);
//            InitializeArray(_saveCurrentPrices, 0);

//            _states = new List<Dictionary<BarsState, bool>>();
//            _logLines = new List<string>();

//            for (int i = 0; i < Count; i++)
//            {
//                _states.Add(new Dictionary<BarsState, bool>()
//                {
//                    [BarsState.None] = false,
//                    [BarsState.LastBarRemoved] = false,
//                    [BarsState.BarClosed] = false,
//                    [BarsState.FirstTick] = false,
//                    [BarsState.PriceChanged] = false,
//                    [BarsState.Tick] = false
//                });
//            }

//            if (ShowLogServiceLabel)
//                LogServiceLabel = "[" + LogServiceLabel + "]: ";
//            else
//                LogServiceLabel = string.Empty;

//            _isInitialized = true;

//            OnInit();
//        }

//        private bool GetIsClosed(int barsInProgress)    => !IsOutOfRange(barsInProgress) && _states[barsInProgress][BarsState.BarClosed];
//        private bool GetHasNewTick(int barsInProgress)  => !IsOutOfRange(barsInProgress) && Calculate == Calculate.OnEachTick && _states[barsInProgress][BarsState.Tick];
//        private bool GetIsRemoved(int barsInProgress)   => !IsOutOfRange(barsInProgress) && _states[barsInProgress][BarsState.LastBarRemoved];
//        private bool GetHasNewPrice(int barsInProgress) => Calculate != Calculate.OnBarClose && _states[barsInProgress][BarsState.PriceChanged];
//        private bool GetIsFirstTick(int barsInProgress) => Calculate != Calculate.OnBarClose && _states[barsInProgress][BarsState.FirstTick];

//        private void ExecuteMethods(List<Action> methods)
//        {
//            if (methods == null || methods.Count == 0) 
//                return;  
            
//            for (int i = 0; i < methods.Count; i++)
//                methods[i]();
//        }

//        private void InitializeArray(int[] array, int value)
//        {
//            for (int i = 0; i < array.Length; i++)
//                array[i] = value;
//        }
//        private void InitializeArray(double[] array, double value)
//        {
//            for (int i = 0; i < array.Length; i++)
//                array[i] = value;
//        }

//        private bool IsOutOfRange(int barsInProgress)
//        {
//            if (barsInProgress < 0 || barsInProgress >= Count)
//                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
//            return true;
//        }

//        private void SetStateProperties(int barsInProgress, bool noneState, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
//        {
//            _states[barsInProgress][BarsState.None] = noneState;
//            _states[barsInProgress][BarsState.LastBarRemoved] = isLastBarRemoved;
//            _states[barsInProgress][BarsState.BarClosed] = isBarClosed;
//            _states[barsInProgress][BarsState.FirstTick] = isFirstTick;
//            _states[barsInProgress][BarsState.PriceChanged] = isPriceChanged;
//            _states[barsInProgress][BarsState.Tick] = isNewTick;
//        }
//        private void ResetStates(int barsInProgress)
//        {
//            SetStateProperties(
//                barsInProgress: barsInProgress,
//                noneState: false,
//                isLastBarRemoved: false,
//                isBarClosed: false,
//                isFirstTick: false,
//                isPriceChanged: false,
//                isNewTick: false
//                );
//            _logLines.Clear();
//        }
//        private void SetStateValue(int barsInProgress, BarsState state, bool value)
//        {
//            _states[barsInProgress][state] = value;
//            if (state.ToLogLevel() >= MinLogLevel)
//                _logLines.Add(state.ToString());
//        }
//    }
//}
