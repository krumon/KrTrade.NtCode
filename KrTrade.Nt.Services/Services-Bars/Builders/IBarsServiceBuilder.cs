using KrTrade.Nt.Core.Data;
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
        IBarsServiceBuilder ConfigureOptions(Action<BarsServiceOptions> barsServiceOptions);

        /// <summary>
        /// Adds new series to be used in the bars service.
        /// </summary>
        /// <param name="configureSeries">The delegate for configuring the <see cref="SeriesInfo"/> and the <see cref="SeriesOptions"/> 
        /// that will be usedto construct the <see cref="Series.ISeries"/></param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder AddSeries<TInfo,TOptions>(Action<TInfo,TOptions> configureSeries)
            where TInfo: BaseSeriesInfo, new()
            where TOptions: SeriesOptions, new();

        /// <summary>
        /// Adds new series to be used in the bars service.
        /// </summary>
        /// <param name="configureSeries">The delegate for configuring the <see cref="SeriesInfo"/> and the <see cref="SeriesOptions"/> 
        /// that will be usedto construct the <see cref="Series.ISeries"/></param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo,SeriesOptions> configureSeries)
            where TInfo: BaseSeriesInfo, new();

        /// <summary>
        /// Adds new series to be used in the bars service.
        /// </summary>
        /// <param name="configureSeries">The delegate for configuring the <see cref="SeriesInfo"/> and the <see cref="SeriesOptions"/> 
        /// that will be usedto construct the <see cref="Series.ISeries"/></param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder AddSeries(Action<SeriesInfo,SeriesOptions> configureSeries);

    }
}
