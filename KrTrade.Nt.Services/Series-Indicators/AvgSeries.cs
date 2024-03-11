using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest average prices for specified period.
    /// </summary>
    public class AvgSeries : IndicatorSeries
    {
        private readonly SumSeries _sumSeries;

        ///// <summary>
        ///// Create <see cref="AvgSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="IBarsMaster"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="AvgSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public AvgSeries(IBarsMaster input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Inputs[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}
        ///// <summary>
        ///// Create <see cref="AvgSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="AvgSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public AvgSeries(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Inputs[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}

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
            if (input is SumSeries sumCache)
                _sumSeries = sumCache;
        }

        public override string Name
            => $"Avg({Capacity})";
        public override INumericSeries<double> GetInput(object input)
        {
            if (input is SumSeries sumSeries)
                return sumSeries;

            return null;
        }

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
        {
            if (Input != null)
                return Input[0] / Math.Min(Count, Period);

            _sumSeries.Add();
            return _sumSeries[0] / Math.Min(Count, Period);
        }

        protected override double GetInitValuePreviousRecalculate()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckAddConditions(double currentValue, double candidateValue)
        {
            throw new NotImplementedException();
        }

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
        {
            throw new NotImplementedException();
        }

        //protected override double UpdateCurrentValue()
        //{
        //    if (Input != null)
        //        return Input[0] / Math.Min(Count, Period);

        //    _sumSeries.Update();
        //    return _sumSeries[0] / Math.Min(Count, Period);
        //}
        //protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue != currentValue;

    }
}
