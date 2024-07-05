using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Options;

namespace KrTrade.Nt.Core.Infos
{
    public interface IServiceCollectionInfo<TElementInfo,TElementOptions> : ICollectionInfo<TElementInfo, ServiceType, ServiceCollectionType>
        where TElementInfo : IServiceInfo
        where TElementOptions : IServiceOptions
    {
    }
    public interface IServiceCollectionInfo : IServiceCollectionInfo<IServiceInfo, IServiceOptions>
    {
    }
}
