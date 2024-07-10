using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsCacheService : BarUpdateService<BarUpdateServiceInfo, BarUpdateServiceOptions>, IBarsCacheService
    {
        #region Private members

        internal readonly BarsCache Cache;

        #endregion

        #region Constructors

        public BarsCacheService(IBarsService barsService) : this(barsService, new BarUpdateServiceInfo(), new BarUpdateServiceOptions()) { }
        public BarsCacheService(IBarsService barsService, BarUpdateServiceInfo info, BarUpdateServiceOptions options) : base(barsService, info, options)
        {
            Cache = new BarsCache(barsService.CacheCapacity, barsService.RemovedCacheCapacity);
        }

        #endregion

        #region Implementation

        public int Idx => Cache.Idx;
        public DateTime Time => Cache.Time;
        public double Open => Cache.Open;
        public double High => Cache.High;
        public double Low => Cache.Low;
        public double Close => Cache.Close;
        public double Volume => Cache.Volume;
        public long Ticks => Cache.Ticks;

        // IEnumerable<T> implementation
        public Bar this[int index]  => Cache[index];

        public override void BarUpdate()
        {
            if (!Options.IsEnable || (!IsConfigure || !IsDataLoaded))
                return;

            if (Bars.LastBarIsRemoved)
                Cache.RemoveLastElement();
            else if (Bars.IsClosed)
                Cache.Add(Bars,0);
            else if (Bars.IsPriceChanged || Bars.IsTick)
                Cache.Update(Bars, 0);
        }
        public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (!Options.IsEnable || (!IsConfigure || !IsDataLoaded))
                return;
            if(Bars.IsClosed && Bars.IsMarketData)
            {
                if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnBarClose)
                    Cache.Add(Bars, args);
                else 
                    Cache.Update(Bars, args);
            }
            else if (Bars.IsMarketData)
                Cache.Update(Bars, args);
        }

        protected ServiceType GetServiceType() => ServiceType.BARS_CACHE;
        public override string ToString() => Cache.ToString();
        public Bar GetBar(int barsAgo) => Cache.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => Cache.GetBar(barsAgo, period);
        public Bar[] GetRange(int barsAgo, int period) => Cache.GetRange(barsAgo, period);

        protected override ServiceType ToElementType() => ServiceType.BARS_CACHE;
        protected override void Configure(out bool isConfigured)
        {
            Cache.Reset();
            isConfigured = true;
        }
        protected override void DataLoaded(out bool isDataLoaded)
        {
            //if (!Cache.IsReset)
            //  Reset();
            Cache.Reset();
            isDataLoaded = true;
        }
        public override void Terminated() => Cache.Dispose();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }
        protected override string GetHeaderString()
        {
            throw new System.NotImplementedException();
        }
        protected override string GetParentString()
        {
            throw new System.NotImplementedException();
        }
        protected override string GetDescriptionString()
        {
            throw new System.NotImplementedException();
        }

        // IEnumerable implementation
        public IEnumerator<Bar> GetEnumerator() => Cache.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Cache.GetEnumerator();
    }

    #endregion

}
