using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Double values cache. The new values of cache are the last value of the input series. 
    /// The cache current value is updated when the current value and the candidate value are different.
    /// </summary>
    public class PriceSeries : DoubleSeries<ISeries<double>>, IPriceSeries
    {
        private readonly string _name;

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="PriceSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public PriceSeries(IBarsService input, string name) : this(input?.Ninjascript.Inputs[input?.Index ?? 0], name, input.CacheCapacity, input.RemovedCacheCapacity, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="PriceSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public PriceSeries(NinjaScriptBase input, string name, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.Inputs[barsIndex], name, capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="PriceSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal PriceSeries(ISeries<double> input, string name, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                _name = "UnknownPriceSeries";
            else _name = name;
        }

        public override string Name
            => $"{_name}({Capacity})";

        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaTrader.NinjaScript.PriceSeries priceSeries)
                return priceSeries;
            return null;
        }

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

        protected override bool CheckAddConditions(double lastValue, double candidateValue)
            => true;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

    }
}
