using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public abstract class CalculateCache : DoubleCache<ISeries<double>>
    {
        public int Period { get;protected set; }

        public CalculateCache(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(input, capacity,lengthOfRemovedCache,barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }
        public CalculateCache(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY,int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(input, capacity,lengthOfRemovedCache, barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }
        public CalculateCache(IBarsService input, int period, int capacity = DEFAULT_CAPACITY,int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(input?.Ninjascript, capacity,lengthOfRemovedCache, barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }

    }
}
