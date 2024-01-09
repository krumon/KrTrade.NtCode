using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeCache : CalculateSeriesCache
    {
        /// <inheritdoc/>
        public RangeCache(NinjaScriptBase ninjascript) : base(ninjascript) { }
        /// <inheritdoc/>
        public RangeCache(NinjaScriptBase ninjascript, int capacity) : base(ninjascript, capacity) { }
        /// <inheritdoc/>
        public RangeCache(NinjaScriptBase ninjascript, int capacity, int displacement) : base(ninjascript, capacity, displacement) { }
        /// <inheritdoc/>
        public RangeCache(NinjaScriptBase ninjascript, int capacity, int displacement, int seriesIdx) : base(ninjascript, capacity, displacement, seriesIdx) { }

        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript, int ninjascriptSeriesIdx) =>
            ninjascript.Highs[SeriesIdx][Displacement] - ninjascript.Lows[SeriesIdx][Displacement];
        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

    }
}
