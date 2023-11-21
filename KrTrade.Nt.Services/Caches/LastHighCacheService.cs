using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market high prices.
    /// </summary>
    public class LastHighCacheService : SeriesCacheService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(LastHighCacheService);

        public LastHighCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => Ninjascript.Highs[_barsService.Idx];
        public override bool IsBetterCandidateValue() => CandidateValue.ApproxCompare(Cache[Count - 1]) > 0;
    }
}
