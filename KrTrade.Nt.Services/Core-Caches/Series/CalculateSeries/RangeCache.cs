using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeCache : CalculateSeriesCache
    {
        /// <inheritdoc/>
        public RangeCache() : base() { }
        /// <inheritdoc/>
        public RangeCache(int period) : base(period) { }
        /// <inheritdoc/>
        public RangeCache(int period, int displacement) : base(period, displacement) { }
        /// <inheritdoc/>
        public RangeCache(int period, int displacement, int seriesIdx) : base(period, displacement, seriesIdx) { }

        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript) =>
            ninjascript.Highs[SeriesIdx][Displacement] - ninjascript.Lows[SeriesIdx][Displacement];

        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

    }
}
