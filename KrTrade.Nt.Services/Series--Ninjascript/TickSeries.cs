using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market data ticks.
    /// </summary>
    public class TickSeries : DoubleSeries<Bars>, ITickSeries
    {

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="TickSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TickSeries(IBarsService input) : this(input?.Ninjascript?.BarsArray[input?.Index ?? 0], input?.CacheCapacity ?? DEFAULT_CAPACITY, input?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TickSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TickSeries(NinjaScriptBase input, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.BarsArray[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="TickSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal TickSeries(Bars input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period : 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name 
            => $"Tick({Capacity})";

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input.TickCount;

        protected override bool CheckAddConditions(double lastValue, double candidateValue)
            => true;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

        public override Bars GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.BarsArray[BarsIndex];
            if (input is BarsService barsService)
                return barsService.Ninjascript.BarsArray[BarsIndex];
            if (input is BarsManager barsMaster)
                return barsMaster.Ninjascript.BarsArray[BarsIndex];
            if (input is Bars[] barsArray)
                return barsArray[BarsIndex];
            if (input is Bars bars)
                return bars;
            return null;
        }
    }
}
