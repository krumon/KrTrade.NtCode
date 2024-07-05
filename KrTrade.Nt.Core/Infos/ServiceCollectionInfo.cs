using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Options;

namespace KrTrade.Nt.Core.Infos
{

    public class ServiceCollectionInfo<TElementInfo,TElementOptions> : BaseCollectionInfo<TElementInfo, ServiceType, ServiceCollectionType>, IServiceCollectionInfo<TElementInfo,TElementOptions>
        where TElementInfo : IServiceInfo
        where TElementOptions : IServiceOptions
    {
    }
}
