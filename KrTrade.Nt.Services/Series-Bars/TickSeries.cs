﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using NinjaTrader.Data;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series thats stored the lastest market data ticks.
    /// </summary>
    public class TickSeries : BaseNumericSeries, ITickSeries
    {

        public Bars Input {  get; set; }

        public TickSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.TICK,
                      bars?.CacheCapacity ?? DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public TickSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.TICK, capacity, oldValuesCapacity))
        {
        }

        public TickSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.TICK)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.TICK}. The series type is going to be changed from {info.Type} to {BarsSeriesType.TICK}.");
                info.Type = BarsSeriesType.TICK;
            }
        }

        internal override void Configure(out bool isConfigured) => isConfigured = true;
        internal override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.BarsArray[Bars.Index];
            isDataLoaded = Input != null;
        }
        protected override double GetCandidateValue(bool isCandidateValueForUpdate) => Input.TickCount;
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(double candidateValue) => candidateValue != CurrentValue;

    }
}
