using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the indexs of the bars series.
    /// </summary>
    public class CurrentBarSeries : IntSeries<int[]>, ICurrentBarSeries
    {

        /// <summary>
        /// Create <see cref="CurrentBarSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="CurrentBarSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public CurrentBarSeries(IBarsService input) : this(input?.Ninjascript.CurrentBars, input.CacheCapacity, input.RemovedCacheCapacity, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="CurrentBarSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="CurrentBarSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public CurrentBarSeries(NinjaScriptBase input, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.CurrentBars, capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="CurrentBarSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="CurrentBarSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal CurrentBarSeries(int[] input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name
            => $"CurentBar({Capacity})";

        public override int[] GetInput(object input)
        {
            if (input is int[] inputArray)
                return inputArray;
            return null;
        }

        protected override bool CheckAddConditions(int lastValue, int candidateValue)
            => true;

        protected override bool CheckUpdateConditions(int currentValue, int candidateValue)
            => false;

        protected override int GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input[BarsIndex]; 


    }
}
