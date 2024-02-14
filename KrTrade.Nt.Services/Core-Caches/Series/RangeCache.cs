using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeCache : DoubleCache<ISeries<double>>
    {
        /// <summary>
        /// Gets the second series necesary for calculate the values of cache.
        /// </summary>
        public ISeries<double> Input2 { get; private set; }

        /// <summary>
        /// Create <see cref="RangeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="RangeCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeCache(IBarsService input, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Highs[barsIndex], input?.Ninjascript.Lows[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="RangeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="RangeCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeCache(NinjaScriptBase input, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Highs[barsIndex], input?.Lows[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="RangeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input1">The high <see cref="ISeries{double}"/> series used to gets elements for <see cref="RangeCache"/>.</param>
        /// <param name="input2">The low <see cref="ISeries{double}"/> series used to gets elements for <see cref="RangeCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeCache(ISeries<double> input1, ISeries<double> input2, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input1, capacity, oldValuesCapacity, barsIndex)
        {
            Input2 = input2 ?? throw new System.ArgumentNullException(nameof(input2));
        }

        public override string Name 
            => $"Range({Capacity})";
        protected override double GetCandidateValue() 
            => Input[0] - Input2[0];
        protected override double UpdateCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) 
            => candidateValue > currentValue;
        protected override ISeries<double> GetInput(ISeries<double> input) 
            => input;

    }
}
