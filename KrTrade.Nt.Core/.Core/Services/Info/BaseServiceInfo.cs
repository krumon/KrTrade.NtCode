using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core
{
    public abstract class BaseServiceInfo : BaseInfo, IServiceInfo
    {
        new public ServiceType Type { get => base.Type.ToServiceType(); set => base.Type = value.ToElementType(); }

        protected BaseServiceInfo() : this(ServiceType.UNKNOWN) { }
        protected BaseServiceInfo(ServiceType type) : base(type.ToElementType()) {  }

    }

    public abstract class BaseServiceInfo<T> : BaseServiceInfo, IBaseServiceInfo<T>
    where T : Enum
    {
        private T _type;
        new public T Type
        {
            get => _type;
            set
            {
                base.Type = value.ToElementType().ToServiceType();
                _type = value;
            }
        }

    }

}
