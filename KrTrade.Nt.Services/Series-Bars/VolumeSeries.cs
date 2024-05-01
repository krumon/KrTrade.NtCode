using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series thats stored the lastest market data volumes.
    /// </summary>
    public class VolumeSeries : BaseNumericSeries, IVolumeSeries
    {

        public ISeries<double> Input {  get; set; }

        public VolumeSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.VOLUME,
                      bars?.CacheCapacity ?? DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public VolumeSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.VOLUME, capacity, oldValuesCapacity))
        {
        }

        public VolumeSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Name != BarsSeriesType.VOLUME.ToString())
                bars.PrintService.LogWarning($"Error configuring {nameof(TickSeries)}. The bars series type must be {BarsSeriesType.VOLUME}. The series type is going to be changed from {info.Type} to {BarsSeriesType.VOLUME}.");
            if (info.Inputs != null)
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series cannot have input series. The input series are going to be deleted.");

            info.Type = BarsSeriesType.VOLUME;
            info.Inputs = null;
        }

        internal override void Configure(out bool isConfigured) => isConfigured = true;
        internal override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.Volumes[Bars.Index];
            isDataLoaded = Input != null;
        }
        protected override double GetCandidateValue(bool isCandidateValueToUpdate) => Input[0];
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(double candidateValue) => candidateValue != CurrentValue;

    }
}
