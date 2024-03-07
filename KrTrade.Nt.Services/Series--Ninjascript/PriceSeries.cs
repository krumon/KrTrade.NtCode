using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Double values cache. The new values of cache are the last value of the input series. 
    /// The cache current value is updated when the current value and the candidate value are different.
    /// </summary>
    public abstract class PriceSeries : DoubleSeries<ISeries<double>>, IPriceSeries
    {
        private readonly string _name;

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="PriceSeries"/>.</param>
        /// <param name="name">the name of Cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public PriceSeries(IBarsService input, string name, int capacity = Cache.DEFAULT_CAPACITY, int oldValuesCapacity = Cache.DEFAULT_OLD_VALUES_CAPACITY) : this(input?.Ninjascript.Inputs[input?.Index ?? 0], name, input?.Index ?? 0, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="PriceSeries"/>.</param>
        /// <param name="name">the name of Cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public PriceSeries(NinjaScriptBase input, string name, int barsIndex, int capacity = Cache.DEFAULT_CAPACITY, int oldValuesCapacity = Cache.DEFAULT_OLD_VALUES_CAPACITY) : this(input?.Inputs[barsIndex], name, barsIndex, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="PriceSeries"/>.</param>
        /// <param name="name">the name of Cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal PriceSeries(ISeries<double> input, string name, int barsIndex, int capacity, int oldValuesCapacity) : base(input, period : 1, capacity, oldValuesCapacity, barsIndex)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                _name = "UnknownPriceSeries";
            else _name = name;
        }

        public override string Name 
            => $"{_name}({Capacity})";
        protected override double GetCandidateValue() 
            => Input[0];
        protected override double ReplaceCurrentValue() 
            => GetCandidateValue();
        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaTrader.NinjaScript.PriceSeries priceSeries)
                return priceSeries;
            return null;
        }

    }
}
