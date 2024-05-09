using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class HighSeries : PriceSeries
    {
        public HighSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.HIGH,
                      bars?.CacheCapacity ?? DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public HighSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.HIGH, capacity, oldValuesCapacity))
        {
        }

        public HighSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.HIGH)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.HIGH}. The series type is going to be changed from {info.Type} to {BarsSeriesType.HIGH}.");
                info.Type = BarsSeriesType.HIGH;
            }
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.Highs[Bars.Index];
            isDataLoaded = Input != null;
        }
    }
}
