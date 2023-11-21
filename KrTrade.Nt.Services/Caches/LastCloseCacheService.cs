using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market close prices.
    /// </summary>
    public class LastCloseCacheService : SeriesCacheService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(LastCloseCacheService);

        public LastCloseCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => Ninjascript.Closes[_barsService.Idx];
        public override bool IsBetterCandidateValue() => true;

    }
}
