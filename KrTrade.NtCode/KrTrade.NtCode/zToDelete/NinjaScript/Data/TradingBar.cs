using System;

namespace ConsoleApp
{
    public class TradingBar : BaseElement
    {

        #region Public properties

        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
        public DateTime Time { get; set; }

        public double Range => High - Low;
        public double Median => (High + Low) / 2;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="idx">Index of the bar in the chart.</param>
        /// <param name="open">Open price.</param>
        /// <param name="high">High price.</param>
        /// <param name="low">Low price.</param>
        /// <param name="close">Close price.</param>
        /// <param name="volume">Represents the volume of the bar.</param>
        /// <param name="time">Represents the time of the bar.</param>
        public TradingBar(int idx, double open, double high, double low, double close, long volume, DateTime time)
        {
            Idx = idx;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Time = time;
        }

        #endregion

        #region ToString Methods

        public override string ToString()
        {
            return String.Format("O: {0} | H: {1} | L: {2} | C: {3}", Open.ToString(), High.ToString(),Low.ToString(), Close.ToString());
        }

        #endregion

    }
}
