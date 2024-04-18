using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IBarsService"/> objects. 
    /// </summary>
    public interface IBaseBarsServiceBuilder 
    {

        //IBarsServicesBuilder AddFilters();
        //IBarsServicesBuilder AddIndicators();
        //IBarsServicesBuilder AddStats();

        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        /// </summary>
        /// <param name="barsManager">The 'NinjaScript' used to configure <see cref="IBarsService"/> instance.</param>
        /// <returns>An initialized <see cref="IBarsService"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsService Build(IBarsManager barsManager);

    }
}
