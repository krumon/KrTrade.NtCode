using KrTrade.Nt.Core.Bars;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class BarService : NinjascriptService<BarOptions>, IBarUpdateService
    {

        #region Private members

        private readonly IBarsService _barsService;
        private Bar _bar;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => "Bar(" + Displacement + ")";

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
        /// Gets the index of the bar in the bars collection. 
        /// This index start in 0. The current bar is the greater value of the index.
        /// </summary>
        public int Idx { get { return _bar.Idx; } set { _bar.Idx = value; } }

        /// <summary>
        /// Gets the date time struct of the bar.
        /// </summary>
        public DateTime Time { get { return _bar.Time; } set { _bar.Time = value; } }

        /// <summary>
        /// Gets the open price of the bar.
        /// </summary>
        public double Open { get { return _bar.Open; } set { _bar.Open = value; } }

        /// <summary>
        /// Gets the high price of the bar.
        /// </summary>
        public double High { get { return _bar.High; } set { _bar.High = value; } }

        /// <summary>
        /// Gets the low price of the bar.
        /// </summary>
        public double Low { get { return _bar.Low; } set { _bar.Low = value; } }

        /// <summary>
        /// Gets the close price of the bar.
        /// </summary>
        public double Close { get { return _bar.Close; } set { _bar.Close = value; } }

        /// <summary>
        /// Gets the volume of the bar.
        /// </summary>
        public double Volume { get { return _bar.Volume; } set { _bar.Volume = value; } }

        /// <summary>
        /// Gets the range of the bar.
        /// </summary>
        public double Range => _bar.Range;

        /// <summary>
        /// Gets the median price of the bar.
        /// </summary>
        public double Median => _bar.Median;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="BarService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarService(IBarsService barsService) : base(barsService?.Ninjascript, barsService?.PrintService)
        {
        }

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="BarService"/>.
        /// <param name="displacement">The <see cref="BarService"/> displacement respect the bars collection.</param>
        /// <param name="barsIdx">The index of the <see cref="IBarsService"/> to witch it belong.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarService(IBarsService barsService, int displacement, int barsIdx) : base(barsService?.Ninjascript, barsService?.PrintService)
        {
            Options.Displacement = displacement;
            Options.BarsIdx = barsIdx;
        }
        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="BarService"/>.
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarService(IBarsService barsService, IConfigureOptions<BarOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions)
        {
        }

        #endregion

        #region Implementation methods

        public IBarsService BarsService => _barsService;
        
        public void Update()
        {
            UpdateValues();
        }

        internal override void Configure(out bool isConfigured)
        {
            if (_bar == null)
                _bar = new Bar();
            isConfigured = true;
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;
        }

        #endregion

        #region Public methods

        protected void UpdateValues()
        {
            Idx = GetBarIdx(BarsIdx, Displacement) - Displacement;
            Open = GetOpen(BarsIdx, Displacement);
            High = GetHigh(BarsIdx, Displacement);
            Low = GetLow(BarsIdx, Displacement);
            Close = GetClose(BarsIdx, Displacement);
            Volume = GetVolume(BarsIdx, Displacement);
            Time = GetTime(BarsIdx, Displacement);
        }

        /// <summary>
        /// Log the values of the <see cref="BarService"/>.
        /// </summary>
        public void LogUpdatedState()
        {
            if (PrintService != null && Options.IsLogEnable)
                PrintService.LogOHLCV(Name, Displacement, BarsIdx);
        }

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public void CopyTo(BarService barService)
        {
            barService.Displacement = Displacement;
            barService.BarsIdx = BarsIdx;
            barService.Idx = Idx;
            barService.Open = Open;
            barService.High = High;
            barService.Low = Low;
            barService.Close = Close;
            barService.Volume = Volume;
            barService.Time = Time;
        }

        #endregion

    }
}
