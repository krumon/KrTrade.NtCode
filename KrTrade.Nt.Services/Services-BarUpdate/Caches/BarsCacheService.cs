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

        public override string Name => _cache.ToString();
        public IndexCache Index => _cache.Index;
        public TimeCache Time => _cache.Time;
        public DoubleCache<NinjaScriptBase> Open => _cache.Open;
        public HighCache High => _cache.High;
        public DoubleCache<NinjaScriptBase> Low => _cache.Low;
        public DoubleCache<NinjaScriptBase> Close => _cache.Close;
        public VolumeCache Volume => _cache.Volume;
        public TicksCache Ticks => _cache.Ticks;
        public Bar GetBar(int barsAgo) => _cache.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _cache.GetBar(barsAgo);
        public IList<Bar> GetBars(int barsAgo, int period) => _cache.GetBars(barsAgo, period);

    }
}
