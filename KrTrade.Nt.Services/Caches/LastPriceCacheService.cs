using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Cache to store the latest market prices.
    /// </summary>
    public class LastPriceCacheService : SeriesCacheService
    {
        public LastPriceCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => _ninjascript.Inputs[_barsService.Idx];
        public override bool IsBetterCandidateValue() => true;

    }
}
