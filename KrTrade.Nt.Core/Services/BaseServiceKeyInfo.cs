using KrTrade.Nt.Core.Info;

namespace KrTrade.Nt.Core.Services
{
    public abstract class BaseServiceKeyInfo : BaseKeyInfo, IServiceInfo
    {
        public ServiceType Type { get; set; }

    }
}
