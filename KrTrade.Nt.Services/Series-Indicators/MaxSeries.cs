using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MaxSeries : IndicatorSeries
    {

        /// <summary>
        /// Create <see cref="MaxSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="MaxSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxSeries(IBarsService input, int period) : this(input?.High, period, input?.CacheCapacity ?? DEFAULT_CAPACITY, input?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="MaxSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="MaxSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxSeries(INumericSeries<double> input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, "MAX", period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override INumericSeries<double> GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return (INumericSeries<double>)ninjascript.Highs[BarsIndex];
            if (input is BarsService barsService)
                return barsService.High;
            if (input is BarsManager barsMaster)
                return barsMaster.Highs[BarsIndex];
            if (input is INumericSeries<double> series)
                return series;

            return null;
        }

        protected override double GetInitValuePreviousRecalculate() 
            => double.MinValue;

        protected override bool CheckAddConditions(double currentValue, double candidateValue) 
            => candidateValue >= currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue) 
            => candidateValue >= currentValue;

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

    }
}
