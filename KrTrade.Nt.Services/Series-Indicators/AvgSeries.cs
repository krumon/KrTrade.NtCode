using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest average prices for specified period.
    /// </summary>
    public class AvgSeries : IndicatorSeries<SumSeries>
    {

        /// <summary>
        /// Create <see cref="AvgSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="AvgSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgSeries(SumSeries input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : base(input, "Avg", period, capacity, oldValuesCapacity, 0)
        {
        }

        public override string Name
            => $"Avg({Input.Name})";

        public override INumericSeries<double> GetInput(object input)
        {
            if (input is SumSeries sumSeries)
                return sumSeries;

            return null;
        }

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input[0] / Math.Min(Count, Period);

        protected override double GetInitValuePreviousRecalculate()
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue) 
            => candidateValue != currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue) 
            => candidateValue != currentValue;

    }
}
