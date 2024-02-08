using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest summary prices for specified period.
    /// </summary>
    public class SumCache : DoubleCache<ISeries<double>>
    {
        private readonly int _barsIndex = 0;

        /// <summary>
        /// Create <see cref="VolumeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="VolumeCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public SumCache(ISeries<double> input, int period, int displacement) : base(input, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="SumCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="SumCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public SumCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input, period, displacement)
        {
            _barsIndex = barsIndex;
        }

        protected override double GetCandidateValue()
        {
            if (Input.Count > Period)
                return this[0] + Input[0] - Input[Period];
            else
                return this[0] + Input[0];
        }
        protected override double UpdateCurrentValue()
        {
            if (Input.Count > Period)
                return this[1] + Input[0] - Input[Period];
            else
                return this[1] + Input[0];
        }
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue != currentValue;

        protected override ISeries<double> GetInput(ISeries<double> input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Inputs[_barsIndex];

            return input;
        }
    }
}
