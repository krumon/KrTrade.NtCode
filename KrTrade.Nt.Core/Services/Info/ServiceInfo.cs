namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Provides the information for any service.
    /// </summary>
    public class ServiceInfo : BaseServiceInfo
    {
        public ServiceInfo() : base()
        {
        }
        public ServiceInfo(ServiceType type) : base(type)
        {
        }
    }
}
