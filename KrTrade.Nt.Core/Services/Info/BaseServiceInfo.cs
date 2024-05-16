using KrTrade.Nt.Core.Info;

namespace KrTrade.Nt.Core.Services
{
    public abstract class BaseServiceInfo : BaseInfo, IServiceInfo
    {
        public ServiceType Type { get; set; }

        protected BaseServiceInfo()
        {
        }
        protected BaseServiceInfo(ServiceType type)
        {
            Type = type;
        }

    }
}
