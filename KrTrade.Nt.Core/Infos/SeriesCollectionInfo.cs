using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Infos
{

    public class SeriesCollectionInfo : BaseCollectionInfo<ISeriesInfo, SeriesType, SeriesCollectionType>, ISeriesCollectionInfo
    {
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }
    }
}
