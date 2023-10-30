using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    public class LastCacheService : DoubleCacheService
    {
        public LastCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override bool CheckReplacementConditions(double candidateValue) => true;

        public override double GetNextCacheValue(int barsAgo = 0)
        {

            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBars[_ninjascript.BarsInProgress] < 0)
                return double.NaN;

            return _ninjascript.Inputs[_ninjascript.BarsInProgress][barsAgo];

        }
    }
}
