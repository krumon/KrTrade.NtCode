using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest average prices for specified period.
    /// </summary>
    public class AvgCache : DoubleCache<ISeries<double>>
    {
        private readonly int _barsIndex = 0;
        private readonly SumCache _sumCache;

        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="AvgCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgCache(ISeries<double> input, int period, int displacement) : base(input, period, displacement)
        {
            if (input is SumCache sumCache)
            {
                Displacement = sumCache.Displacement;
                Period = sumCache.Period;
            }
            else
                _sumCache = new SumCache(input, period, displacement);
        }

        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="AvgCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input, period, displacement)
        {
            _barsIndex = barsIndex;
        }

        protected override double GetCandidateValue()
        {
            if (_sumCache == null)
                return Input[0] / Period < Count ? Count : Period;

            _sumCache.Add();
            return _sumCache[0] / Period < Count ? Count : Period;
        }
        protected override double UpdateCurrentValue()
        {
            if (_sumCache == null)
                return Input[0] / Period < Count ? Count : Period;

            _sumCache.Update();
            return _sumCache[0] / Period < Count ? Count : Period;
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
