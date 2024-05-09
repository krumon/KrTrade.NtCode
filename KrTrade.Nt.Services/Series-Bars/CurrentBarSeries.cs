using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Cache to store the indexs of the bars series.
    /// </summary>
    public class CurrentBarSeries : BaseIntSeries, ICurrentBarSeries
    {
        public int[] Input {  get; protected set; }

        public CurrentBarSeries(IBarsService bars)
            : this(bars, 
                  new BarsSeriesInfo(
                      BarsSeriesType.CURRENT_BAR, 
                      bars?.CacheCapacity ?? DEFAULT_CAPACITY, 
                      bars?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public CurrentBarSeries(IBarsService bars, int capacity, int oldValuesCapacity) 
            : base(bars, new BarsSeriesInfo(BarsSeriesType.CURRENT_BAR,capacity,oldValuesCapacity))
        {
        }

        public CurrentBarSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.CURRENT_BAR)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.CURRENT_BAR}. The series type is going to be changed from {info.Type} to {BarsSeriesType.CURRENT_BAR}.");
                info.Type = BarsSeriesType.CURRENT_BAR;
            }
        }

        internal override void Configure(out bool isConfigured) => isConfigured = true;
        internal override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.CurrentBars;
            isDataLoaded = Input != null;
        }
        protected override int GetCandidateValue(bool isCandidateValueForUpdate) => Input[Bars.Index];
        protected override bool IsValidValueToAdd(int candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(int candidateValue) => false;

    }
}
