using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseNinjascriptService<BarsOptions>, IBarsService
    {
        #region Consts

        //public const string Series = "SERIES";
        public const string ServicesDefaultName = "DEFAULT";

        #endregion

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
        private readonly IBarsCacheService _cache;
        // Services
        private IList<IBarUpdateService> _services;
        private Dictionary<string, IBarUpdateService> _svcs;

        #endregion

        #region Public properties

        public int Index { get; protected set; } = 0;
        public int CachePeriod => Options.CacheOptions.Period;
        public int CacheDisplacement => Options.CacheOptions.Displacement;
        public Bar CurrentBar => _cache.GetBar(0);  //_currentBar;
        //public IBarsCacheService Series => (IBarsCacheService)_svcs[Series_];
        public IBarsCacheService Series => Get<BarsCacheService>("Series");
        public IBarUpdateService this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    return null;

                name = name.ToUpper();
                if (!ContainsService(name))
                    return null;

                return _svcs[name];
            }
        }

        public bool IsUpdated => IsConfigured && _barEvents[BarEvent.Updated];
        public bool BarClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool LastBarRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool Tick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool FirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool PriceChanged => IsUpdated && _barEvents[BarEvent.PriceChanged];

        #endregion

        #region Constructors

        public BarsService(NinjaScriptBase ninjascript) : this(ninjascript, null, Cache.DEFAULT_PERIOD, Cache.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService, Cache.DEFAULT_PERIOD, Cache.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, int period) : this(ninjascript, printService, period, Cache.DEFAULT_DISPLACEMENT) { }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, int period, int displacement) : base(ninjascript, printService, null, new BarsOptions())
        {
            Options.CacheOptions.Period = period;
            Options.CacheOptions.Displacement = displacement;
            AddService<BarsCacheService, CacheServiceOptions>("Series", Options.CacheOptions);
        }

        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<BarsOptions> configureOptions) : base(ninjascript, printService, configureOptions) 
        {
            Options = new BarsOptions();
            configureOptions?.Configure(Options);
            AddService<BarsCacheService,CacheServiceOptions>("Series",Options.CacheOptions);
        }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, Action<BarsOptions> configureOptions) : base(ninjascript, printService, configureOptions,null)
        {
            Options = new BarsOptions();
            configureOptions?.Invoke(Options);
            AddService<BarsCacheService, CacheServiceOptions>("Series", Options.CacheOptions);
        }
        public BarsService(NinjaScriptBase ninjascript, IPrintService printService, BarsOptions options) : base(ninjascript, printService, null,options)
        {
            Options = options ?? new BarsOptions();
            AddService<BarsCacheService, CacheServiceOptions>("Series", Options.CacheOptions);
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
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (_services != null && _services.Count > 0)
                foreach (var service in _services)
                    service.DataLoaded();

            isDataLoaded = Index > 0 && Index < Ninjascript.BarsArray.Length;

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

        public IBarsService AddService<TService, TOptions>(string key, Action<TOptions> configureOptions = null,object input1 = null, object input2 = null)
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new()
        {
            TOptions options = new TOptions();
            configureOptions?.Invoke(options);

            IBarUpdateService service = CreateBarUpdateService<TService, TOptions>(options,input1,input2) ?? 
                throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = service.Name;

            key = key.ToUpper();

            if (!ContainsService<TService>(key))
                Add(key, service);

            return this;
        }
        public IBarsService AddService<TService, TOptions>(string key,TOptions options, object input1 = null, object input2 = null)
            where TService : IBarUpdateService
            where TOptions : BarUpdateServiceOptions, new()
        {
            if (options == null)
                options = new TOptions() { Period = CachePeriod, Displacement = CacheDisplacement };

            IBarUpdateService service = CreateBarUpdateService<TService, TOptions>(options,input1,input2) ??
                throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = service.Name;

            key = key.ToUpper();

            if (!ContainsService<TService>(key))
                Add(key, service);

            return this;    
        }
        public IBarsService AddService<TService>(string key,TService service)
            where TService : IBarUpdateService
        {
            if (service == null)
                throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = service.Name;

            key = key.ToUpper();

            if (!ContainsService<TService>(key))
                Add(key,service);

            return this;    
        }
        public TService Get<TService>(string key = "")
            where TService : class, IBarUpdateService
        {
            if (_svcs == null || _svcs.Count == 0)
                return null;

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = ServicesDefaultName;
            key = key.ToUpper();

            //// List
            //foreach (var service in _services)
            //    if (service.GetType() == typeof(TService))
            //        if(key == ServicesDefaultName || service.Name.ToUpper() == key)
            //            return (TService)service;

            // Dictionary
            foreach (var service in _svcs)
                if (service.Value.GetType() == typeof(TService))
                    if (key == ServicesDefaultName || service.Key == key)
                        return (TService)service.Value;

            return null;
        }
        public CacheService<TCache> GetCache<TCache>(string key = "")
            where TCache : class, IBarUpdateCache
        {
            if (_svcs == null || _svcs.Count == 0)
                return null;

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = ServicesDefaultName;
            key = key.ToUpper();

            // Dictionary
            foreach (var service in _svcs)
                if (service.Value.GetType() == typeof(CacheService<TCache>))
                    if (key == ServicesDefaultName || service.Key == key)
                        return (CacheService<TCache>)service.Value;

            return null;
        }
        public IBarUpdateService Get(string key)
        {
            if (_svcs == null || _svcs.Count == 0)
                return null;

            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                key = ServicesDefaultName;

            if (key == ServicesDefaultName)
                return null;

            key = key.ToUpper();

            //// List
            //foreach (var service in _services)
            //    if(service.Name.ToUpper() == key)
            //        return service;

            // Dictionary
            foreach (var service in _svcs)
                if (service.Key == key)
                    return service.Value;

            return null;
        }

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Protected methods

        protected void Add(string key,IBarUpdateService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            if (IsConfigured)
                throw new Exception($"{Name} must be added before 'IBarsService' has been configured.");

            //// List
            //if (_services == null)
            //    _services = new List<IBarUpdateService>();
            // No se puede asignar un nombre específico.
            _services.Add(service);

            // Dictionary
            if (_svcs == null)
                _svcs = new Dictionary<string, IBarUpdateService>();
            key = string.IsNullOrEmpty(key) ? service.Name : string.IsNullOrWhiteSpace(key) ? service.Name : key;
            _svcs.Add(key.ToUpper(), service);
        }
        protected void ExecuteServices()
        {
            //// List
            //foreach (var service in _services)
            //    service.Update();

            // Dictionary
            foreach(var service in _svcs)
                service.Value.Update();
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
        private IBarUpdateService CreateBarUpdateService<TService,TOptions>(TOptions options, object input1, object input2)
            where TOptions : BarUpdateServiceOptions, new()
        {
            IBarUpdateService service;
            Type type = typeof(TService);
            if (options == null)
                options = new TOptions()
                {
                    Period = CachePeriod,
                    Displacement = CacheDisplacement,
                    LengthOfRemovedValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY,
                    BarsIndex = 0,
                };
            if ((type == typeof(BarsCacheService) || type == typeof(IBarsCacheService)))
                service = new BarsCacheService(this);
            else if (type == typeof(CacheService<MaxCache>))
                if (options is CacheServiceOptions maxOptions) service = new CacheService<MaxCache>(this, input1, maxOptions);
                else service = new CacheService<MaxCache>(this, input1);
            else if (type == typeof(CacheService<MinCache>))
                if (options is CacheServiceOptions minOptions) service = new CacheService<MinCache>(this, input1, minOptions);
                else service = new CacheService<MinCache>(this, input1);
            else
                throw new NotImplementedException($"The {type.Name} has not been created. Krumon...implemented it!!!!");

            return service;
        }
        private bool ContainsService<TService>(string name)
            where TService : IBarUpdateService
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                name = ServicesDefaultName;
            name = name.ToUpper();

            // List
            if (_services == null || _services.Count == 0)
                return false;

            foreach (var service in _services)
                if (service.GetType() == typeof(TService))
                    if (name == ServicesDefaultName || service.Name.ToUpper() == name) 
                        return true;

            // Dictionary
            if (_svcs == null || _svcs.Count == 0)
                return false;

            foreach (var service in _svcs)
                if (service.Value.GetType() == typeof(TService))
                    if (name == ServicesDefaultName || service.Key == name)
                        return true;
            return false;
        }
        private bool ContainsService(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                name = ServicesDefaultName;
            name = name.ToUpper();

            // List
            if (_services == null || _services.Count == 0)
                return false;

            foreach (var service in _services)
                if (name == ServicesDefaultName || service.Name.ToUpper() == name )
                    return true;

            // Dictionary
            if (_svcs == null || _svcs.Count == 0)
                return default;

            foreach (var service in _svcs)
                if (name == ServicesDefaultName || service.Key == name)
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
