using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Information;

namespace KrTrade.Nt.Core.Services
{
    public class ServiceInfo : Info, IServiceInfo
    {
        new public ServiceType Type { get => base.Type.ToServiceType(); set => base.Type = value.ToElementType(); }

        protected ServiceInfo() : this(ServiceType.UNKNOWN) { }
        protected ServiceInfo(ServiceType type) : base(type.ToElementType()) {  }

    }

    //public class BaseServiceInfo<T> : ServiceInfo, IServiceInfo
    //where T : Enum
    //{
    //    private T _type;
    //    new public T Type
    //    {
    //        get => _type;
    //        set
    //        {
    //            base.Type = value.ToElementType().ToServiceType();
    //            _type = value;
    //        }
    //    }
    //}
}
