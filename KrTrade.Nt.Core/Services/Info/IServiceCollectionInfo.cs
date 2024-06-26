using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Information;

namespace KrTrade.Nt.Core.Services
{
    public interface IServiceCollectionInfo<TInfo,TOptions> : ICollectionInfo<TInfo, ServiceCollectionType>
        where TInfo : IServiceInfo
        where TOptions : IServiceOptions
    {
    }
    public interface IServiceCollectionInfo : ICollectionInfo<IServiceInfo, ServiceCollectionType>
    {
    }
}
