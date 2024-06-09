namespace KrTrade.Nt.Core.Elements
{
    public interface ISeriesCollectionInfo : IInfoCollection<ISeriesInfo>
    {

        int Capacity { get; set; }
        int OldValuesCapacity { get; set; }

    }
}
