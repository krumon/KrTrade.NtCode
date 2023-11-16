using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Cache to store the latest market high prices.
    /// </summary>
    public class LastHighCacheService : SeriesCacheService
    {
        public LastHighCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => _ninjascript.Highs[_barsService.Idx];
        public override bool IsBetterCandidateValue() => CandidateValue.ApproxCompare(Cache[Count - 1]) > 0;
    }
}
