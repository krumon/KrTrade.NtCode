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
    public class BarsCache : Cache<Bar>, IBarsCache
    {
        public int Idx => this[0].Idx;
        public DateTime Time => this[0].Time;
        public double Open => this[0].Open;
        public double High => this[0].High;
        public double Low => this[0].Low;
        public double Close => this[0].Close;
        public double Volume => this[0].Volume;
        public long Ticks => this[0].Ticks;

        public BarsCache(int capacity, int oldValuesCapacity) : base(capacity, oldValuesCapacity) { }
        public BarsCache() : base(Globals.SERIES_DEFAULT_CAPACITY, Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY) { }

        public void Add(IBarsService barsService, int barsAgo) 
        {
            Bar bar = new Bar();
            if (bar.Update(barsService, barsAgo))
                Add(bar);
            else
                barsService.Ninjascript.Print("The BAR cannot be added to the cache.");

        }
        public void Add(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            Bar bar = new Bar();
            if (bar.Update(ninjascript,barsInProgress,barsAgo))
                Add(bar);
            else
                ninjascript.Print("The BAR cannot be added to the cache.");
        }
        public void Add(IBarsService barsService, NinjaTrader.Data.MarketDataEventArgs args)
        {
            Bar bar = new Bar();
            if (bar.Update(barsService, args))
                Add(bar);
            else
                barsService.Ninjascript.Print("The BAR cannot be added to the cache.");
        }
        public void Add(NinjaScriptBase ninjascript, int barsInProgress, NinjaTrader.Data.MarketDataEventArgs args)
        {
            Bar bar = new Bar();
            if (bar.Update(ninjascript, barsInProgress, args))
                Add(bar);
            else
                ninjascript.Print("The BAR cannot be added to the cache.");
        }

        public void Update(IBarsService barsService, int barsAgo)
        {
            Bar currentValue = new Bar();
            currentValue.Update(barsService,barsAgo);
            if (barsAgo == 0)
            {
                if (this[barsAgo] != currentValue)
                {
                    Bar aux = CurrentValue;
                    CurrentValue = currentValue;
                    LastValue = aux;
                    this[barsAgo] = CurrentValue;
                }
                else
                    barsService.Ninjascript.Print("The BAR cannot be updated.");
            }
            else
            {
                if (this[barsAgo] != currentValue)
                    this[barsAgo] = currentValue;
                else
                    barsService.Ninjascript.Print("The BAR cannot be updated.");
            }
        }
        public void Update(NinjaScriptBase ninjascript, int barsInProgress, int barsAgo)
        {
            Bar currentValue = new Bar();
            currentValue.Update(ninjascript,barsInProgress,barsAgo);
            if (barsAgo == 0)
            {
                if (this[barsAgo] != currentValue)
                {
                    Bar aux = CurrentValue;
                    CurrentValue = currentValue;
                    LastValue = aux;
                    this[barsAgo] = CurrentValue;
                }
                else
                    ninjascript.Print("The BAR cannot be updated.");
            }
            else
            {
                if (this[barsAgo] != currentValue) 
                    this[barsAgo] = currentValue;
                else
                    ninjascript.Print("The BAR cannot be updated.");
            }
        }
        public void Update(IBarsService barsService, NinjaTrader.Data.MarketDataEventArgs args)
        {
            Bar currentValue = new Bar();
            currentValue.Update(barsService, args);
            if (this[0] != currentValue)
            {
                Bar aux = CurrentValue;
                CurrentValue = currentValue;
                LastValue = aux;
                this[0] = CurrentValue;
            }
            else
                barsService.Ninjascript.Print("The BAR cannot be updated.");
        }
        public void Update(NinjaScriptBase ninjascript, int barsInProgress, NinjaTrader.Data.MarketDataEventArgs args)
        {
            Bar currentValue = new Bar();
            currentValue.Update(ninjascript, barsInProgress, args);
            if (this[0] != currentValue)
            {
                Bar aux = CurrentValue;
                CurrentValue = currentValue;
                LastValue = aux;
                this[0] = CurrentValue;
            }
            else
                ninjascript.Print("The BAR cannot be updated.");
        }


        //public void Terminated() => Dispose();

        //public void BarUpdate()
        //{
        //    if (BarsService == null)
        //        return;

        //    if (BarsService.LastBarIsRemoved)
        //        RemoveLastElement();
        //    else if (BarsService.IsClosed)
        //        Add(0);
        //    else if (BarsService.IsPriceChanged || BarsService.IsTick)
        //        Update(0);
        //}
        //public void BarUpdate(IBarsService updatedBarsService)
        //{
        //    if (updatedBarsService == null)
        //        return;

        //    updatedBarsService[0].CopyTo(this[0]);
        //}
        //public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        //{
        //    if (args == null)
        //        return;
        //    Update(args,0);
        //}
        //public void MarketData(IBarsService updatedBarsService)
        //{
        //    if (updatedBarsService == null)
        //        return;

        //    updatedBarsService[0].CopyTo(this[0]);
        //}
        //public void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public void MarketDepth(IBarsService updatedBarsService)
        //{
        //    throw new System.NotImplementedException();
        //}

        public Bar GetBar(int barsAgo) => this[barsAgo].Get();
        public Bar GetBar(int barsAgo, int period)
        {
            if (!IsValidRange(barsAgo, period)) return default;

            Bar bar = new Bar();
            this[barsAgo].CopyTo(bar);
            for (int i = barsAgo + 1; i <= barsAgo + period; i++)
                bar += this[i];
            return bar;
        }

        public override string ToString() => ToString(0);

        public string ToString(int barsAgo) => IsValidIndex(barsAgo) ? this[barsAgo].ToString() : "BarsCache index out of range.";
    }
}
