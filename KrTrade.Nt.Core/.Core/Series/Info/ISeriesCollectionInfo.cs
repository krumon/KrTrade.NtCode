namespace KrTrade.Nt.Core
{
    public interface ISeriesCollectionInfo : IInfoCollection<ISeriesInfo>
    {

        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }

    }
}
