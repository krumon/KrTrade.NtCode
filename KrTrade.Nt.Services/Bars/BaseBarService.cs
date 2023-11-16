using KrTrade.Nt.Core.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Bars
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public abstract class BaseBarService : BaseService
    {

        #region Private members

        private readonly Bar _bar;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the bars ago of the bar in the bars collection. 
        /// This bars ago 0 is the last bar of the bars collection.
        /// </summary>
        public int BarsAgo { get; private set; }

        /// <summary>
        /// Gets the index of the bars series. 
        /// </summary>
        public int BarsIdx { get; private set; }

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
        /// Create <see cref="BarService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript where the service is being executed.</param>
        /// <param name="printService">The print service to write in the NinjaScript output window.</param>
        /// <param name="barsAgo">The bars ago of the bar in the data series.</param>
        /// <param name="barsIdx">The bars index of the bar data series.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/>cannot be null.</exception>
        public BaseBarService(NinjaScriptBase ninjascript, PrintService printService, int barsAgo, int barsIdx) : base(ninjascript, printService)
        {
            _bar = new Bar();

            if (barsIdx < 0)
                throw new ArgumentOutOfRangeException(nameof(barsIdx));
            if (barsAgo < 0)
                throw new ArgumentOutOfRangeException(nameof(barsAgo));

            BarsIdx = barsIdx;
            BarsAgo = barsAgo;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Print in the NinjaScript output window the high price of the bar.
        /// </summary>
        public void PrintHigh()
        {
            if (_printService == null)
                return;

            _printService.Write("High", High);
        }

        /// <summary>
        /// Print in the NinjaScript output window the last price of the bar.
        /// </summary>
        public void PrintLast()
        {
            if (_printService == null)
                return;

            _printService.Write("Last", Close);
        }

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public void CopyTo(BarService barService)
        {
            barService.BarsAgo = BarsAgo;
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

        #region Private methods

        protected virtual void UpdateValues()
        {
            Idx = GetCurrentBar(BarsIdx, BarsAgo) - BarsAgo;
            Open = GetOpen(BarsIdx, BarsAgo);
            High = GetHigh(BarsIdx, BarsAgo);
            Low = GetLow(BarsIdx, BarsAgo);
            Close = GetClose(BarsIdx, BarsAgo);
            Volume = GetVolume(BarsIdx, BarsAgo);
            Time = GetTime(BarsIdx, BarsAgo);
        }

        #endregion

    }
}
