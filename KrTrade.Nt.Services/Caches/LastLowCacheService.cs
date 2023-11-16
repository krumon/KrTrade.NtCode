using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Cache to store the latest market low prices.
    /// </summary>
    public class LastLowCacheService : SeriesCacheService
    {
        public LastLowCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }
        public override ISeries<double> Series => _ninjascript.Lows[_barsService.Idx];
        public override bool IsBetterCandidateValue() => CandidateValue.ApproxCompare(Cache[Count - 1]) < 0;
    }
}
