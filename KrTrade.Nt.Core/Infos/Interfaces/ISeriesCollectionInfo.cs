using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;

namespace KrTrade.Nt.Core.Infos
{
    public interface ISeriesCollectionInfo : ICollectionInfo<ISeriesInfo,SeriesType,SeriesCollectionType>
    {
        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }
    }
}
