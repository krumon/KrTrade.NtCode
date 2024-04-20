using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Cache to store the indexs of the bars series.
    /// </summary>
    public class CurrentBarSeries : IntSeries<int[],NinjaScriptBase>, ICurrentBarSeries
    {

        /// <summary>
        /// Create <see cref="CurrentBarSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="CurrentBarSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public CurrentBarSeries(IBarsService entry) : this(entry?.Ninjascript, entry.CacheCapacity, entry.RemovedCacheCapacity, entry?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="CurrentBarSeries"/> default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="CurrentBarSeries"/>.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public CurrentBarSeries(NinjaScriptBase entry, int capacity, int oldValuesCapacity, int barsIndex) : base(entry, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        //public override string Name => "CurrentBar";
        //public override string Key => $"{Name.ToUpper()}";

        public override int[] GetInput(NinjaScriptBase entry)
            => entry.CurrentBars;

        protected override bool CheckAddConditions(int lastValue, int candidateValue)
            => true;

        protected override bool CheckUpdateConditions(int currentValue, int candidateValue)
            => false;

        protected override int GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input[BarsIndex]; 


    }
}
