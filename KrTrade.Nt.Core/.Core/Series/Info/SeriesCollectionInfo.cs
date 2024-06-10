using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{

    public class SeriesCollectionInfo : BaseInfoCollection<ISeriesInfo>, ISeriesCollectionInfo
    {
        new public SeriesCollectionType Type { get => base.Type.ToSeriesCollectionType(); set => base.Type = value.ToElementType(); }
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }

        public string ToString(string owner)
        {
            throw new System.NotImplementedException();
        }

        protected override string ToUniqueString()
        {
            throw new System.NotImplementedException();
        }
    }
}
