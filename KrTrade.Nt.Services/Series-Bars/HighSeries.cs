using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighSeries : PriceSeries
    {

        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="HighSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighSeries(IBarsService barsService) : this(barsService?.Ninjascript, barsService?.CacheCapacity ?? DEFAULT_CAPACITY, barsService?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY, barsService?.Index ?? 0)
        {
        }


        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="HighSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public HighSeries(NinjaScriptBase entry, int capacity, int oldValuesCapacity, int barsIndex) : base(entry, capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="HighSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets series.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public HighSeries(NinjaTrader.NinjaScript.PriceSeries input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name => $"High";

        public override NinjaTrader.NinjaScript.PriceSeries GetInput(NinjaScriptBase entry)
            => entry.Highs[BarsIndex];

    }
}
