using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using System;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series thats stored the lastest market data time stamp.
    /// </summary>
    public class TimeSeries : BaseTimeSeries, ITimeSeries
    {
        public NinjaTrader.NinjaScript.TimeSeries Input { get; protected set; }

        public TimeSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.TIME,
                      bars?.CacheCapacity ?? DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public TimeSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.TIME, capacity, oldValuesCapacity))
        {
        }

        public TimeSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.TIME)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.TIME}. The series type is going to be changed from {info.Type} to {BarsSeriesType.TIME}.");
                info.Type = BarsSeriesType.TIME;
            }
        }

        internal override void Configure(out bool isConfigured) => isConfigured = true;
        internal override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.Times[Bars.Index];
            isDataLoaded = Input != null;
        }
        protected override DateTime GetCandidateValue(bool isCandidateValueToUpdate) => Input[0];
        protected override bool IsValidValueToAdd(DateTime candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(DateTime candidateValue) => false;
    }
}
