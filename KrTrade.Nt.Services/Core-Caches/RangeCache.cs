using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeCache<TInput> : DoubleCache<TInput>
    {
        /// <summary>
        /// Create <see cref="RangeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="RangeCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        /// <exception cref="System.Exception">The <paramref name="input"/> must be <see cref="NinjaScriptBase"/> object or <see cref="IBarsCache"/> object.</exception>
        public RangeCache(TInput input, int period, int displacement) : base(input, period, displacement)
        {
        }

        protected override double GetCandidateValue() => Input is NinjaScriptBase n ? n.High[0] - n.Low[0] : Input is IBarsCache b ? b.High[0] - b.Low[0] : default;
        protected override double UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

        protected override TInput GetInput(TInput input)
        {
            if (input is NinjaScriptBase || input is IBarsCache)
                return input;

            throw new System.Exception("RangeCache needs a NinjaScript or BarsCache series.");
        }

    }
}
