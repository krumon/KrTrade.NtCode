using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{

    public class ServiceCollectionInfo : CollectionInfo<IServiceInfo>, IServiceCollectionInfo
    {
        new public ServiceCollectionType Type { get => base.Type.ToServiceCollectionType(); set => base.Type = value.ToElementType(); }

    }
}
