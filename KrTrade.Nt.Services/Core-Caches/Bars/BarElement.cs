//using KrTrade.Nt.Core.Bars;
//using KrTrade.Nt.Core.Caches;
//using NinjaTrader.NinjaScript;
//using System;

//namespace KrTrade.Nt.Services
//{
//    public class BarElement : Bar, ICacheElement<Bar>
//    {
//        #region Constructors

//        /// <summary>
//        /// Create <see cref="Bar"/> default instance.
//        /// </summary>
//        public BarElement()
//        {
//            Reset();
//        }

//        #endregion

//        #region Public methods

//        public void Reset()
//        {
//            Idx = -1;
//            Time = DateTime.MinValue;
//            Open = 0;
//            High = 0;
//            Low = 0;
//            Close = 0;
//            Volume = 0;
//            Ticks = 0;
//        }
//        public void Set(NinjaScriptBase ninjascript, int barsAgo = 0, int barsInProgress = 0)
//        {
//            if (ninjascript == null || ninjascript.State != State.Historical || ninjascript.State != State.Realtime) 
//                return;

//            Idx = ninjascript.CurrentBars[barsInProgress] - barsAgo;
//            Time = ninjascript.Times[barsInProgress][barsAgo];
//            Open = ninjascript.Opens[barsInProgress][barsAgo];
//            High = ninjascript.Highs[barsInProgress][barsAgo];
//            Low = ninjascript.Lows[barsInProgress][barsAgo];
//            Close = ninjascript.Closes[barsInProgress][barsAgo];
//            Volume = (long)ninjascript.Volumes[barsInProgress][barsAgo];

//            if (ninjascript.Calculate == Calculate.OnEachTick)
//            {
//                if (ninjascript.State == State.Realtime || (ninjascript.State == State.Historical && ninjascript.Bars.IsTickReplay))
//                    Ticks = ninjascript.BarsArray[barsInProgress].TickCount;
//            }
//        }
//        public void Set(NinjaTrader.Data.MarketDataEventArgs args)
//        {
//            // TODO: Asignar valores cada vez que los datos de mercado cambian.
//        }
//        public void CopyTo(Bar bar)
//        {
//            bar.Idx = Idx;
//            bar.Time = Time;
//            bar.Open = Open;
//            bar.High = High;
//            bar.Low = Low;
//            bar.Close = Close;
//            bar.Volume = Volume;
//            bar.Ticks = Ticks;
//        }
//        public Bar Get()
//        {
//            return new Bar()
//            {
//                Idx = this.Idx,
//                Time = this.Time,
//                Open = this.Open,
//                High = this.High,
//                Low = this.Low,
//                Close = this.Close,
//                Volume = this.Volume,
//                Ticks = this.Ticks
//            };
//        }

//        #endregion

//    }
//}
