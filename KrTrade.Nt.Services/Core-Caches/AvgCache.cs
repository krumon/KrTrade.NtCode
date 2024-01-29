using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest average prices for specified period.
    /// </summary>
    public class AvgCache : DoubleCache<ISeries<double>>
    {
        private readonly SumCache _sumCache;

        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="AvgCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        /// <exception cref="System.Exception">The <paramref name="input"/> must be <see cref="NinjaScriptBase"/> object or <see cref="IBarsCache"/> object.</exception>
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

        protected override ISeries<double> GetInput(ISeries<double> input) => input;

    }
}
