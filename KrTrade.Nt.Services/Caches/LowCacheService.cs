using KrTrade.Nt.Services.Bars;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Caches
{
    public class LowCacheService : DoubleCacheService
    {
        private readonly NinjaScriptBase _ninjaScript;

        public LowCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
            _ninjaScript = ninjascript ?? throw new System.ArgumentException(nameof(ninjascript));
        }
        public override bool CheckReplacementConditions(double candidateValue)
        {
            return Count == Capacity && candidateValue.ApproxCompare(this[Count - 1]) < 0;
        }

        public override double GetNextCacheValue(int barsAgo = 0)
        {

            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBars[_ninjascript.BarsInProgress] < 0)
                return double.NaN;

            return
                !(
                _ninjaScript.Inputs[_ninjaScript.BarsInProgress] is PriceSeries ||
                _ninjaScript.Inputs[_ninjaScript.BarsInProgress] is NinjaTrader.Data.Bars
                ) 
                ? _ninjaScript.Inputs[_ninjaScript.BarsInProgress][barsAgo] 
                : _ninjaScript.Lows[_ninjaScript.BarsInProgress][barsAgo];
        }
    }
}
