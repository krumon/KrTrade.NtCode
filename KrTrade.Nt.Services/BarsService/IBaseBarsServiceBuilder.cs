using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
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
        /// <param name="isPrimaryDataSeries">Indicates if the bars service to construct is the 'NinjaScript' primary data series.</param>
        /// <returns>An initialized <see cref="IBarsService"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsService Build(IBarsManager barsManager, bool isPrimaryDataSeries = false);

    }
}
