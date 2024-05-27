using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    public class BarsSeriesInfo : BaseSeriesInfo<BarsSeriesType>
    {
        public BarsSeriesInfo() : this(BarsSeriesType.INPUT, Core.Series.Series.DEFAULT_CAPACITY, Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type) : this(type, Core.Series.Series.DEFAULT_CAPACITY, Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity) : this(type, capacity, Core.Series.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
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
