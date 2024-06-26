using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Information;

namespace KrTrade.Nt.Core.Series
{

    public class SeriesCollectionInfo : CollectionInfo<ISeriesInfo, SeriesCollectionType>, ISeriesCollectionInfo
    {
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }

    }
}
