using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsManager : BaseNinjascriptService<BarsManagerOptions>, IBarsManager
    {
        #region Consts

        public const string ServicesDefaultName = "DEFAULT";

        #endregion

        #region Private members

        //// Data Series information
        //private TradingHoursCode _tradingHoursCode;
        //private MarketDataType _marketDataType;
        //private InstrumentCode _instrumentCode;
        //private TimeFrame _timeFrame;

        //// Bar update control
        //private int _lastBarIdx;
        //private int _currentBarIdx;
        //private double _lastPrice;
        //private double _currentPrice;
        private bool _isRunning;
        //// Events
        //private Dictionary<BarEvent, bool> _barEvents;
        //// Logging
        //private List<string> _logLines;
        // DataSeries
        private readonly BarsService _primaryDataSeries;
        private readonly BarsServiceCollection _dataSeries;
        private DataSeriesInfo[] _dataSeriesInfo;

        //// Services
        //private IList<IIndicatorService> _indicators;
        //private IList<IStatisticsService> _stats;
        //private IList<IFilterService> _filters;

        //private IList<IBarUpdateService> _services;
        //private Dictionary<string, IBarUpdateService> _svcs;

        #endregion

        #region Public properties

        //public int Index { get; protected set; }
        //public string TradingHoursName => _primaryDataSeries?.TradingHoursName;
        //public NinjaTrader.Data.MarketDataType MarketDataType => _primaryDataSeries == null ? NinjaTrader.Data.MarketDataType.Unknown : _primaryDataSeries.MarketDataType;
        //public string InstrumentName => _primaryDataSeries?.InstrumentName;
        //public NinjaTrader.Data.BarsPeriod BarsPeriod => _primaryDataSeries?.BarsPeriod;

        public int DefaultCachesCapacity { get; protected set; }
        public int DefaultRemovedCachesCapacity { get; protected set; }

        public CurrentBarSeries CurrentBar => _primaryDataSeries.CurrentBar;
        public TimeSeries Time => _primaryDataSeries.Time;
        public PriceSeries Open => _primaryDataSeries.Open;
        public PriceSeries High => _primaryDataSeries.High;
        public PriceSeries Low => _primaryDataSeries.Low;
        public PriceSeries Close => _primaryDataSeries.Close;
        public VolumeSeries Volume => _primaryDataSeries.Volume;
        public TickSeries Ticks => _primaryDataSeries.Tick;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }

        public IBarsService this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    return null;

                string upperName = name.ToUpper();
                if (upperName == "0" || upperName == "PRIMARY" || upperName == "DEFAULT")
                    return _primaryDataSeries;

                if (_dataSeries == null || _dataSeries.Count == 0 || !_dataSeries.ContainsKey(name))
                    return null;

                return _dataSeries[name];
            }
        }
        public IBarsService this[int idx]
        {
            get
            {
                if (idx == 0)
                    return _primaryDataSeries;

                if (_dataSeries == null || _dataSeries.Count == 0)
                    return null;

                if (idx < 0 || idx >= _dataSeries.Count)
                    return null;

                int count = 0;
                foreach (var dataSeries in _dataSeries)
                {
                    if (idx == count)
                        return dataSeries;
                    count++;
                }
                return null;
            }
        }

        public bool IsUpdated => _primaryDataSeries.IsUpdated;
        public bool IsClosed => _primaryDataSeries.IsClosed;
        public bool LastBarIsRemoved => _primaryDataSeries.LastBarIsRemoved;
        public bool IsTick => _primaryDataSeries.IsTick;
        public bool IsFirstTick => _primaryDataSeries.IsFirstTick;
        public bool IsPriceChanged => _primaryDataSeries.IsPriceChanged;

        #endregion

        #region Constructors

        public BarsManager(NinjaScriptBase ninjascript, IPrintService printService, Action<BarsManagerOptions> configureOptions) : base(ninjascript, printService, configureOptions,null)
        {
            Options = new BarsManagerOptions();
            configureOptions?.Invoke(Options);
            _primaryDataSeries = new BarsService(this);
            string key = _primaryDataSeries.InstrumentName + "_" + _primaryDataSeries.BarsPeriod.ToShortString();
            _dataSeries.Add(key, _primaryDataSeries);
        }
        public BarsManager(NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : base(ninjascript, printService, null,options)
        {
            Options = options ?? new BarsManagerOptions();
            _primaryDataSeries = new BarsService(this);
            string key = _primaryDataSeries.InstrumentName + "_" + _primaryDataSeries.BarsPeriod.ToShortString();
            _dataSeries.Add(key,_primaryDataSeries);
        }

        #endregion

        #region Implementation

        public override string Name => $"Bars[{_primaryDataSeries.InstrumentName},{_primaryDataSeries.BarsPeriod.ToShortString()}]";
        public override string Key => throw new NotImplementedException();
        public DataSeriesInfo[] DataSeries => throw new NotImplementedException();
        public int Index => throw new NotImplementedException();
        public int Capacity => throw new NotImplementedException();
        public int RemovedCacheCapacity => throw new NotImplementedException();

        internal override void Configure(out bool isConfigured)
        {
            _dataSeries.Configure();
            isConfigured = _dataSeries.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (Ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == _dataSeries[i].InstrumentName)
                    if (Ninjascript.BarsPeriods[i] == _dataSeries[i].BarsPeriod &&
                        Ninjascript.BarsArray[i].TradingHours.Name == _dataSeries[i].TradingHoursName
                        )
                    {
                        //Index = i;
                        break;
                    }
            isDataLoaded = Index != -1;

            //if (!isDataLoaded)
            //    return;

            //// Configure services
            //if (_services != null && _services.Count > 0)
            //    foreach (var service in _services)
            //    {
            //        service.DataLoaded();
            //        if (isDataLoaded)
            //            isDataLoaded = service.IsDataLoaded;
            //    }
        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                Update();
            else
            {
                if (!IsConfigureAll)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                Update();
            }
        }
        public override string ToLogString()
        {
            //if (_logLines == null || _logLines.Count == 0)
            //    return string.Empty;
            //string stateText = string.Empty;
            //for (int i = 0; i < _logLines.Count; i++)
            //    stateText += _logLines[i];

            return string.Empty; // stateText;
        }
        public Bar GetBar(int barsAgo) => _primaryDataSeries.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _primaryDataSeries.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _primaryDataSeries.GetBars(barsAgo, period);

        #endregion

        #region Public methods

        //public IBarsService AddService<TService, TOptions>(string key, Action<TOptions> configureOptions = null,object input1 = null, object input2 = null)
        //    where TService : IBarUpdateService
        //    where TOptions : BarUpdateServiceOptions, new()
        //{
        //    TOptions options = new TOptions();
        //    configureOptions?.Invoke(options);

        //    IBarUpdateService service = CreateBarUpdateService<TService, TOptions>(options,input1,input2) ?? 
        //        throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = service.Name;

        //    key = key.ToUpper();

        //    if (!ContainsService<TService>(key))
        //        Add(key, service);

        //    return this;
        //}
        //public IBarsService AddService<TService, TOptions>(string key,TOptions options, object input1 = null, object input2 = null)
        //    where TService : IBarUpdateService
        //    where TOptions : BarUpdateServiceOptions, new()
        //{
        //    if (options == null)
        //        options = new TOptions() {  };

        //    IBarUpdateService service = CreateBarUpdateService<TService, TOptions>(options,input1,input2) ??
        //        throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = service.Name;

        //    key = key.ToUpper();

        //    if (!ContainsService<TService>(key))
        //        Add(key, service);

        //    return this;    
        //}
        //public IBarsService AddService<TService>(string key,TService service)
        //    where TService : IBarUpdateService
        //{
        //    if (service == null)
        //        throw new Exception("El servicio no se puede añadir porque su valor es 'NULL'.");

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = service.Name;

        //    key = key.ToUpper();

        //    if (!ContainsService<TService>(key))
        //        Add(key,service);

        //    return this;    
        //}
        //public TService Get<TService>(string key = "")
        //    where TService : class, IBarUpdateService
        //{
        //    if (_svcs == null || _svcs.Count == 0)
        //        return null;

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = ServicesDefaultName;
        //    key = key.ToUpper();

        //    //// List
        //    //foreach (var service in _services)
        //    //    if (service.GetType() == typeof(TService))
        //    //        if(key == ServicesDefaultName || service.Name.ToUpper() == key)
        //    //            return (TService)service;

        //    // Dictionary
        //    foreach (var service in _svcs)
        //        if (service.Value.GetType() == typeof(TService))
        //            if (key == ServicesDefaultName || service.Key == key)
        //                return (TService)service.Value;

        //    return null;
        //}
        //public CacheService<TCache> GetCache<TCache>(string key = "")
        //    where TCache : class, IBarUpdateCache
        //{
        //    if (_svcs == null || _svcs.Count == 0)
        //        return null;

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = ServicesDefaultName;
        //    key = key.ToUpper();

        //    // Dictionary
        //    foreach (var service in _svcs)
        //        if (service.Value.GetType() == typeof(CacheService<TCache>))
        //            if (key == ServicesDefaultName || service.Key == key)
        //                return (CacheService<TCache>)service.Value;

        //    return null;
        //}
        //public IBarUpdateService Get(string key)
        //{
        //    if (_svcs == null || _svcs.Count == 0)
        //        return null;

        //    if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        //        key = ServicesDefaultName;

        //    if (key == ServicesDefaultName)
        //        return null;

        //    key = key.ToUpper();

        //    //// List
        //    //foreach (var service in _services)
        //    //    if(service.Name.ToUpper() == key)
        //    //        return service;

        //    // Dictionary
        //    foreach (var service in _svcs)
        //        if (service.Key == key)
        //            return service.Value;

        //    return null;
        //}

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Protected methods

        //protected void Add(string key,IBarUpdateService service)
        //{
        //    if (service == null) throw new ArgumentNullException(nameof(service));

        //    if (IsConfigureAll)
        //        throw new Exception($"{Name} must be added before 'IBarsService' has been configured.");

        //    //// List
        //    //if (_services == null)
        //    //    _services = new List<IBarUpdateService>();
        //    // No se puede asignar un nombre específico.
        //    _services.Add(service);

        //    // Dictionary
        //    if (_svcs == null)
        //        _svcs = new Dictionary<string, IBarUpdateService>();
        //    key = string.IsNullOrEmpty(key) ? service.Name : string.IsNullOrWhiteSpace(key) ? service.Name : key;
        //    _svcs.Add(key.ToUpper(), service);
        //}
        //protected void ExecuteServices()
        //{
        //    //// List
        //    //foreach (var service in _services)
        //    //    service.Update();

        //    // Dictionary
        //    foreach(var service in _svcs)
        //        service.Value.Update();
        //}

        #endregion

        #region Private methods

        private void Update()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != Index)
                return;

            //// Check FILTERS

            //ResetBarsEvents();
            //_currentBarIdx = Ninjascript.CurrentBars[Index];
            //_currentPrice = Ninjascript.Closes[Index][0];

            //// LasBarRemoved
            //if (Ninjascript.BarsArray[Index].BarsType.IsRemoveLastBarSupported && _currentBarIdx < _lastBarIdx)
            //{
            //    SetBarsEventValue(BarEvent.Removed, true);
            //    OnLastBarRemoved();
            //}
            //else
            //{
            //    // BarClosed Or First tick success
            //    if (_currentBarIdx != _lastBarIdx)
            //    {
            //        SetBarsEventValue(BarEvent.Closed, true);
            //        OnBarClosed();

            //        if (Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose)
            //        {
            //            SetBarsEventValue(BarEvent.FirstTick, true);
            //            if (_lastPrice.ApproxCompare(_currentPrice) != 0)
            //                SetBarsEventValue(BarEvent.PriceChanged, true);

            //            if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
            //                SetBarsEventValue(BarEvent.Tick, true);
            //        }
            //    }

            //    // Tick Success
            //    else
            //    {
            //        if (_lastPrice.ApproxCompare(_currentPrice) != 0)
            //        {
            //            SetBarsEventValue(BarEvent.PriceChanged, true);
            //            OnPriceChanged();
            //        }
            //        if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
            //        {
            //            SetBarsEventValue(BarEvent.Tick, true);
            //            OnEachTick();
            //        }
            //    }
            //}
            //SetBarsEventValue(BarEvent.Updated, true);
            //Log();
            //// Calculate Stats
            //ExecuteServices();
            //SetBarsEventValue(BarEvent.Updated, false);
            //_lastBarIdx = _currentBarIdx;
            //_lastPrice = _currentPrice;
        }

        //private void SetBarsEvents(bool updated, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        //{
        //    _barEvents[BarEvent.Updated] = updated;
        //    _barEvents[BarEvent.Removed] = isLastBarRemoved;
        //    _barEvents[BarEvent.Closed] = isBarClosed;
        //    _barEvents[BarEvent.FirstTick] = isFirstTick;
        //    _barEvents[BarEvent.PriceChanged] = isPriceChanged;
        //    _barEvents[BarEvent.Tick] = isNewTick;
        //}
        //private void ResetBarsEvents()
        //{
        //    SetBarsEvents(
        //        updated: false,
        //        isLastBarRemoved: false,
        //        isBarClosed: false,
        //        isFirstTick: false,
        //        isPriceChanged: false,
        //        isNewTick: false
        //        );
        //    _logLines.Clear();
        //}
        //private void SetBarsEventValue(BarEvent barsEvent, bool value)
        //{
        //    _barEvents[barsEvent] = value;

        //    if (PrintService == null)
        //        return;

        //    if (PrintService.IsLogLevelsEnable(Core.Logging.LogLevel.Information) && Options.IsLogEnable)
        //        _logLines.Add(barsEvent.ToString());
        //}
        //private IBarUpdateService CreateBarUpdateService<TService,TOptions>(TOptions options, object input1, object input2)
        //    where TOptions : BarUpdateServiceOptions, new()
        //{
        //    IBarUpdateService service;
        //    Type type = typeof(TService);
        //    if (options == null)
        //        options = new TOptions()
        //        {
        //            //Period = CachePeriod,
        //            //Displacement = CacheDisplacement,
        //            //LengthOfRemovedValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY,
        //            BarsIndex = 0,
        //        };
        //    if ((type == typeof(BarsCacheService) || type == typeof(IBarsCacheService)))
        //        service = new BarsCacheService(this);
        //    else if (type == typeof(CacheService<MaxCache>))
        //        if (options is CacheServiceOptions maxOptions) service = new CacheService<MaxCache>(this, input1, maxOptions);
        //        else service = new CacheService<MaxCache>(this, input1);
        //    else if (type == typeof(CacheService<MinCache>))
        //        if (options is CacheServiceOptions minOptions) service = new CacheService<MinCache>(this, input1, minOptions);
        //        else service = new CacheService<MinCache>(this, input1);
        //    else
        //        throw new NotImplementedException($"The {type.Name} has not been created. Krumon...implemented it!!!!");

        //    return service;
        //}
        //private bool ContainsService<TService>(string name)
        //    where TService : IBarUpdateService
        //{
        //    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        //        name = ServicesDefaultName;
        //    name = name.ToUpper();

        //    // List
        //    if (_services == null || _services.Count == 0)
        //        return false;

        //    foreach (var service in _services)
        //        if (service.GetType() == typeof(TService))
        //            if (name == ServicesDefaultName || service.Name.ToUpper() == name) 
        //                return true;

        //    // Dictionary
        //    if (_svcs == null || _svcs.Count == 0)
        //        return false;

        //    foreach (var service in _svcs)
        //        if (service.Value.GetType() == typeof(TService))
        //            if (name == ServicesDefaultName || service.Key == name)
        //                return true;
        //    return false;
        //}
        //private bool ContainsService(string name)
        //{
        //    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        //        name = ServicesDefaultName;
        //    name = name.ToUpper();

        //    // List
        //    if (_services == null || _services.Count == 0)
        //        return false;

        //    foreach (var service in _services)
        //        if (name == ServicesDefaultName || service.Name.ToUpper() == name )
        //            return true;

        //    // Dictionary
        //    if (_svcs == null || _svcs.Count == 0)
        //        return default;

        //    foreach (var service in _svcs)
        //        if (name == ServicesDefaultName || service.Key == name)
        //            return true;
        //    return false;
        //}

        #endregion

    }
}
