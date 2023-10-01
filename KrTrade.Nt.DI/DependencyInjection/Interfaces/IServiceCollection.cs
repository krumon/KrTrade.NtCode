
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.DI.DependencyInjection
{
    /// <summary>
    /// Specifies the contract for a collection of ninjascript descriptors.
    /// </summary>
    public interface IServiceCollection : 
        IList<ServiceDescriptor>,
        ICollection<ServiceDescriptor>,
        IEnumerable<ServiceDescriptor>,
        IEnumerable
    {
    }
    /// <summary>
    /// Specifies the contract for a collection of ninjascript descriptors.
    /// </summary>
    public interface IServiceCollection<T> : 
        IList<T>,
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable
        where T : IServiceDescriptor
    {
    }
}
