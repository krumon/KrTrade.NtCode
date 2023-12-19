using KrTrade.Nt.Core.Bars;
using NinjaTrader.Core.FloatingPoint;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class BarService : BarUpdateService<BarOptions>, IBarUpdateService
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

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the bars ago of the bar in the data series. 
        /// The most recent bar is 0.
        /// </summary>
        public int Displacement { get => Options.Displacement; set { Options.Displacement = value; } }

        /// <summary>
        /// The index of the data series to which it belongs. 
        /// </summary>
        public int BarsIdx { get => Options.BarsIdx; set { Options.BarsIdx = value; } }

        /// <summary>
        /// Gets the any data of the bar.
        /// </summary>
        public BarDataModel Data {get; protected set; }
        
        public bool IsClose => _barEvents?[BarEvent.Closed] ?? false;
        public bool IsRemove => _barEvents?[BarEvent.Removed] ?? false;
        public bool IsTick => _barEvents?[BarEvent.Tick] ?? false;
        public bool IsFirstTick => _barEvents?[BarEvent.FirstTick] ?? false;
        public bool IsPriceChange => _barEvents?[BarEvent.PriceChanged] ?? false;

        ///// <summary>
        ///// Gets the index of the bar in the bars collection. 
        ///// This index start in 0. The current bar is the greater value of the index.
        ///// </summary>
        //public int Idx { get; internal set; }

        ///// <summary>
        ///// Gets the date time struct of the bar.
        ///// </summary>
        //public DateTime Time { get; internal set; }

        ///// <summary>
        ///// Gets the open price of the bar.
        ///// </summary>
        //public double Open { get; internal set; }

        ///// <summary>
        ///// Gets the high price of the bar.
        ///// </summary>
        //public double High { get; internal set; }

        ///// <summary>
        ///// Gets the low price of the bar.
        ///// </summary>
        //public double Low { get; internal set; }

        ///// <summary>
        ///// Gets the close price of the bar.
        ///// </summary>
        //public double Close { get; internal set; }

        ///// <summary>
        ///// Gets the volume of the bar.
        ///// </summary>
        //public double Volume { get; internal set; }

        ///// <summary>
        ///// Gets the number of ticks in the bar.
        ///// </summary>
        //public int Ticks { get; internal set; }

        ///// <summary>
        ///// Gets the range of the bar.
        ///// </summary>
        //public double Range => High - Low;

        ///// <summary>
        ///// Gets the median price of the bar.
        ///// </summary>
        //public double Median => (High + Low) / 2;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BarService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarService"/>.</param>
        /// <param name="displacement">The <see cref="BarService"/> displacement respect the bars collection.</param>
        /// <param name="barsIdx">The index of the <see cref="IDataSeriesService"/> to witch it belong.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BarService(IDataSeriesService dataSeriesService, int displacement, int barsIdx) : base(dataSeriesService)
        {
            Options.Displacement = displacement;
            Options.BarsIdx = barsIdx;
        }

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BarService(IDataSeriesService dataSeriesService, IConfigureOptions<BarOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        #endregion

        #region Implementation

        public override string Name => "Bar(" + Displacement + ")";
        public IList<IBarUpdateService> Services => throw new NotImplementedException();

        internal override void Configure(out bool isConfigured)
        {
            _barEvents = new Dictionary<BarEvent, bool>()
            {
                [BarEvent.None] = false,
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

            Data = new BarDataModel();

            isConfigured = Displacement >= 0;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = BarsIdx > 0 && BarsIdx < Ninjascript.BarsArray.Length;
        }

        public void OnBarUpdate()
        {
            if (!Options.IsEnable)
                return;

            if (!IsConfigured)
                LoggingHelpers.ThrowIsNotConfigureException(Name);

            if (Ninjascript.BarsInProgress != BarsIdx)
                return;

            if (!IsInRunningStates())
                LoggingHelpers.ThrowOutOfRunningStatesException(Name);

            Update();
        }

        public override void Update()
        {
            if (Ninjascript.BarsInProgress != BarsIdx || Ninjascript.CurrentBars[BarsIdx] < Displacement)
                return;

            _currentBarIdx = Ninjascript.CurrentBars[BarsIdx];
            _currentPrice = Ninjascript.Closes[BarsIdx][Displacement];
            ResetBarsEvents();

            // LasBarRemoved
            if (Ninjascript.BarsArray[BarsIdx].BarsType.IsRemoveLastBarSupported && _currentBarIdx < _lastBarIdx)
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
                        OnFirstTick();

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
            _lastBarIdx = _currentBarIdx;
            _lastPrice = _currentPrice;
        }

        public override string ToLogString(string format = "")
        {
            if (string.IsNullOrEmpty(format))
                format = "DEFAULT";
            else
                format = format.ToUpper();

            if (format == "VALUES")
                return ToLogValuesString();
            else if (format == "EVENTS")
                return ToLogEventsString();
            else
                return ToLogValuesString();

        }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public void CopyTo(BarDataModel data)
        {
            //barService.Displacement = Displacement;
            //barService.BarsIdx = BarsIdx;
            data.Idx = Data.Idx;
            data.Open = Data.Open;
            data.High = Data.High;
            data.Low = Data.Low;
            data.Close = Data.Close;
            data.Volume = Data.Volume;
            data.Time = Data.Time;
            // TODO: Copiar todas las propiedades
        }

        #endregion

        #region Protected methods

        protected virtual void OnLastBarRemoved()
        {
            UpdateBarClosedValues();
        }
        protected virtual void OnBarClosed()
        {
            UpdateBarClosedValues();
        }
        protected virtual void OnFirstTick()
        {

        }
        protected virtual void OnPriceChanged()
        {

        }
        protected virtual void OnEachTick()
        {
            UpdateTickValues();
        }

        #endregion

        #region Private methods

        private void SetBarsEvents(bool noneEvent, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _barEvents[BarEvent.None] = noneEvent;
            _barEvents[BarEvent.Removed] = isLastBarRemoved;
            _barEvents[BarEvent.Closed] = isBarClosed;
            _barEvents[BarEvent.FirstTick] = isFirstTick;
            _barEvents[BarEvent.PriceChanged] = isPriceChanged;
            _barEvents[BarEvent.Tick] = isNewTick;
        }
        private void ResetBarsEvents()
        {
            SetBarsEvents(
                noneEvent: false,
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

        private string ToLogEventsString()
        {
            if (_logLines == null || _logLines.Count == 0)
                return string.Empty;
            string stateText = string.Empty;
            for (int i = 0; i < _logLines.Count; i++)
                stateText += _logLines[i];

            return stateText;
        }

        private string ToLogValuesString() => 
            $"{Name}=> Open:{Data.Open}, High:{Data.High}, Low:{Data.Low}, Close:{Data.Close}, Vol:{Data.Volume}.";

        protected void UpdateBarClosedValues()
        {
            Data.Idx = GetBarIdx(BarsIdx, Displacement);
            Data.Open = GetOpen(BarsIdx, Displacement);
            Data.High = GetHigh(BarsIdx, Displacement);
            Data.Low = GetLow(BarsIdx, Displacement);
            Data.Close = GetClose(BarsIdx, Displacement);
            Data.Volume = GetVolume(BarsIdx, Displacement);
            Data.Time = GetTime(BarsIdx, Displacement);
        }

        protected void UpdateTickValues()
        {
            Data.Ticks = Ninjascript.BarsArray[BarsIdx].TickCount;
        }



        #endregion

    }
}
