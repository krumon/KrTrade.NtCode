using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Extensions;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseNinjascriptService<BarsServiceOptions>, IBarsService
    {

        #region Consts

        //public const string Series = "SERIES";
        //public const string ServicesDefaultName = "DEFAULT";

        #endregion

        #region Private members

        // Data series info
        private DataSeriesOptions _dataSeriesInfo;
        //internal InstrumentCode InstrumentCode { get => Options.InstrumentCode; set { Options.InstrumentCode = value; } }
        //internal TimeFrame TimeFrame { get => Options.TimeFrame; set { Options.TimeFrame= value; } }
        //internal TradingHoursCode TradingHoursCode { get => Options.TradringHoursCode; set { Options.TradringHoursCode= value; } }
        //internal MarketDataType MarketDataType { get => Options.MarketDataType; set { Options.MarketDataType= value; } }

        // Data series control
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
        private IBarSeriesService _barSeries;
        // Access
        private Dictionary<int, string> _keys;

        #endregion

        #region Public properties

        public int CacheCapacity { get => Options.CacheCapacity; protected set { Options.CacheCapacity = value; } }
        public int RemovedCacheCapacity {  get => Options.RemovedCacheCapacity; protected set { Options.RemovedCacheCapacity = value; }}
        public int Index { get; internal set; } = -1;
        public bool IsWaitingFirstTick => false;

        public string InstrumentName => _dataSeriesInfo.InstrumentCode.ToName();
        public string TradingHoursName => _dataSeriesInfo.TradingHoursCode.ToName();
        public NinjaTrader.Data.BarsPeriod BarsPeriod => _dataSeriesInfo.TimeFrame.ToBarsPeriod();

        public double this[int index] => _barSeries[index];
        public CurrentBarSeries CurrentBar => _barSeries.CurrentBar;
        public TimeSeries Time => _barSeries.Time;
        public PriceSeries Open => _barSeries.Open;
        public PriceSeries High => _barSeries.High;
        public PriceSeries Low => _barSeries.Low;
        public PriceSeries Close => _barSeries.Close;
        public VolumeSeries Volume => _barSeries.Volume;
        public TickSeries Tick => _barSeries.Tick;

        public bool IsUpdated => IsConfigureAll && _barEvents[BarEvent.Updated];
        public bool IsClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool LastBarIsRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool IsTick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool IsFirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool IsPriceChanged => IsUpdated && _barEvents[BarEvent.PriceChanged];

        public override string Name => string.IsNullOrEmpty(_dataSeriesInfo.Name) ? $"Bars[{Index}]({InstrumentName},{BarsPeriod.ToShortString()})" : _dataSeriesInfo.Name;
        public override string Key => $"{InstrumentName}({BarsPeriod.ToShortString()},{BarsPeriod.MarketDataType},{TradingHoursName})";
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

        public IndicatorCollection Indicators { get; private set; }
        public StatsCollection Stats { get; private set; }
        public FiltersCollection Filters { get; private set; }

        #endregion

        #region Constructors

        //public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript) : base(ninjascript) { }
        //public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        //public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsServiceOptions> configureOptions) : base(ninjascript, printService, configureOptions) { }
        //public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsServiceOptions options) : base(ninjascript, printService, options) { }
        //public BarsService(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsServiceOptions> configureOptions, BarsServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

        internal BarsService(IBarsManager barsManager) : this(barsManager, new BarsServiceOptions()) { }
        internal BarsService(IBarsManager barsManager, BarsServiceOptions options) : this(barsManager, null, options) { }
        internal BarsService(IBarsManager barsManager, DataSeriesOptions dataSeriesOptions, BarsServiceOptions barsServiceOptions) : base(barsManager?.Ninjascript, barsManager?.PrintService, barsServiceOptions)
        {
            barsManager.PrintService.LogTrace("BarsService constructor.");

            _dataSeriesInfo = dataSeriesOptions ?? new DataSeriesOptions(barsManager.Ninjascript);
            _barSeries = new BarSeriesService(this);
            barsManager.PrintService.LogTrace("BarsSeries has been created.");

            //Indicators = new IndicatorCollection(this);
            //Filters = new FiltersCollection(this);
            //Stats = new StatsCollection(this);
        }

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

            if (_dataSeriesInfo == null)
                PrintService.LogError(new Exception("The 'DataSeriesOptions' must be loaded before the service will be configured."));

            _barSeries = new BarSeriesService(this);

            _barSeries.Configure();
            Indicators?.Configure();
            Filters?.Configure();
            Stats?.Configure();

            isConfigured = _dataSeriesInfo != null && _barSeries.IsConfigure && Indicators.IsConfigure && Filters.IsConfigure && Stats.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (_dataSeriesInfo.EqualsTo(Ninjascript, i))
                {
                    Index = i;
                    break;
                }

            if (Index == -1)
                PrintService.LogError(new Exception($"{Name} cannot be loaded because the 'DataSeriesOptions' don't mutch with any 'NinjaScript.DataSeries'."));

            // Configure services
            _barSeries.DataLoaded();
            Indicators?.DataLoaded();
            Filters?.DataLoaded();
            Stats?.DataLoaded();

            isDataLoaded =
                Index != -1 &&
                _barSeries.IsDataLoaded &&
                Indicators != null ? Indicators.IsDataLoaded : true &&
                Filters != null ? Filters.IsDataLoaded : true &&
                Stats != null ? Stats.IsDataLoaded : true
                ;
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
            _barSeries.Update();
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
        public Bar GetBar(int barsAgo) => _barSeries.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barSeries.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barSeries.GetBars(barsAgo, period);

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Private methods

        internal void SetDataSeriesInfo(DataSeriesOptions dataSeriesOptions)
        {
            if (dataSeriesOptions == null)
                throw new ArgumentNullException(nameof(dataSeriesOptions));

            if (_dataSeriesInfo == null)
                _dataSeriesInfo = new DataSeriesOptions();

            _dataSeriesInfo.InstrumentCode = dataSeriesOptions.InstrumentCode;
            _dataSeriesInfo.TimeFrame = dataSeriesOptions.TimeFrame;
            _dataSeriesInfo.TradingHoursCode = dataSeriesOptions.TradingHoursCode;
            _dataSeriesInfo.MarketDataType = dataSeriesOptions.MarketDataType;
        }

        internal void SetDefaultDataSeriesInfo(NinjaScriptBase ninjascript)
        {
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            if (_dataSeriesInfo == null)
                _dataSeriesInfo = new DataSeriesOptions();

            _dataSeriesInfo.InstrumentCode = ninjascript.BarsArray[0].Instrument.MasterInstrument.Name.ToInstrumentCode();
            _dataSeriesInfo.TimeFrame = ninjascript.BarsArray[0].BarsPeriod.ToTimeFrame();
            _dataSeriesInfo.TradingHoursCode = ninjascript.BarsArray[0].TradingHours.Name.ToTradingHoursCode();
            _dataSeriesInfo.MarketDataType = ninjascript.BarsArray[0].BarsPeriod.MarketDataType.ToKrMarketDataType();
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
            if ((type == typeof(BarSeriesService) || type == typeof(IBarSeriesService)))
                service = new BarSeriesService(this);
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
