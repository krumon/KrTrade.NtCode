using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Caches
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public class BarsCache : Cache<Bar>, IBarsCache, ITerminated, IBarUpdate, IMarketData, IMarketDepth
    {
        protected IBarsService BarsService { get; set; }

        public int Idx => this[0].Idx;
        public DateTime Time => this[0].Time;
        public double Open => this[0].Open;
        public double High => this[0].High;
        public double Low => this[0].Low;
        public double Close => this[0].Close;
        public double Volume => this[0].Volume;
        public long Ticks => this[0].Ticks;

        public BarsCache(int capacity = Globals.SERIES_DEFAULT_CAPACITY, int oldValuesCapacity = Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY) : base(capacity,oldValuesCapacity)
        {
            BarsService = null;
        }
        public BarsCache(IBarsService barsService) : base(barsService.CacheCapacity, barsService.RemovedCacheCapacity)
        {
            BarsService = barsService ?? throw new System.ArgumentNullException(nameof(barsService));
        }

        public override void Add(int barsAgo) => Add(BarsService, barsAgo);
        public override void Update(int barsAgo) => Update(BarsService, barsAgo);
        public override void Update(NinjaTrader.Data.MarketDataEventArgs args, int barsAgo) => Update(args, barsAgo);

        public void Add(IBarsService barsService = null, int barsAgo = 0) => Add(barsService?.Ninjascript ?? BarsService?.Ninjascript,barsService == null ? BarsService == null ? 0 : BarsService.Index : barsService.Index, barsAgo);
        public void Add(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            if (ninjascript == null)
            {
                ninjascript.Print("The BAR cannot be added to the cache. The 'NinjaScript' or 'IBarsService' cannot be null.");
                return;
            }

            Bar bar = new Bar();
            if (bar.Set(ninjascript,barsInProgress, barsAgo))
                Add(bar);
            else
                ninjascript.Print("The BAR cannot be added to the cache. The 'NinjaScript' is out of Historical or RealTime State.");
        }

        public void Update(IBarsService barsService = null, int barsAgo = 0) => Update(barsService?.Ninjascript ?? BarsService?.Ninjascript, barsService == null ? BarsService == null ? 0 : BarsService.Index : barsService.Index, barsAgo);
        public void Update(NinjaScriptBase ninjascript, int barsInProgress,int barsAgo = 0)
        {
            if (ninjascript == null)
            {
                ninjascript.Print("The BAR cannot be updated. The 'NinjaScript' or 'IBarsService' cannot be null.");
                return;
            }

            if (barsAgo == 0)
            {
                Bar lastValue = new Bar();
                CurrentValue.CopyTo(lastValue);
                if (this[barsAgo].Update(ninjascript, barsInProgress, barsAgo))
                {
                    LastValue = lastValue;
                    this[barsAgo].CopyTo(CurrentValue);
                }
                else
                    ninjascript.Print("The BAR cannot be updated.");
            }
            else
                if (!this[barsAgo].Update(ninjascript, barsInProgress, barsAgo))
                    ninjascript.Print("The BAR cannot be updated.");
        }

        public void Update(NinjaTrader.Data.MarketDataEventArgs args, int barsAgo = 0, IBarsService barsService = null)
        {
            if (BarsService == null) return;

            if (barsAgo == 0)
            {
                Bars.Bar lastValue = new Bars.Bar();
                CurrentValue.CopyTo(lastValue);
                if (this[barsAgo].Update(barsService ?? BarsService, args))
                {
                    LastValue = lastValue;
                    this[barsAgo].CopyTo(CurrentValue);
                }
            }
            else
                this[barsAgo].Update(barsService ?? BarsService, args);
        }
        public void Update(NinjaScriptBase ninjascript, NinjaTrader.Data.MarketDataEventArgs args, int barsAgo = 0)
        {
            if (ninjascript == null)
            {
                ninjascript.Print("The BAR cannot be updated. The 'NinjaScript' or 'IBarsService' cannot be null.");
                return;
            }

            if (barsAgo == 0)
            {
                Bar lastValue = new Bar();
                CurrentValue.CopyTo(lastValue);
                if (this[barsAgo].Update(ninjascript, args))
                {
                    LastValue = lastValue;
                    this[barsAgo].CopyTo(CurrentValue);
                }
                else
                    ninjascript.Print("The BAR cannot be updated.");
            }
            else
                if (!this[barsAgo].Update(ninjascript, args))
                ninjascript.Print("The BAR cannot be updated.");
        }

        public void Terminated() => Dispose();

        public void BarUpdate()
        {
            if (BarsService == null)
                return;

            if (BarsService.LastBarIsRemoved)
                RemoveLastElement();
            else if (BarsService.IsClosed)
                Add(0);
            else if (BarsService.IsPriceChanged || BarsService.IsTick)
                Update(0);
        }
        public void BarUpdate(IBarsService updatedBarsService)
        {
            throw new System.NotImplementedException();
        }
        public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (args == null)
                return;
            Update(args);
        }
        public void MarketData(IBarsService updatedBarsService)
        {
            throw new System.NotImplementedException();
        }
        public void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            throw new System.NotImplementedException();
        }
        public void MarketDepth(IBarsService updatedBarsService)
        {
            throw new System.NotImplementedException();
        }

        public Bar GetBar(int barsAgo) => this[barsAgo].Get();
        public Bar GetBar(int barsAgo, int period)
        {
            if (!IsValidRange(barsAgo, period)) return null;

            Bar bar = new Bar();
            this[barsAgo].CopyTo(bar);
            for (int i = barsAgo + 1; i <= barsAgo + period; i++)
                bar += this[i];
            return bar;
        }

    }
}
