using KrTrade.Nt.Core.Info;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines the information of the service.
    /// </summary>
    public interface IServiceInfo : IInfo
    {
        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        ServiceType Type { get; set; }
    }
}
