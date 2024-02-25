using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighCache : DoubleCache<ISeries<double>>
    {

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="HighCache"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighCache(IBarsService input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Highs[barsIndex], capacity,period, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="HighCache"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighCache(NinjaScriptBase input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Highs[barsIndex], capacity,period, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="HighCache"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighCache(ISeries<double> input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, capacity,period,oldValuesCapacity, barsIndex)
        {
        }

        public override string Name 
            => $"High({Capacity})";
        protected override double GetCandidateValue() 
            => Input[0];
        protected override double UpdateCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) 
            => candidateValue > currentValue;
        protected override ISeries<double> GetInput(ISeries<double> input) 
            => input;

    }
}
