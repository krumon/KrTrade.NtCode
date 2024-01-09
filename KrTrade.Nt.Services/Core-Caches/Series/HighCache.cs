using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighCache : PriceSeriesCache
    {
        /// <inheritdoc/>
        public HighCache(ISeries<double> series) : base(series){ }

        /// <inheritdoc/>
        public HighCache(ISeries<double> series, int capacity) : base(series, capacity) { }

        /// <inheritdoc/>
        public HighCache(ISeries<double> series, int capacity, int displacement) : base(series, capacity, displacement) { }

        /// <inheritdoc/>
        public HighCache(NinjaScriptBase ninjascript) : base(ninjascript) { }

        /// <inheritdoc/>
        public HighCache(NinjaScriptBase ninjascript, int capacity) : base(ninjascript,capacity) { }

        /// <inheritdoc/>
        public HighCache(NinjaScriptBase ninjascript, int capacity, int displacement) : base(ninjascript,capacity,displacement) { }

        /// <inheritdoc/>
        public HighCache(NinjaScriptBase ninjascript, int capacity, int displacement, int seriesIdx) : base(ninjascript,capacity,displacement,seriesIdx) { }

        protected override ISeries<double> GetNinjascriptSeries(NinjaScriptBase ninjascript, int seriesIdx) => ninjascript.Highs[seriesIdx];
        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

    }
}
