using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Cache to store the latest market close prices.
    /// </summary>
    public class LastCloseCacheService : SeriesCacheService
    {
        public LastCloseCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => _ninjascript.Closes[_barsService.Idx];
        public override bool IsBetterCandidateValue() => true;

    }
}
