using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services.Series
{

    public class BarsSeriesInfo : SeriesInfo<BarsSeriesType>
    {
        public BarsSeriesInfo() : this(BarsSeriesType.INPUT, Core.Series.DEFAULT_CAPACITY, Core.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type) : this(type, Core.Series.DEFAULT_CAPACITY, Core.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity) : this(type, capacity, Core.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity, int oldValuesCapacity)
        {
            Type = type;
            Capacity = capacity;
            OldValuesCapacity = oldValuesCapacity;
        }

        protected override string GetInputsKey() => string.Empty;
        protected override object[] GetParameters() => null;

    }
}
