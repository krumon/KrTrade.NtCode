using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series thats stored the lastest market data ticks.
    /// </summary>
    public class TickSeries : DoubleSeries<Bars,NinjaScriptBase>, ITickSeries, IDoubleSeries<Bars>
    {

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> thats are necesary to gets <see cref="VolumeSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        public TickSeries(IBarsService barsService) : this(barsService?.Ninjascript, barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService?.Index ?? 0)
        {
        }

        /// <summary>TickSeries
        /// Create <see cref="TickSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public TickSeries(NinjaScriptBase entry, int capacity, int oldValuesCapacity, int barsIndex) : base(entry, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="TickSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets series.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TickSeries(Bars input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        //public override string Name => "Tick";
        //public override string Key => $"{Name.ToUpper()}";

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input.TickCount;

        protected override bool CheckAddConditions(double lastValue, double candidateValue)
            => true;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

        public override Bars GetInput(NinjaScriptBase entry)
            => entry.BarsArray[BarsIndex];

    }
}
