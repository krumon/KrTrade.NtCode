using KrTrade.Nt.Core.Caches;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Bars
{
    public class Bar : ICacheElement<Bar>
    {
        #region Public properties

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

        /// Gets the number of ticks in the bar.
        /// </summary>
        public long Ticks { get; set; }

        /// <summary>
        /// Gets the range of the bar.
        /// </summary>
        public double Range => High - Low;

        /// <summary>
        /// Gets the median price of the bar.
        /// </summary>
        public double Median => (High + Low) / 2;

        /// <summary>
        /// Gets the typical price of the bar.
        /// </summary>
        public double Typical => (High + Low + Close) / 3;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="Bar"/> default instance.
        /// </summary>
        public Bar()
        {
            Reset();
        }

        #endregion

        #region Public methods

        public void Reset()
        {
            Idx = -1;
            Time = DateTime.MinValue;
            Open = 0;
            High = 0;
            Low = 0;
            Close = 0;
            Volume = 0;
            Ticks = 0;
        }
        public void Set(NinjaScriptBase ninjascript, int barsAgo, int barsInProgress)
        {
            if (ninjascript == null || ninjascript.State != State.Historical || ninjascript.State != State.Realtime)
                return;

            Idx = ninjascript.CurrentBars[barsInProgress] - barsAgo;
            Time = ninjascript.Times[barsInProgress][barsAgo];
            Open = ninjascript.Opens[barsInProgress][barsAgo];
            High = ninjascript.Highs[barsInProgress][barsAgo];
            Low = ninjascript.Lows[barsInProgress][barsAgo];
            Close = ninjascript.Closes[barsInProgress][barsAgo];
            Volume = (long)ninjascript.Volumes[barsInProgress][barsAgo];

            if (ninjascript.Calculate == Calculate.OnEachTick)
            {
                if (ninjascript.State == State.Realtime || (ninjascript.State == State.Historical && ninjascript.Bars.IsTickReplay))
                    Ticks = ninjascript.BarsArray[barsInProgress].TickCount;
            }
        }
        public void Set(MarketDataEventArgs args)
        {
            // TODO: Asignar valores cada vez que los datos de mercado cambian.
        }
        public void CopyTo(Bar bar)
        {
            bar.Idx = Idx;
            bar.Time = Time;
            bar.Open = Open;
            bar.High = High;
            bar.Low = Low;
            bar.Close = Close;
            bar.Volume = Volume;
            bar.Ticks = Ticks;
        }
        public Bar Get()
        {
            return new Bar()
            {
                Idx = this.Idx,
                Time = this.Time,
                Open = this.Open,
                High = this.High,
                Low = this.Low,
                Close = this.Close,
                Volume = this.Volume,
                Ticks = this.Ticks
            };
        }

        protected void Set(int idx, DateTime time, double open, double high, double low, double close, double volume, long ticks)
        {
            Idx = idx;
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Ticks = ticks;
        }

        #endregion

        #region Operators

        public static Bar operator +(Bar bar) => bar;
        public static Bar operator -(Bar bar) => 
            new Bar() {
                Time = bar.Time, 
                Open = -bar.Open, 
                High = -bar.High, 
                Low = -bar.Low, 
                Close = -bar.Close,
                Volume = -bar.Volume,
                Ticks = -bar.Ticks,
            };
        public static Bar operator +(Bar bar1, Bar bar2) => new Bar()
        {
            Idx = bar1.Time < bar2.Time ? bar2.Idx : bar1.Idx,
            Time = bar1.Time < bar2.Time ? bar2.Time : bar1.Time,
            Open = bar1.Time < bar2.Time ? bar1.Open : bar2.Open,
            High = bar1.High > bar2.High ? bar1.High : bar2.High,
            Low = bar1.Low < bar2.Low ? bar1.Low : bar2.Low,
            Close = bar1.Time < bar2.Time ? bar2.Close : bar1.Close,
            Volume = bar1.Volume + bar2.Volume,
            Ticks = bar1.Ticks + bar2.Ticks,
        };
        public static Bar operator -(Bar bar1, Bar bar2)
        {
            if (bar1 >= bar2)
                return bar1 + (-bar2);
            else
                return bar2 + (-bar1);
        }
        public static Bar operator +(double volume, Bar bar) 
        {
            bar.Volume += volume;
            return bar;
        }
        public static Bar operator +(long ticks, Bar bar) 
        {
            bar.Ticks += ticks;
            return bar;
        }
        public static Bar operator -(double volume, Bar bar) 
        {
            bar.Volume -= volume;
            return bar;
        }
        public static Bar operator -(long ticks, Bar bar) 
        {
            bar.Ticks -= ticks;
            return bar;
        }

        public static bool operator ==(Bar bar1, Bar bar2) => 
            bar1.Idx == bar2.Idx && 
            bar1.Time == bar2.Time && 
            bar1.Open == bar2.Open &&
            bar1.High == bar2.High &&
            bar1.Low == bar2.Low &&
            bar1.Close == bar2.Close &&
            bar1.Volume == bar2.Volume &&
            bar1.Ticks == bar2.Ticks
            ;
        public static bool operator !=(Bar bar1, Bar bar2) => !(bar1 == bar2);
        public static bool operator <(Bar bar1, Bar bar2) => bar1.Time < bar2.Time;
        public static bool operator >(Bar bar1, Bar bar2) => bar1.Time > bar2.Time;
        public static bool operator <=(Bar bar1, Bar bar2) => bar1.Time <= bar2.Time;
        public static bool operator >=(Bar bar1, Bar bar2) => bar1.Time >= bar2.Time;
        public static bool operator <(DateTime time, Bar bar2) => time < bar2.Time;
        public static bool operator >(DateTime time, Bar bar2) => time > bar2.Time;
        public static bool operator <=(DateTime time, Bar bar2) => time <= bar2.Time;
        public static bool operator >=(DateTime time, Bar bar2) => time >= bar2.Time;

        public override bool Equals(object obj) => obj is Bar other && this == other;
        public override int GetHashCode() => Time.GetHashCode();

        #endregion

    }
}
