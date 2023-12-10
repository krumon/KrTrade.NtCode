using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Optional contract used by Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService``1(System.IServiceProvider)
    /// to resolve services if supported by <see cref="IServiceProvider"/>.
    /// </summary>
    public interface ISupportRequiredService
    {
        /// <summary>
        /// Gets service of type serviceType from the System.IServiceProvider implementing
        /// this interface.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type serviceType. Throws an exception if the System.IServiceProvider
        /// cannot create the object.</returns>
        object GetRequiredService(Type serviceType);
    }
}
