using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrTrade.NtCode.DependencyInjection
{
    /// <summary>
    /// A factory for creating instances of Microsoft.Extensions.DependencyInjection.IServiceScope,
    /// which is used to create services within a scope.
    /// </summary>
    public interface IServiceScopeFactory
    {
        /// <summary>
        /// Create an Microsoft.Extensions.DependencyInjection.IServiceScope which contains
        /// an System.IServiceProvider used to resolve dependencies from a newly created
        /// scope.
        /// </summary>
        /// <returns>
        /// An Microsoft.Extensions.DependencyInjection.IServiceScope controlling the lifetime
        /// of the scope. Once this is disposed, any scoped services that have been resolved
        /// from the Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider
        /// will also be disposed.
        /// </returns>
        IServiceScope CreateScope();
        
    }
}
