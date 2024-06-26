using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Information;

namespace KrTrade.Nt.Core.Series
{
    public interface ISeriesCollectionInfo : ICollectionInfo<ISeriesInfo,SeriesCollectionType>
    {
        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }
    }
}
