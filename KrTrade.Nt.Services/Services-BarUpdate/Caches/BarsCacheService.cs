using KrTrade.Nt.Core.Bars;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents a cache with bar values.
    /// </summary>
    public class BarsCacheService : CacheService<BarsCache>, IBarsCacheService
    {
        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated <see cref="BarsCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarsCacheService(IBarsService barsService) : base(barsService, barsService?.Ninjascript, barsService?.Options.CacheOptions)
        {
        }

        public override string Name => BarUpdateCache.ToString();
        public IndexCache Index => BarUpdateCache.Index;
        public TimeCache Time => BarUpdateCache.Time;
        public DoubleCache<NinjaScriptBase> Open => BarUpdateCache.Open;
        public HighCache High => BarUpdateCache.High;
        public DoubleCache<NinjaScriptBase> Low => BarUpdateCache.Low;
        public DoubleCache<NinjaScriptBase> Close => BarUpdateCache.Close;
        public VolumeCache Volume => BarUpdateCache.Volume;
        public TicksCache Ticks => BarUpdateCache.Ticks;
        public Bar GetBar(int barsAgo) => BarUpdateCache.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => BarUpdateCache.GetBar(barsAgo);
        public IList<Bar> GetBars(int barsAgo, int period) => BarUpdateCache.GetBars(barsAgo, period);

    }
}
