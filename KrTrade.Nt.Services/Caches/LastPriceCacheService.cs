using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the latest market prices.
    /// </summary>
    public class LastPriceCacheService : SeriesCacheService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(LastPriceCacheService);

        public LastPriceCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override ISeries<double> Series => Ninjascript.Inputs[_barsService.Idx];
        public override bool IsBetterCandidateValue() => true;

    }
}
