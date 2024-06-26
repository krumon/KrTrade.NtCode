using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    public class BarsSeriesInfo : SeriesInfo<BarsSeriesType>
    {
        public BarsSeriesInfo() : this(BarsSeriesType.INPUT, Globals.SERIES_DEFAULT_CAPACITY, Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type) : this(type, Globals.SERIES_DEFAULT_CAPACITY, Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY) { }
        public BarsSeriesInfo(BarsSeriesType type, int capacity) : this(type, capacity, Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY) { }
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
