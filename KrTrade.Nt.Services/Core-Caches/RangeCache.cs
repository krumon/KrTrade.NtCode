using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeCache : DoubleCache<NinjaScriptBase>
    {

        private readonly int _barsIndex = 0;

        /// <summary>
        /// Create <see cref="RangeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="RangeCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The 'NinjaScript Series' index.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input, period, displacement)
        {
            _barsIndex = barsIndex < 0 ? 0 : barsIndex;
        }

        protected override double GetCandidateValue() => Input.Highs[_barsIndex][0] - Input.Lows[_barsIndex][0];
        protected override double UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

        protected override NinjaScriptBase GetInput(NinjaScriptBase input) => input;

    }
}
