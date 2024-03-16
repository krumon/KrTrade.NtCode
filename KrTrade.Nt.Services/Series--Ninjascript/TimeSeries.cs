using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Series thats stored the lastest market data time stamp.
    /// </summary>
    public class TimeSeries : DateTimeSeries<NinjaTrader.NinjaScript.TimeSeries, NinjaScriptBase>, ITimeSeries
    {

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> thats are necesary to gets <see cref="VolumeSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        public TimeSeries(IBarsService barsService) : this(barsService?.Ninjascript?.Times[barsService?.Index ?? 0], barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public TimeSeries(NinjaScriptBase entry, int capacity, int oldValuesCapacity, int barsIndex) : base(entry, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TimeSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets series.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TimeSeries(NinjaTrader.NinjaScript.TimeSeries input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name => "Time";
        public override string Key => $"{Name.ToUpper()}";

        public override NinjaTrader.NinjaScript.TimeSeries GetInput(NinjaScriptBase entry)
            => entry.Times[BarsIndex];

        protected override bool CheckAddConditions(DateTime lastValue, DateTime candidateValue)
            => true;

        protected override bool CheckUpdateConditions(DateTime currentValue, DateTime candidateValue)
            => false;

        protected override DateTime GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

    }
}
