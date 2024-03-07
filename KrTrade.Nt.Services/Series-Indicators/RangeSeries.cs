using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeSeries : DoubleSeries<ISeries<double>>
    {
        /// <summary>
        /// Gets the second series necesary for calculate the values of cache.
        /// </summary>
        public ISeries<double> Input2 { get; private set; }

        /// <summary>
        /// Create <see cref="RangeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to calculate the <see cref="RangeSeries"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeSeries(IBarsService input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.High, input?.Low, capacity, period, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="RangeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsSeries"/> instance used to calculate the <see cref="RangeSeries"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeSeries(IBarsSeries input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.High, input?.Low, capacity, period, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="RangeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="RangeSeries"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeSeries(NinjaScriptBase input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Highs[barsIndex], input?.Lows[barsIndex], capacity, period, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="RangeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input1">The high <see cref="ISeries{double}"/> series used to gets elements for <see cref="RangeSeries"/>.</param>
        /// <param name="input2">The low <see cref="ISeries{double}"/> series used to gets elements for <see cref="RangeSeries"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeSeries(ISeries<double> input1, ISeries<double> input2, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input1, capacity, period, oldValuesCapacity, barsIndex)
        {
            Input2 = input2 ?? throw new System.ArgumentNullException(nameof(input2));
        }

        public override string Name 
            => $"Range({Capacity})";
        protected override double GetCandidateValue() 
            => Input[0] - Input2[0];
        protected override double ReplaceCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToReplace(double currentValue, double candidateValue) 
            => candidateValue > currentValue;
        public override ISeries<double> GetInput(object input) 
            => input;

    }
}
