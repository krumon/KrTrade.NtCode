using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{

    public class SeriesCollectionInfo : CollectionInfo<ISeriesInfo, SeriesCollectionType>, ISeriesCollectionInfo
    {
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }

    }
}
