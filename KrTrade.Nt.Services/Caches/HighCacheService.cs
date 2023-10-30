using KrTrade.Nt.Services.Bars;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    public class HighCacheService : DoubleCacheService
    {
        public HighCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override bool CheckReplacementConditions(double candidateValue)
        {
            return Count == Capacity && candidateValue.ApproxCompare(this[Count - 1]) > 0;
        }

        public override double GetNextCacheValue(int barsAgo = 0)
        {
            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBars[_ninjascript.BarsInProgress] < 0)
                return double.NaN;

            return 
                !(
                _ninjascript.Inputs[_ninjascript.BarsInProgress] is PriceSeries ||
                _ninjascript.Inputs[_ninjascript.BarsInProgress] is NinjaTrader.Data.Bars
                ) 
                ? _ninjascript.Inputs[_ninjascript.BarsInProgress][barsAgo] 
                : _ninjascript.Highs[_ninjascript.BarsInProgress][barsAgo];
        }
    }
}
