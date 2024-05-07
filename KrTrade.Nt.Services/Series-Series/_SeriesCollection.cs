using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    public class SeriesCollection : BaseNinjascriptSeriesCollection<INumericSeries>, INumericSeriesCollection
    {

        public SeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo()) { }
        public SeriesCollection(IBarsService barsService, SeriesCollectionInfo info) : base(barsService, info) { }
        public SeriesCollection(IBarsService barsService, SeriesCollectionInfo info, int capacity) : base(barsService, info, capacity) { }

    }

    public abstract class SeriesCollection<TInfo> : SeriesCollection
        where TInfo : SeriesCollectionInfo, new()
    {
        protected new TInfo _info;
        public new TInfo Info { get => _info ?? new TInfo(); protected set { _info = value; } }

        protected SeriesCollection(IBarsService barsService) : this(barsService, new TInfo()) { }
        protected SeriesCollection(IBarsService barsService, TInfo info) : base(barsService, info) { }
        protected SeriesCollection(IBarsService barsService, TInfo info, int capacity) : base(barsService, info, capacity) { }

    }
}
