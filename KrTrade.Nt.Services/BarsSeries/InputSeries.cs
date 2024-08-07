﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class InputSeries : PriceSeries
    {
        public InputSeries(IBarsService bars)
            : this(bars,
                  new BarsSeriesInfo(
                      BarsSeriesType.INPUT,
                      bars?.CacheCapacity ?? Globals.SERIES_DEFAULT_CAPACITY,
                      bars?.RemovedCacheCapacity ?? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY))
        {
        }

        public InputSeries(IBarsService bars, int capacity, int oldValuesCapacity)
            : this(bars, new BarsSeriesInfo(BarsSeriesType.INPUT, capacity, oldValuesCapacity))
        {
        }

        public InputSeries(IBarsService bars, BarsSeriesInfo info) : base(bars, info)
        {
            if (info.Type != BarsSeriesType.INPUT)
            {
                bars.PrintService.LogWarning($"Error configuring {Name} series. The series type must be {BarsSeriesType.INPUT}. The series type is going to be changed from {info.Type} to {BarsSeriesType.INPUT}.");
                info.Type = BarsSeriesType.INPUT;
            }

        }

        protected override SeriesType ToElementType() => SeriesType.CURRENT_BAR;
        protected override void DataLoaded(out bool isDataLoaded)
        {
            Input = Bars.Ninjascript.Inputs[Bars.Index];
            isDataLoaded = Input != null;
        }
    }
}
