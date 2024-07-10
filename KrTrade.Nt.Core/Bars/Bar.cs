using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Bars
{
    public class Bar //:IBarUpdate, IMarketData
    {
        protected readonly IBarsService BarsService;
        protected NinjaScriptBase Ninjascript;

        private int _ticksCounter;

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
        /// <summary>
        /// Create <see cref="Bar"/> default instance.
        /// </summary>
        public Bar(IBarsService barsService)
        {
            Reset();
            Update(barsService,0);
        }
        /// <summary>
        /// Create <see cref="Bar"/> default instance.
        /// </summary>
        public Bar(IBarsService barsService, int barsAgo)
        {
            Reset();
            Update(barsService,barsAgo);
        }
        /// <summary>
        /// Create <see cref="Bar"/> default instance.
        /// </summary>
        public Bar(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            Reset();
            Set(ninjascript,barsInProgress,barsAgo);
        }

        #endregion

        #region Implementation

        //public void BarUpdate()
        //{
        //    if (BarsService.IsClosed)
        //        Set(0);
        //    else if (BarsService.IsUpdated)
        //        Update(0);
        //}

        //public void BarUpdate(IBarsService updatedBarsService)
        //{
        //    throw new NotImplementedException();
        //}

        //public void MarketData(MarketDataEventArgs args)
        //{
        //    Update(args);
        //}

        //public void MarketData(IBarsService updatedBarsService)
        //{
        //    throw new NotImplementedException();
        //}

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

            _ticksCounter = 0;
        }
        public bool Update(IBarsService barsService, int barsAgo)
        {
            if (barsService != null && barsService.IsClosed)
                Reset();

            return Set(barsService.Ninjascript, barsService.Index, barsAgo);
        }
        public bool Update(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            return Set(ninjascript, barsInProgress, barsAgo);
        }
        public bool Update(IBarsService barsService, MarketDataEventArgs args)
        {
            if (barsService != null && barsService.IsClosed)
                Reset();

            return Set(barsService.Ninjascript, barsService.Index, args);
        }
        public bool Update(NinjaScriptBase ninjascript, int barsInProgress, MarketDataEventArgs args)
        {
            return Set(ninjascript, barsInProgress, args);
        }

        //public bool Update(int barsAgo) => Update(barsAgo, BarsService.Index);
        //public bool Update(int barsAgo, int barsInProgress)
        //{
        //    if (Ninjascript.State != State.Historical || Ninjascript.State != State.Realtime)
        //        return false;

        //    double open, high, low, close, volume;
        //    long ticks = 0;

        //    open = Ninjascript.Opens[barsInProgress][barsAgo];
        //    high = Ninjascript.Highs[barsInProgress][barsAgo];
        //    low = Ninjascript.Lows[barsInProgress][barsAgo];
        //    close = Ninjascript.Closes[barsInProgress][barsAgo];
        //    volume = Ninjascript.Volumes[barsInProgress][barsAgo];
        //    if (Ninjascript.Calculate == Calculate.OnEachTick)
        //        ticks = Ninjascript.BarsArray[barsInProgress].TickCount;

        //    bool isUpdated = false;
        //    if(open != Open)
        //    {
        //        Open = open;
        //        isUpdated = true;
        //    }
        //    if (high > High)
        //    {
        //        High = high;
        //        isUpdated = true;
        //    }
        //    if (low < Low)
        //    {
        //        Low = low;
        //        isUpdated = true;
        //    }
        //    if (close != Close)
        //    {
        //        Close = close;
        //        isUpdated = true;
        //    }
        //    if (volume != Volume)
        //    {
        //        Volume = volume;
        //        isUpdated = true;
        //    }
        //    if (ticks != Ticks)
        //    {
        //        Ticks = ticks;
        //        isUpdated = true;
        //    }
        //    return isUpdated;
        //}
        //public bool Update(MarketDataEventArgs args)
        //{
        //    if (args == null || Ninjascript.State != State.Historical || Ninjascript.State != State.Realtime)
        //        return false;

        //    if (args.MarketDataType != MarketDataType.Last)
        //        return false;

        //    double price = args.Price;
        //    High = price > High ? price : High;
        //    Low = price < Low ? price : Low;
        //    Close = price != Close ? price : Close;
        //    Volume += args.Volume;
        //    Ticks = _ticksCounter++;

        //    return true;
        //}
        //public void Update(IBarsService barsService, int barsAgo = 0)
        //{
        //    if (barsService.Ninjascript.State != State.Historical || barsService.Ninjascript.State != State.Realtime)
        //        return;
        //    if (barsService.IsClosed)
        //    {
        //        Reset();
        //        Set(barsService, barsAgo);
        //    }

        //}

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

        #endregion

        #region Private methods

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
        protected bool Set(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            if (ninjascript.State != State.Historical && ninjascript.State != State.Realtime)
                return false;

            SetValues(ninjascript, barsInProgress, barsAgo);
            SetTicks(ninjascript, barsInProgress);

            return true;
        }
        protected bool Set(NinjaScriptBase ninjascript, int barsInProgress, MarketDataEventArgs args)
        {
            if (args == null || Ninjascript.State != State.Historical || Ninjascript.State != State.Realtime)
                return false;

            if (ninjascript.State == State.Historical && !ninjascript.BarsArray[barsInProgress].IsTickReplay)
            {
                SetValues(ninjascript, barsInProgress, 0);
                SetTicks(ninjascript, barsInProgress);
            }
            else
            {
                if (args.MarketDataType != MarketDataType.Last)
                    return false;

                double price = args.Price;
                High = price > High ? price : High;
                Low = price < Low ? price : Low;
                Close = price != Close ? price : Close;
                Volume += args.Volume;
                Ticks = _ticksCounter++;
            }
            return true;
        }
        protected void SetValues(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            Idx = ninjascript.CurrentBars[barsInProgress] - barsAgo;
            Time = ninjascript.Times[barsInProgress][barsAgo];
            Open = ninjascript.Opens[barsInProgress][barsAgo];
            High = ninjascript.Highs[barsInProgress][barsAgo];
            Low = ninjascript.Lows[barsInProgress][barsAgo];
            Close = ninjascript.Closes[barsInProgress][barsAgo];
            Volume = (long)ninjascript.Volumes[barsInProgress][barsAgo];
        }
        protected void SetTicks(NinjaScriptBase ninjascript, int barsInProgress)
        {
            if (ninjascript.Calculate == Calculate.OnEachTick && ninjascript.State == State.Realtime)
                Ticks = ninjascript.BarsArray[barsInProgress].TickCount;
            else if (ninjascript.Calculate == Calculate.OnEachTick && ninjascript.BarsArray[barsInProgress].IsTickReplay && ninjascript.State == State.Historical)
                Ticks++;
            else
                Ticks = 1;
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
                Ninjascript = bar.Ninjascript
            };
        public static Bar operator +(Bar bar1, Bar bar2) => 
            new Bar()
            {
                Idx = bar1.Time < bar2.Time ? bar2.Idx : bar1.Idx,
                Time = bar1.Time < bar2.Time ? bar2.Time : bar1.Time,
                Open = bar1.Time < bar2.Time ? bar1.Open : bar2.Open,
                High = bar1.High > bar2.High ? bar1.High : bar2.High,
                Low = bar1.Low < bar2.Low ? bar1.Low : bar2.Low,
                Close = bar1.Time < bar2.Time ? bar2.Close : bar1.Close,
                Volume = bar1.Volume + bar2.Volume,
                Ticks = bar1.Ticks + bar2.Ticks,
                Ninjascript = bar1.Ninjascript
            
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
            (bar1 is null && bar2 is null) ||
            (!(bar1 is null) && !(bar2 is null) && Equals(bar1,bar2));
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

        public override string ToString() 
            => $"Time: {Time.ToString(Data.TimeFormat.Minute, Data.FormatType.Log, Data.FormatLength.Short)}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}, Ticks: {Ticks}.";

        #endregion

        private static bool Equals(Bar bar1, Bar bar2) =>
            bar1.Idx == bar2.Idx &&
            bar1.Time == bar2.Time &&
            bar1.Open == bar2.Open &&
            bar1.High == bar2.High &&
            bar1.Low == bar2.Low &&
            bar1.Close == bar2.Close &&
            bar1.Volume == bar2.Volume &&
            bar1.Ticks == bar2.Ticks;

    }
}
