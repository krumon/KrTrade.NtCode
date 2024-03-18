using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Core.FloatingPoint;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseNinjascriptService<BarsServiceOptions>, IBarsService
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
        private readonly IBarsSeriesService _barsCacheSvc;
        // Access
        private Dictionary<int, string> _keys;

        #endregion

        #region Public properties

        public int CacheCapacity { get => Options.CacheCapacity; protected set { Options.CacheCapacity = value; } }
        public int RemovedCacheCapacity {  get => Options.RemovedCacheCapacity; protected set { Options.RemovedCacheCapacity = value; }}
        public int Index { get; protected set; } = -1;
        public bool IsWaitingFirstTick => throw new NotImplementedException();

        public string TradingHoursName {  get; protected set; }
        //{
        //    get => Options.TradringHoursCode.ToName();
        //    protected set { Options.TradringHoursCode = EnumHelpers.ForEach<TradingHoursCode>((th) => th.ToName() == value); }
        //}
        public NinjaTrader.Data.MarketDataType MarketDataType { get; protected set; }
        //{
        //    get => Options.MarketDataType.ToNtMarketDataType();
        //    protected set { Options.MarketDataType = value.ToKrMarketDataType(); }
        //}
        public string InstrumentName { get; protected set; }
        //{
        //    get => Options.InstrumentCode.ToString();
        //    protected set { Options.InstrumentCode = EnumHelpers.ForEach<InstrumentCode>((ic) => ic.ToString() == value); }
        //}
        public NinjaTrader.Data.BarsPeriod BarsPeriod { get; protected set; }
        //{
        //    get => Options.TimeFrame.ToBarsPeriod();
        //    protected set { Options.TimeFrame = value.ToTimeFrame(); }
        //}

        public CurrentBarSeries CurrentBar => _barsCacheSvc.CurrentBar;
        public TimeSeries Time => _barsCacheSvc.Time;
        public PriceSeries Open => _barsCacheSvc.Open;
        public PriceSeries High => _barsCacheSvc.High;
        public PriceSeries Low => _barsCacheSvc.Low;
        public PriceSeries Close => _barsCacheSvc.Close;
        public VolumeSeries Volume => _barsCacheSvc.Volume;
        public TickSeries Tick => _barsCacheSvc.Tick;

        public bool IsUpdated => IsConfigureAll && _barEvents[BarEvent.Updated];
        public bool IsClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool LastBarIsRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool IsTick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool IsFirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool IsPriceChanged => IsUpdated && _barEvents[BarEvent.PriceChanged];

        public override string Name => InstrumentName + "(" + BarsPeriod.ToShortString() + ")";
        public override string ToLogString()
        {
            if (_logLines == null || _logLines.Count == 0)
                return string.Empty;
            string stateText = Name + ": ";
            for (int i = 0; i < _logLines.Count; i++)
            {
                stateText += _logLines[i];
                if ( i < _logLines.Count - 1)
                    stateText += " - ";
            }

            return stateText;
        }

        public SeriesCollection Series { get; private set; }
        public IndicatorCollection Indicators { get; private set; }
        public StatsCollection Stats { get; private set; }
        public FiltersCollection Filters { get; private set; }

        #endregion

        #region Constructors

        public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript) : base(ninjascript) { }
        public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsServiceOptions> configureOptions) : base(ninjascript, printService, configureOptions) { }
        public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceOptions options) : base(ninjascript, printService, options) { }
        public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsServiceOptions> configureOptions, BarsServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

        public BarsService(IBarsManager barsManager) : base(barsManager?.Ninjascript, barsManager?.PrintService)
        {
            _barsCacheSvc = new BarsSeriesService(this);
            Indicators = new IndicatorCollection(this);
            Filters = new FiltersCollection(this);
            Stats = new StatsCollection(this);
        }
        //public BarsService(IBarsMaster barsService, Action<BarsServiceOptions> configureOptions) : base(barsService, configureOptions)
        //{
        //    _barsCacheSvc = new BarsCacheService(barsService);
        //    Indicators = new IndicatorsCollection(barsService);
        //    Filters = new FiltersCollection(barsService);
        //    Stats = new StatsCollection(barsService);
        //}
        //public BarsService(IBarsMaster barsService, BarsServiceOptions options) : base(barsService, options)
        //{
        //    _barsCacheSvc = new BarsCacheService(barsService);
        //    Indicators = new IndicatorsCollection(barsService);
        //    Filters = new FiltersCollection(barsService);
        //    Stats = new StatsCollection(barsService);
        //}

        #endregion

        #region Implementation

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

            //if (Options.InstrumentCode == InstrumentCode.Default)
            //    InstrumentName = Ninjascript.BarsArray[0].Instrument.MasterInstrument.Name;
            //if (Options.TradringHoursCode == TradingHoursCode.Default)
            //    TradingHoursName = Ninjascript.BarsArray[0].TradingHours.Name;

            _barsCacheSvc.Configure();
            Indicators?.Configure();
            Filters?.Configure();
            Stats?.Configure();

            isConfigured = _barsCacheSvc.IsConfigure && Indicators.IsConfigure && Filters.IsConfigure && Stats.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {

            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (Ninjascript.BarsArray[i].Instrument.MasterInstrument.Name == InstrumentName)
                    if (Ninjascript.BarsPeriods[i] == BarsPeriod &&
                        Ninjascript.BarsArray[i].TradingHours.Name == TradingHoursName
                        )
                    {
                        Index = i;
                        break;
                    }
            isDataLoaded = Index != -1;

            if (!isDataLoaded)
                return;

            // Configure services
            _barsCacheSvc.DataLoaded();
            Indicators?.DataLoaded();
            Filters?.DataLoaded();
            Stats?.DataLoaded();

            isDataLoaded = _barsCacheSvc.IsDataLoaded && Indicators.IsDataLoaded && Filters.IsDataLoaded && Stats.IsDataLoaded;
        }

        public void OnBarUpdate()
        {
            if (_isRunning && Ninjascript.BarsInProgress == Index)
                Update();
            else
            {
                if (!IsConfigureAll)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                if (Ninjascript.BarsInProgress == Index)
                    Update();
            }
        }
        public void Update()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != Index)
                return;

            // Check FILTERS
            Filters.Update();

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

            //// Check FILTERS
            //Filters.Update();
            _barsCacheSvc.Update();
            Stats.Update();
            Indicators.Update();

            SetBarsEventValue(BarEvent.Updated, false);
            _lastBarIdx = _currentBarIdx;
            _lastPrice = _currentPrice;
        }
        public void Update(IBarsService updatedBarsSeries)
        {
            throw new NotImplementedException();
        }
        public void MarketData()
        {
            throw new NotImplementedException();
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            throw new NotImplementedException();
        }
        public void MarketDepth()
        {
            throw new NotImplementedException();
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            throw new NotImplementedException();
        }
        public void Render()
        {
            throw new NotImplementedException();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            throw new NotImplementedException();
        }

        public T GetSeries<T>()
        {
            throw new NotImplementedException();
        }
        public Bar GetBar(int barsAgo) => _barsCacheSvc.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barsCacheSvc.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barsCacheSvc.GetBars(barsAgo, period);



        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

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
        private IBarUpdateService CreateBarUpdateService<TService, TOptions>(TOptions options, object input1, object input2)
            where TOptions : BarUpdateServiceOptions, new()
        {
            IBarUpdateService service;
            Type type = typeof(TService);
            if (options == null)
                options = new TOptions()
                {
                    //Period = CachePeriod,
                    //Displacement = CacheDisplacement,
                    //LengthOfRemovedValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY,
                    BarsIndex = 0,
                };
            if ((type == typeof(BarsSeriesService) || type == typeof(IBarsSeriesService)))
                service = new BarsSeriesService(this);
            else if (type == typeof(SeriesService<MaxSeries>))
                if (options is SeriesOptions maxOptions) service = new SeriesService<MaxSeries>(this, input1, maxOptions);
                else service = new SeriesService<MaxSeries>(this, input1);
            else if (type == typeof(SeriesService<MinSeries>))
                if (options is SeriesOptions minOptions) service = new SeriesService<MinSeries>(this, input1, minOptions);
                else service = new SeriesService<MinSeries>(this, input1);
            else
                throw new NotImplementedException($"The {type.Name} has not been created. Krumon...implemented it!!!!");

            return service;
        }

        #endregion
    }
}
