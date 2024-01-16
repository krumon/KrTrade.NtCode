using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class SumCache : CacheSeriesCache
    {
        /// <inheritdoc/>
        public SumCache(ISeries<double> input) : base(input) { }

        protected override ISeries<double> GetSeries(ISeries<double> input, int seriesIdx)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Inputs[seriesIdx];

            return input;

        }
        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript = null)
        {
            if (Input.Count > Period)
                return this[0] + Input[0] - Input[Period];
            else
                return this[0] + Input[0];
        }
        protected sealed override double UpdateCurrentValue()
        {
            if (Input.Count > Period)
                return this[1] + Input[0] - Input[Period];
            else
                return this[1] + Input[0];
        }
        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => true;

    }
}
