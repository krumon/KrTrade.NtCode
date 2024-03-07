using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IBarsService"/> objects. 
    /// </summary>
    public interface IBarsServiceBuilder : IBarUpdateBuilder<IBarsService,BarsServiceOptions,IBarsServiceBuilder>
    {
        ///// <summary>
        ///// Adds the indicators configuration for the <see cref="IDataSeriesService"/> objects..
        ///// </summary>
        ///// <param name="configureIndicatorsDelegate">The delegate for configuring the <see cref="IndicatorsCollection"/> that will be used
        ///// to construct the <see cref="IDataSeriesService"/>.</param>
        ///// <returns>The same instance of the <see cref="IDataSeriesBuilder"/> for chaining.</returns>
        //IDataSeriesBuilder AddIndicators(Action<IIndicatorsBuilder> configureIndicatorsDelegate);

        //IDataSeriesBuilder AddFilters();
        //IDataSeriesBuilder UseStats();

    }
}
