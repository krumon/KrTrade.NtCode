using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighSeries : PriceSeries
    {

        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="HighSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighSeries(IBarsService input) : this(input?.Ninjascript?.High, input?.CacheCapacity ?? DEFAULT_CAPACITY, input?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="HighSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighSeries(NinjaScriptBase input, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.Highs[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="HighSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal HighSeries(ISeries<double> input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, "High", capacity, oldValuesCapacity, barsIndex)
        {
        }
    }
}
