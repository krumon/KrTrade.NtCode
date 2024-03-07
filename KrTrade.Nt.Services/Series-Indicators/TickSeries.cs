using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market data ticks.
    /// </summary>
    public class TickSeries : LongSeries<Bars[]>
    {

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="TickSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TickSeries(IBarsService input, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : this(input?.Ninjascript.BarsArray, input?.Index ?? 0, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TickSeries"/>.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TickSeries(NinjaScriptBase input, int barsIndex, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : this(input?.BarsArray, barsIndex, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="TickSeries"/>.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal TickSeries(Bars[] input, int barsIndex, int capacity, int oldValuesCapacity) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name 
            => $"Ticks({Capacity})";
        protected override long GetCandidateValue() 
            => Input[_barsIndex].TickCount;
        protected override long ReplaceCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToReplace(long currentValue, long candidateValue) 
            => candidateValue > currentValue;
        public override Bars[] GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.BarsArray;
            if (input is BarsService barsService)
                return barsService.Ninjascript.BarsArray;
            if (input is BarsMaster barsMaster)
                return barsMaster.Ninjascript.BarsArray;
            if (input is Bars[] barsArray)
                return barsArray;
            return null;
        }

    }
}
