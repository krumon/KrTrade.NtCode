using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Services.Series;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IBarsService"/> objects. 
    /// </summary>
    public interface IBarsServiceBuilder : IBaseBarsServiceBuilder
    {
        /// <summary>
        /// Sets up the options for the primary data series objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="barsServiceOptions">The delegate for configuring the <see cref="BarsServiceOptions"/> that will be used
        /// to construct the <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder ConfigureOptions(Action<BarsServiceInfo, BarsServiceOptions> barsServiceOptions);

        /// <summary>
        /// Adds new series to be used in the bars service.
        /// </summary>
        /// <param name="configureSeries">The delegate for configuring the <see cref="SeriesServiceInfo"/> and the <see cref="SeriesServiceOptions"/> 
        /// that will be usedto construct the <see cref="Series.ISeries"/></param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo> configureSeries)
            where TInfo : IInputSeriesInfo, new();

        /// <summary>
        /// Adds new series to be used in the bars service.
        /// </summary>
        /// <param name="configureSeries">The delegate for configuring the <see cref="SeriesServiceInfo"/> and the <see cref="SeriesServiceOptions"/> 
        /// that will be usedto construct the <see cref="Series.ISeries"/></param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder AddSeries_Period(Action<PeriodSeriesInfo> configureSeries);

    }
}
