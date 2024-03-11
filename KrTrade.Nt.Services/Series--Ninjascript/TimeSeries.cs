using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the time of the bars series.
    /// </summary>
    public class TimeSeries : DateTimeSeries<ISeries<DateTime>>, ITimeSeries
    {

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="TimeSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TimeSeries(IBarsService input) : this(input?.Ninjascript?.Times[input?.Index ?? 0], input.CacheCapacity, input.RemovedCacheCapacity, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TimeSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TimeSeries(NinjaScriptBase input, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.Times[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="TimeSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal TimeSeries(NinjaTrader.NinjaScript.TimeSeries input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name
            => $"Time({Capacity})";

        public override ISeries<DateTime> GetInput(object input)
        {
            if (input is NinjaTrader.NinjaScript.TimeSeries timeSeries)
                return (ISeries<DateTime>)timeSeries;
            return null;
        }

        protected override bool CheckAddConditions(DateTime lastValue, DateTime candidateValue)
            => true;

        protected override bool CheckUpdateConditions(DateTime currentValue, DateTime candidateValue)
            => false;

        protected override DateTime GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

    }
}
