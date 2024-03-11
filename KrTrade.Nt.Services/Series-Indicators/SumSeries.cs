using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest summary prices for specified period.
    /// </summary>
    public class SumSeries : IndicatorSeries
    {

        /// <summary>
        /// Create <see cref="SumSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="SumSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public SumSeries(object input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, "SUM", period, capacity, oldValuesCapacity, barsIndex)
        {
        }
        
        public override string Name 
            => $"Sum({Period})";
        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
        {
            if (!isCandidateValueForUpdate)
            {
                if (Count == 0 && barsAgo < Input.Count)
                    return Input[barsAgo];
                if (Input.Count > Period)
                    return this[barsAgo] + Input[barsAgo] - Input[Period];
                else
                    return this[barsAgo] + Input[barsAgo];
            }
            return Input.Count > Period && barsAgo + 1 < Input.Count ? this[1] + Input[0] - Input[Period] : this[1] + Input[0];
        }

        protected override double GetInitValuePreviousRecalculate()
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

        public override INumericSeries<double> GetInput(object input)
        {
            if (input is INumericSeries<double> series)
                return series;
            return null;
        }

    }
}
