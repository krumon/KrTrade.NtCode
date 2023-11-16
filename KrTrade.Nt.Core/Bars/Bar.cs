using System;

namespace KrTrade.Nt.Core.Bars
{
    public class Bar
    {
        #region Public properties

        ///// <summary>
        ///// Gets the bars ago of the bar in the bars collection. 
        ///// This bars ago 0 is the last bar of the bars collection.
        ///// </summary>
        //public int BarsAgo { get; set; }

        ///// <summary>
        ///// Gets the index of the bars series. 
        ///// </summary>
        //public int BarsIdx { get; set; }

        /// <summary>
        /// Gets the index of the bar in the bars collection. 
        /// This index start in 0. The current bar is the greater value of the index.
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// Gets the date time struct of the bar.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets the open price of the bar.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Gets the high price of the bar.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Gets the low price of the bar.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Gets the close price of the bar.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Gets the volume of the bar.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Gets the range of the bar.
        /// </summary>
        public double Range => High - Low;

        /// <summary>
        /// Gets the median price of the bar.
        /// </summary>
        public double Median => (High + Low) / 2;

        #endregion

        #region Constructors

        ///// <summary>
        ///// Create <see cref="Bar"/> default instance.
        ///// </summary>
        //public Bar(int barsAgo, int barsIdx)
        //{
        //    BarsAgo = barsAgo;
        //    BarsIdx = barsIdx;
        //    Reset();
        //}

        /// <summary>
        /// Create <see cref="Bar"/> default instance.
        /// </summary>
        public Bar()
        {
            Reset();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Reset the bar values.
        /// </summary>
        public void Reset()
        {
            Idx = -1;
            Open = 0;
            High = double.MinValue;
            Low = double.MaxValue;
            Close = 0;
            Volume = -1;
            Time = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// Set the bar values.
        /// </summary>
        public virtual void SetValues(int idx, double open, double high, double low, double close, long volume, DateTime time)
        {
            Idx = idx;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Time = time;
        }

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public void CopyTo(Bar bar)
        {
            //bar.BarsAgo = BarsAgo;
            //bar.BarsIdx = BarsIdx;
            bar.Idx = Idx;
            bar.Open = Open;
            bar.High = High;
            bar.Low = Low;
            bar.Close = Close;
            bar.Volume = Volume;
            bar.Time = Time;
        }

        /// <summary>
        /// Indicates if two bars are equals.
        /// </summary>
        /// <param name="bar">The second bar to compare.</param>
        /// <returns></returns>
        public bool IsEqualsTo(Bar bar)
        {
            if (bar == null)
                throw new ArgumentNullException(nameof(bar));

            if (bar.Idx == Idx && bar.Open == Open && bar.High == High && bar.Low == Low && bar.Close == Close && bar.Volume == Volume && bar.Time == Time)
                return true;

            return false;
        }

        #endregion

    }
}
