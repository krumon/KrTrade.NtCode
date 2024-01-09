using KrTrade.Nt.Core.Bars;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public abstract class BarsService : BaseNinjascriptService<BarsOptions>, IBarsService
    {

        #region Private members

        private int _lastBarIdx;
        private int _currentBarIdx;
        private double _lastPrice;
        private double _currentPrice;
        // Events
        private Dictionary<BarEvent, bool> _barEvents;
        // Logging
        private List<string> _logLines;
        // Cache
        private int _cacheCapacity;
        private BarsCache _cache;
        private Bar _currentBar;
        // Services
        private IList<IBarUpdateService> _services;
        private bool _isServicesConfigured;
        private bool _isServicesDataLoaded;

        #endregion

        #region Consts

        public const int DEFAULT_CAPACITY = 20;

        #endregion
        
        #region Public properties

        public int ParentBarsIdx { get; protected set; } = 0;
        public BarsCache Cache => _cache;
        public Bar CurrentBar => _currentBar;

        public bool IsUpdated => IsConfigured && _barEvents[BarEvent.Updated];
        public bool IsLastBarClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool IsLastBarRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool NewTick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool FirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool NewPrice => IsUpdated && _barEvents[BarEvent.PriceChanged];

        #endregion

        #region Constructors

        protected BarsService(NinjaScriptBase ninjascript) : this(ninjascript, null, null, DEFAULT_CAPACITY) { }
        protected BarsService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService, null, DEFAULT_CAPACITY) { }
        protected BarsService(NinjaScriptBase ninjascript, IPrintService printService, int cacheCapacity) : this(ninjascript, printService, null, cacheCapacity) { }
        protected BarsService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<BarsOptions> configureOptions) : this(ninjascript, printService, configureOptions, DEFAULT_CAPACITY) { }
        protected BarsService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<BarsOptions> configureOptions, int cacheCapacity) : base(ninjascript, printService, configureOptions) 
        { 
            _cacheCapacity = cacheCapacity;
            _cache = new BarsCache(_cacheCapacity);
            Add(_cache);
        }

        #endregion

        #region Implementation

        public override string Name => $"Bars[{ParentBarsIdx}](services:{_services?.Count})";
        public IList<IBarUpdateService> Services => _services;

        internal override void Configure(out bool isConfigured)
        {
            _barEvents = new Dictionary<BarEvent, bool>()
            {
                [BarEvent.Updated] = false,
                [BarEvent.Removed] = false,
                [BarEvent.Closed] = false,
                [BarEvent.FirstTick] = false,
                [BarEvent.PriceChanged] = false,
                [BarEvent.Tick] = false
            };

            _logLines = new List<string>();
            _lastBarIdx = int.MinValue;
            _currentBarIdx = int.MinValue;
            _lastPrice = double.MinValue;
            _currentPrice = double.MinValue;

            if (_services != null && _services.Count > 0)
                foreach (var service in _services)
                    service.Configure();

            _isServicesConfigured = true;
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (_services != null && _services.Count > 0)
                foreach (var service in _services)
                    service.DataLoaded();

            _isServicesDataLoaded = true;
            isDataLoaded = ParentBarsIdx > 0 && ParentBarsIdx < Ninjascript.BarsArray.Length;

        }

        public void OnBarUpdate()
        {
            if (!IsConfigured)
                LoggingHelpers.ThrowIsNotConfigureException(Name);

            if (!IsInRunningStates())
                LoggingHelpers.ThrowOutOfRunningStatesException(Name);

            Update();
        }

        public void Update()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != ParentBarsIdx)
                return;

            ResetBarsEvents();
            _currentBarIdx = Ninjascript.CurrentBars[ParentBarsIdx];
            _currentPrice = Ninjascript.Closes[ParentBarsIdx][0];

            // LasBarRemoved
            if (Ninjascript.BarsArray[ParentBarsIdx].BarsType.IsRemoveLastBarSupported && _currentBarIdx < _lastBarIdx)
            {
                SetBarsEventValue(BarEvent.Removed, true);
                //_cache.RemoveAt(0);
                //if (_cache.Count == _cache.Capacity -1)
                //{
                //    BarDataModel bar = new BarDataModel();
                //    int displacement = _cache.Count - 2;
                //    BarUpdate(bar,ParentBarsIdx,displacement);
                //    _cache.Insert(displacement, bar);
                //}
                OnLastBarRemoved();
            }
            else
            {
                // BarClosed Or First tick success
                if (_currentBarIdx != _lastBarIdx)
                {
                    //_currentBar = new BarDataModel();
                    SetBarsEventValue(BarEvent.Closed, true);
                    //BarUpdate(_currentBar,ParentBarsIdx,0);
                    //_cache.Add(_currentBar);
                    OnBarClosed();

                    if (Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose)
                    {
                        SetBarsEventValue(BarEvent.FirstTick, true);
                        if (_lastPrice.ApproxCompare(_currentPrice) != 0)
                            SetBarsEventValue(BarEvent.PriceChanged, true);

                        if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                            SetBarsEventValue(BarEvent.Tick, true);
                    }
                }

                // Tick Success
                else
                {
                    //BarUpdate(_currentBar, ParentBarsIdx, 0);
                    //_cache.Update(_currentBar);

                    if (_lastPrice.ApproxCompare(_currentPrice) != 0)
                    {
                        SetBarsEventValue(BarEvent.PriceChanged, true);
                        OnPriceChanged();
                    }
                    if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                    {
                        SetBarsEventValue(BarEvent.Tick, true);
                        OnEachTick();
                    }
                }
            }
            SetBarsEventValue(BarEvent.Updated, true);
            Log();
            ExecuteServices();
            SetBarsEventValue(BarEvent.Updated, false);
            _lastBarIdx = _currentBarIdx;
            _lastPrice = _currentPrice;
        }

        public override string ToLogString()
        {
            if (_logLines == null || _logLines.Count == 0)
                return string.Empty;
            string stateText = string.Empty;
            for (int i = 0; i < _logLines.Count; i++)
                stateText += _logLines[i];

            return stateText;
        }

        #endregion

        #region Public methods

        public void Add(IBarUpdateService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            if (_isServicesConfigured || _isServicesDataLoaded)
                throw new Exception($"{Name} must be added before 'IBarsService' has been configured.");

            if (_services == null)
                _services = new List<IBarUpdateService>();

            _cacheCapacity = Math.Max(_cacheCapacity, service.Displacement + service.Period);
            _services.Add(service);
        }

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Protected methods

        protected void BarCopy(Bar bar1, Bar bar2)
        {
            if (bar1 == null || bar2 == null)
                throw new ArgumentNullException(nameof(bar1));

            bar1.Idx = bar2.Idx;
            bar1.Open = bar2.Open;
            bar1.High = bar2.High;
            bar1.Low = bar2.Low;
            bar1.Close = bar2.Close;
            bar1.Volume = bar2.Volume;
            bar1.Time = bar2.Time;
            // TODO: Copiar todas las propiedades
        }
        protected void BarUpdate(Bar bar, int parentBarIdx, int displacement)
        {
            bar.Idx = GetBarIdx(parentBarIdx, displacement);
            bar.Open = GetOpen(parentBarIdx, displacement);
            bar.High = GetHigh(parentBarIdx, displacement);
            bar.Low = GetLow(parentBarIdx, displacement);
            bar.Close = GetClose(parentBarIdx, displacement);
            bar.Volume = GetVolume(parentBarIdx, displacement);
            bar.Time = GetTime(parentBarIdx, displacement);
            bar.Ticks = Ninjascript.BarsArray[ParentBarsIdx].TickCount;
        }
        protected void ExecuteServices()
        {
            foreach (var service in _services)
                service.Update();
        }

        #endregion

        #region Private methods

        private void SetBarsEvents(bool updated, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _barEvents[BarEvent.Updated] = updated;
            _barEvents[BarEvent.Removed] = isLastBarRemoved;
            _barEvents[BarEvent.Closed] = isBarClosed;
            _barEvents[BarEvent.FirstTick] = isFirstTick;
            _barEvents[BarEvent.PriceChanged] = isPriceChanged;
            _barEvents[BarEvent.Tick] = isNewTick;
        }
        private void ResetBarsEvents()
        {
            SetBarsEvents(
                updated: false,
                isLastBarRemoved: false,
                isBarClosed: false,
                isFirstTick: false,
                isPriceChanged: false,
                isNewTick: false
                );
            _logLines.Clear();
        }
        private void SetBarsEventValue(BarEvent barsEvent, bool value)
        {
            _barEvents[barsEvent] = value;

            if (PrintService == null)
                return;

            if (PrintService.IsLogLevelsEnable(Core.Logging.LogLevel.Information) && Options.IsLogEnable)
                _logLines.Add(barsEvent.ToString());
        }

        #endregion

    }
}
