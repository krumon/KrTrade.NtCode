using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighCache : NinjascriptSeriesCache
    {
        /// <inheritdoc/>
        public HighCache(ISeries<double> input) : base(input){ }

        /// <inheritdoc/>
        public HighCache(ISeries<double> input, int capacity) : base(input, capacity) { }

        /// <inheritdoc/>
        public HighCache(ISeries<double> input, int capacity, int displacement) : base(input, capacity, displacement) 
        {
        }

        /// <inheritdoc/>
        public HighCache(ISeries<double> input, int capacity, int displacement, int seriesIdx) : base(input, capacity, displacement,seriesIdx) 
        {
        }

        protected override ISeries<double> GetSeries(ISeries<double> input, int seriesIdx)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Highs[seriesIdx];

            return input;
        }

        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

    }
}
