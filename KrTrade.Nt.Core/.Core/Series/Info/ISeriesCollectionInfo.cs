using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{
    public interface ISeriesCollectionInfo : IInfoCollection<ISeriesInfo>
    {
        new SeriesCollectionType Type { get; set; }
        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }

    }
}
