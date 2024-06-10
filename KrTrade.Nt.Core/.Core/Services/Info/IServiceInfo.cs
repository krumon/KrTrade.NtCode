using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core   
{
    /// <summary>
    /// Defines the information of the service.
    /// </summary>
    public interface IServiceInfo : IBaseServiceInfo<ServiceType>
    {

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        new ServiceType Type { get; set; }

    }

    public interface IServiceInfo<T> : IServiceInfo
        where T : Enum
    {

        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        new T Type { get; set; }

    }
}
