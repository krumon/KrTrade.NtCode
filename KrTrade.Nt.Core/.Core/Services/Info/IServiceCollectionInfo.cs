using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{
    public interface IServiceCollectionInfo : IInfoCollection<IServiceInfo>
    {
        new ServiceCollectionType Type { get; set; }
    }
}
