using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public abstract class IndicatorsCache : DoubleCache<ISeries<double>>
    {
        public int Period { get;protected set; }

        protected IndicatorsCache(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, capacity,oldValuesCapacity,barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }
        protected IndicatorsCache(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY,int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, capacity,oldValuesCapacity, barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }
        protected IndicatorsCache(IBarsService input, int period, int capacity = DEFAULT_CAPACITY,int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input?.Ninjascript, capacity,oldValuesCapacity, barsIndex)
        {
            Period = period <= 0 ? 1 : period > capacity ? capacity : period;
        }

    }
}
