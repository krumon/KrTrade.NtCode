using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market low prices.
    /// </summary>
    public class LastLowCacheService : SeriesCacheService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(LastLowCacheService);

        public LastLowCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }
        public override ISeries<double> Series => Ninjascript.Lows[_barsService.Idx];
        public override bool IsBetterCandidateValue() => CandidateValue.ApproxCompare(Cache[Count - 1]) < 0;
    }
}
