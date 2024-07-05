using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{

    public class SeriesCollection : BaseSeriesCollection<INumericSeries>, INumericSeriesCollection
    {

        public SeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo()) { }
        public SeriesCollection(IBarsService barsService, SeriesCollectionInfo info) : base(barsService, info,new ServiceOptions()) { }

        protected override string GetHeaderString() => "SERIES";
        protected override string GetParentString() => Bars.ToString();
        protected override string GetDescriptionString() => ToString();

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }

        protected override SeriesCollectionType ToElementType() => SeriesCollectionType.SERIES;
    }

    public abstract class SeriesCollection<TInfo> : SeriesCollection
        where TInfo : SeriesCollectionInfo, new()
    {
        new public TInfo Info => (TInfo)base.Info;

        protected SeriesCollection(IBarsService barsService) : this(barsService, new TInfo()) { }
        protected SeriesCollection(IBarsService barsService, TInfo info) : base(barsService, info) { }

    }
}
