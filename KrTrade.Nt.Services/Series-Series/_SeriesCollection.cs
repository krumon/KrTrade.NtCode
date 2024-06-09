﻿using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services.Series
{

    public class SeriesCollection : BaseNinjascriptSeriesCollection<INumericSeries, SeriesCollectionInfo>, INumericSeriesCollection
    {

        public SeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo()) { }
        public SeriesCollection(IBarsService barsService, SeriesCollectionInfo info) : base(barsService, info) { }

        protected override string GetHeaderString() => "SERIES";
        protected override string GetParentString() => Bars.ToString();
        protected override string GetDescriptionString() => ToString();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class SeriesCollection<TInfo> : SeriesCollection
        where TInfo : SeriesCollectionInfo, new()
    {
        new public TInfo Info => (TInfo)base.Info;

        protected SeriesCollection(IBarsService barsService) : this(barsService, new TInfo()) { }
        protected SeriesCollection(IBarsService barsService, TInfo info) : base(barsService, info) { }

    }
}
