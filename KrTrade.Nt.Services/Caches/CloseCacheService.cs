using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    public class CloseCacheService : DoubleCacheService
    {
        public CloseCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
        }

        public override bool CheckReplacementConditions(double candidateValue) => true;

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
                : _ninjascript.Closes[_ninjascript.BarsInProgress][barsAgo];
        }
    }
}
