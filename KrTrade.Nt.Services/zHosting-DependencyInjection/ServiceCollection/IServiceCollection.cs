using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
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
}
