using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series thats stored the lastest market data volumes.
    /// </summary>
    public class VolumeSeries : BaseNumericSeries, IVolumeSeries
    {

        public NinjaTrader.NinjaScript.ISeries<double> Input {  get; set; }

        public VolumeSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.VOLUME,
                      bars?.CacheCapacity ?? Globals.SERIES_DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public VolumeSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.VOLUME, capacity, oldValuesCapacity))
        {
        }

        public VolumeSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.VOLUME)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.VOLUME}. The series type is going to be changed from {info.Type} to {BarsSeriesType.VOLUME}.");
                info.Type = BarsSeriesType.VOLUME;
            }
        }

        protected override SeriesType ToElementType() => SeriesType.VOLUME;
        protected override void Configure(out bool isConfigured) => isConfigured = true;
        protected override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.Volumes[Bars.Index];
            isDataLoaded = Input != null;
        }
        protected override double GetCandidateValue(bool isCandidateValueToUpdate) => Input[0];
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(double candidateValue) => candidateValue != CurrentValue;

    }
}
