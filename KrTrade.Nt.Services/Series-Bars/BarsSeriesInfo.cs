using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services.Series
{

    public class BarsSeriesInfo : SeriesInfo<BarsSeriesType>
    {
        public BarsSeriesInfo() : this(BarsSeriesType.INPUT, Core.Elements.Series.DEFAULT_CAPACITY, Core.Elements.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type) : this(type, Core.Elements.Series.DEFAULT_CAPACITY, Core.Elements.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity) : this(type, capacity, Core.Elements.Series.DEFAULT_OLD_VALUES_CAPACITY) { }
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
