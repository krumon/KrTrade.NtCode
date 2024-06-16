using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services.Series
{

    public class BarsSeriesInfo : SeriesInfo<BarsSeriesType>
    {
        public BarsSeriesInfo() : this(BarsSeriesType.INPUT, Core.BaseSeries.DEFAULT_CAPACITY, Core.BaseSeries.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type) : this(type, Core.BaseSeries.DEFAULT_CAPACITY, Core.BaseSeries.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity) : this(type, capacity, Core.BaseSeries.DEFAULT_OLD_VALUES_CAPACITY) { }
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
