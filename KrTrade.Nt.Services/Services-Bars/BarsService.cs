using KrTrade.Nt.Core.Bars;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseNinjascriptService<BarsOptions>, IBarsService
    {

        #region Private members

        private int _lastBarIdx;
        private int _currentBarIdx;
        private double _lastPrice;
        private double _currentPrice;
        private bool _isRunning;
        // Events
        private Dictionary<BarEvent, bool> _barEvents;
        // Logging
        private List<string> _logLines;
        // Cache
        private readonly IBarsCache _cache;
        // Services
        private IList<IBarUpdateService> _services;

        #endregion

        #region Public properties

        public int Index { get; protected set; } = 0;
        public int Period => Options.Period;
        public int Displacement => Options.Displacement;
        public Bar CurrentBar => _cache.GetBar(0);  //_currentBar;
        public IBarsSeries Series => _cache;

        public bool IsUpdated => IsConfigured && _barEvents[BarEvent.Updated];
        public bool BarClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool LastBarRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool Tick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool FirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool PriceChanged => IsUpdated && _barEvents[BarEvent.PriceChanged];

        #endregion

        #region Constructors

        public BarsService(NinjaScriptBase ninjascript) : this(ninjascript, null, BarsOptions.DEFAULT_PERIOD, BarsOptions.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService, BarsOptions.DEFAULT_PERIOD, BarsOptions.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, int period) : this(ninjascript, printService, period, BarsOptions.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, int period, int displacement) : base(ninjascript, printService, null, new BarsOptions(period, displacement))
        {
            Options = new BarsOptions(period, displacement);
            _cache = new BarsCache(Ninjascript, Options.Period, Options.Displacement);
            Add((IBarUpdateService)_cache);
        }
        
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<BarsOptions> configureOptions) : base(ninjascript, printService, configureOptions) 
        {
            _cache = new BarsCache(Ninjascript, Options.Period, Options.Displacement);
            Add((IBarUpdateService)_cache);
        }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, Action<BarsOptions> configureOptions) : base(ninjascript, printService, configureOptions,null)
        {
            _cache = new BarsCache(Ninjascript, Options.Period, Options.Displacement);
            Add((IBarUpdateService)_cache);
        }

        #endregion

        #region Implementation

        public override string Name => $"Bars[{Index}](services:{_services?.Count})";
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

            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (_services != null && _services.Count > 0)
                foreach (var service in _services)
                    service.DataLoaded();

            isDataLoaded = Index > 0 && Index < Ninjascript.BarsArray.Length;

        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                Update();
            else
            {
                if (!IsConfigured)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                Update();
            }
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
        public Bar GetBar(int barsAgo) => _cache.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _cache.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _cache.GetBars(barsAgo, period);

        #endregion

        #region Public methods

        public IBarsService AddService<TService, TOptions>(Action<TOptions> configureOptions)
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new()
        {
            TOptions options = new TOptions();
            configureOptions?.Invoke(options);
            options.Period = Period;
            options.Displacement = Displacement;

            IBarUpdateService service = GetService<TService, TOptions>(options) ?? 
                throw new Exception("El servicio no se ha añadido porque el valor obtenido para añadir ha sido null.");
            
            if (!ContainsService<TService, TOptions>(options))
                Add(service);

            return this;
        }
        public IBarsService AddService<TService, TOptions>(TOptions options = null)
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new()
        {
            if (options == null)
                options = new TOptions() { Period = Period, Displacement = Displacement };

            IBarUpdateService service = GetService<TService, TOptions>(options) ??
                throw new Exception("El servicio no se ha añadido porque el valor obtenido para añadir ha sido null.");

            if (!ContainsService<TService, TOptions>(options))
                Add(service);

            return this;    
        }

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Protected methods

        protected void Add(IBarUpdateService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            if (IsConfigured)
                throw new Exception($"{Name} must be added before 'IBarsService' has been configured.");

            if (_services == null)
                _services = new List<IBarUpdateService>();

            //_cacheCapacity = Math.Max(_cacheCapacity, service.Displacement + service.Period);
            _services.Add(service);
        }
        protected void ExecuteServices()
        {
            foreach (var service in _services)
                service.Update();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != Index)
                return;

            // Check FILTERS

            ResetBarsEvents();
            _currentBarIdx = Ninjascript.CurrentBars[Index];
            _currentPrice = Ninjascript.Closes[Index][0];

            // LasBarRemoved
            if (Ninjascript.BarsArray[Index].BarsType.IsRemoveLastBarSupported && _currentBarIdx < _lastBarIdx)
            {
                SetBarsEventValue(BarEvent.Removed, true);
                OnLastBarRemoved();
            }
            else
            {
                // BarClosed Or First tick success
                if (_currentBarIdx != _lastBarIdx)
                {
                    SetBarsEventValue(BarEvent.Closed, true);
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
            // Calculate Stats
            ExecuteServices();
            SetBarsEventValue(BarEvent.Updated, false);
            _lastBarIdx = _currentBarIdx;
            _lastPrice = _currentPrice;
        }
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
        private IBarUpdateService GetService<TService,TOptions>(TOptions options)
            where TOptions : BarUpdateServiceOptions, new()
        {
            IBarUpdateService service = null;
            Type type = typeof(TService);
            if (options == null)
                options = new TOptions()
                {
                    Period = Period,
                    Displacement = Displacement,
                };
            else
            {
                options.Period = Period;
                options.Displacement = Displacement;
            }
            if (type == typeof(BarsCacheService) && options is CacheOptions cacheOptions)
                service = new BarsCacheService(this, cacheOptions);

            return service;
        }
        private bool ContainsService<TService,TOptions>(TOptions options)
        {
            if (_services == null || _services.Count == 0)
                return false;
            foreach (var service in _services)
                if (service.GetType() == typeof(TService))
                    if (service.Options.Equals(options)) 
                        return true;

            return false;
        }

        //protected void BarCopy(Bar bar1, Bar bar2)
        //{
        //    if (bar1 == null || bar2 == null)
        //        throw new ArgumentNullException(nameof(bar1));

        //    bar1.Idx = bar2.Idx;
        //    bar1.Open = bar2.Open;
        //    bar1.High = bar2.High;
        //    bar1.Low = bar2.Low;
        //    bar1.Close = bar2.Close;
        //    bar1.Volume = bar2.Volume;
        //    bar1.Time = bar2.Time;
        //    // TODO: Copiar todas las propiedades
        //}
        //protected void BarUpdate(Bar bar, int parentBarIdx, int displacement)
        //{
        //    bar.Idx = GetBarIdx(parentBarIdx, displacement);
        //    bar.Open = GetOpen(parentBarIdx, displacement);
        //    bar.High = GetHigh(parentBarIdx, displacement);
        //    bar.Low = GetLow(parentBarIdx, displacement);
        //    bar.Close = GetClose(parentBarIdx, displacement);
        //    bar.Volume = GetVolume(parentBarIdx, displacement);
        //    bar.Time = GetTime(parentBarIdx, displacement);
        //    bar.Ticks = Ninjascript.BarsArray[Index].TickCount;
        //}

        #endregion

    }
}
