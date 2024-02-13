using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest average prices for specified period.
    /// </summary>
    public class AvgCache : CalculateCache
    {
        private readonly SumCache _sumCache;

        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="AvgCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgCache(IBarsService input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.Ninjascript.Inputs[barsIndex], period, capacity, lengthOfRemovedCache)
        {
        }
        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="AvgCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgCache(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.Inputs[barsIndex], period, capacity, lengthOfRemovedCache)
        {
        }
        /// <summary>
        /// Create <see cref="AvgCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="AvgCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public AvgCache(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE) : base(input, period, capacity, lengthOfRemovedCache)
        {
            if (input is SumCache sumCache)
                _sumCache = sumCache;
        }

        public override string Name
            => $"Avg({Capacity})";
        protected override double GetCandidateValue()
        {
            if (Input != null)
                return Input[0] / Math.Min(Count,Period);

            _sumCache.Add();
            return _sumCache[0] / Math.Min(Count, Period);
        }
        protected override double UpdateCurrentValue()
        {
            if (Input != null)
                return Input[0] / Math.Min(Count, Period);

            _sumCache.Update();
            return _sumCache[0] / Math.Min(Count, Period);
        }
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue != currentValue;
        protected override ISeries<double> GetInput(ISeries<double> input)
        {
            if(input is SumCache)
                return input;

            return null;
        }

    }
}
