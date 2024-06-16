using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{
    public interface ISeriesCollectionInfo : ICollectionInfo<ISeriesInfo,SeriesCollectionType>
    {
        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }
    }
}
