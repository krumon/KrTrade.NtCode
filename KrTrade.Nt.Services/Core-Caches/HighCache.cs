using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighCache : DoubleCache<ISeries<double>>
    {

        private readonly int _barsIndex = 0;

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="HighCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighCache(ISeries<double> input, int period, int displacement = 0) : base(input, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="HighCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input, period, displacement)
        {
            _barsIndex = barsIndex;
        }

        protected override double GetCandidateValue() => Input[0];
        protected override double UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

        protected override ISeries<double> GetInput(ISeries<double> input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Highs[_barsIndex];

            return input;
        }
    }
}
