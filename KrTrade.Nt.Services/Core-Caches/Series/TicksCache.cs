using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class TicksCache : LongCache<Bars[]>
    {

        /// <summary>
        /// Create <see cref="TicksCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="TicksCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TicksCache(IBarsService input, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.Ninjascript.BarsArray, capacity, lengthOfRemovedCache, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TicksCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TicksCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TicksCache(NinjaScriptBase input, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.BarsArray, capacity, lengthOfRemovedCache, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TicksCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="TicksCache"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TicksCache(Bars[] input, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(input, capacity, lengthOfRemovedCache, barsIndex)
        {
        }

        public override string Name 
            => $"Ticks({Capacity})";
        protected override long GetCandidateValue() 
            => Input[BarsIndex].TickCount;
        protected override long UpdateCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(long currentValue, long candidateValue) 
            => candidateValue > currentValue;
        protected override Bars[] GetInput(Bars[] input) 
            => input;

    }
}
