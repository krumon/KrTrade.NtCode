using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Services.Series;
using NinjaTrader.Core.FloatingPoint;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseNinjascriptService<BarsServiceOptions>, IBarsService
    {

        #region Private members

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
        private readonly Dictionary<int, string> _keys;

        #endregion

        #region Public properties

        public int CacheCapacity { get => Options.CacheCapacity; protected set { Options.CacheCapacity = value; } }
        public int RemovedCacheCapacity {  get => Options.RemovedCacheCapacity; protected set { Options.RemovedCacheCapacity = value; }}
        public int Index { get; internal set; } = -1;
        public bool IsWaitingFirstTick => false;
        public DataSeriesInfo Info { get; internal set; }

        public string InstrumentName => Info.InstrumentCode.ToName();
        public string TradingHoursName => Info.TradingHoursCode.ToName();
        public NinjaTrader.Data.BarsPeriod BarsPeriod => Info.TimeFrame.ToBarsPeriod();

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

        public override string Name => string.IsNullOrEmpty(Info.Name) ? $"Bars[{Index}]({InstrumentName},{BarsPeriod.ToShortString()})" : Info.Name;
        public override string GetKey() 
            => $"{InstrumentName}({BarsPeriod.ToShortString()},{BarsPeriod.MarketDataType},{TradingHoursName})";
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

        // TODO: Implementar SeriesCollection. De esta colección se obtendran todas las series necesarias
        //       para cualquiera de nuestros servicios (SeriesService, StatsService, IndicatorService,...).
        //       En esta colección se deben insertar las NinjaScriptSeries como punto de partida.
        public SeriesCollection<ISeries> Series {  get; set; }

        public IndicatorCollection Indicators { get; private set; }
        public StatsCollection Stats { get; private set; }
        public FiltersCollection Filters { get; private set; }

        #endregion

        #region Constructors

        internal BarsService(IBarsManager barsManager) : this(barsManager, null, new BarsServiceOptions()) { }
        internal BarsService(IBarsManager barsManager, BarsServiceOptions options) : this(barsManager, null, options) { }
        internal BarsService(IBarsManager barsManager, DataSeriesInfo dataSeriesOptions, BarsServiceOptions barsServiceOptions) : base(barsManager?.Ninjascript, barsManager?.PrintService, barsServiceOptions)
        {
            Info = dataSeriesOptions ?? new DataSeriesInfo(barsManager.Ninjascript);
            _barSeries = new BarSeriesService(this);

            //Series = new SeriesCollection<ISeries>(_barSeries);
            Indicators = new IndicatorCollection(this);
            Filters = new FiltersCollection(this);
            Stats = new StatsCollection(this);
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

            if (Info == null)
                PrintService.LogError(new Exception("The 'DataSeriesOptions' must be loaded before the service will be configured."));

            _barSeries = new BarSeriesService(this);

            _barSeries.Configure();
            Indicators?.Configure();
            Filters?.Configure();
            Stats?.Configure();

            isConfigured = Info != null && _barSeries.IsConfigure && Indicators.IsConfigure && Filters.IsConfigure && Stats.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (Info.EqualsTo(Ninjascript, i))
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

        public Bar GetBar(int barsAgo) => _barSeries.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barSeries.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barSeries.GetBars(barsAgo, period);

        public ISeries GetSeries(BaseSeriesInfo options)
        {
            throw new NotImplementedException();
        }

        public ISeries GetOrAddSeries(BaseSeriesInfo options)
        {
            throw new NotImplementedException();
        }

        public void AddSeries(BaseSeriesInfo seriesInfo)
        {

        }


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
        private IBarUpdateService CreateBarUpdateService<TService, TOptions>(TOptions options)
            where TOptions : BarUpdateServiceOptions, new()
        {
            IBarUpdateService service = null;
            //Type type = typeof(TService);
            //if (options == null)
            //    options = new TOptions();
            //if ((type == typeof(BarSeriesService) || type == typeof(IBarSeriesService)))
            //    service = new BarSeriesService(this);
            //else if (type == typeof(SeriesService<MaxSeries>))
            //    if (options is SeriesInfo maxInfo) service = new SeriesService<MaxSeries>(this, maxInfo);
            //else if (type == typeof(SeriesService<MinSeries>))
            //    if (options is SeriesInfo minInfo) service = new SeriesService<MinSeries>(this, minInfo);
            //else
            //    throw new NotImplementedException($"The {type.Name} has not been created. Krumon...implemented it!!!!");

            return service;
        }


        #endregion

    }
}
