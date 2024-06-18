using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{
    public interface IServiceCollectionInfo : ICollectionInfo<IServiceInfo, ServiceCollectionType>
    {
        new ServiceCollectionType Type { get; set; }
    }
}
