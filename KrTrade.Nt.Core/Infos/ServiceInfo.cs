using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Infos
{
    public class ServiceInfo : BaseInfo<ServiceType>, IServiceInfo
    {
        protected ServiceInfo() : this(ServiceType.UNKNOWN) { }
        protected ServiceInfo(ServiceType type) : base(type) {  }

    }
}
