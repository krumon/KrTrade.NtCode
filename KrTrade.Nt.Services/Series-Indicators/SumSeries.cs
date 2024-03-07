using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest summary prices for specified period.
    /// </summary>
    public class SumSeries : DoubleSeries<ISeries<double>>
    {
        ///// <summary>
        ///// Create <see cref="SumSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="IBarsMaster"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="SumSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public SumSeries(IBarsMaster input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Inputs[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="SumSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="SumSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public SumSeries(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input?.Inputs[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}

        /// <summary>
        /// Create <see cref="SumSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="SumSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public SumSeries(object input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name 
            => $"Sum({Period})";
        protected override double GetCandidateValue() 
            => Input.Count > Period ? this[0] + Input[0] - Input[Period] : this[0] + Input[0];
        protected override double ReplaceCurrentValue()
            => Input.Count > Period ? this[1] + Input[0] - Input[Period] : this[1] + Input[0];
        protected override bool IsValidCandidateValueToReplace(double currentValue, double candidateValue) 
            => candidateValue != currentValue;
        public override ISeries<double> GetInput(object input)
        {
            if (input is ISeries<double> priceSeries)
                return priceSeries;
            return null;
        }

    }
}
