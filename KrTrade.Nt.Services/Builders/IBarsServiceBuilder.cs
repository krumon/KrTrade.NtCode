using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="Core.Services.IBarsService"/> objects. 
    /// </summary>
    public interface IBarsServiceBuilder //: IBaseBarsServiceBuilder
    {

        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IBarsService"/> with specified user configuration.</returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsService Build();

        ///// <summary>
        ///// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        ///// </summary>
        ///// <param name="ninjascript">The 'NinjaScript' used to configure <see cref="IBarsService"/> instance.</param>
        ///// <param name="printService">The 'PrintService' used as logger in the service.</param>
        ///// <param name="addDataSeriesDelegate">Delegate method necesary to add the 'DataSeries' to the 'NinjaScript'.</param>
        ///// <returns>An initialized <see cref="IBarsService"/> with specified user configuration.</returns>
        ///// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        //IBarsService Build(NinjaScriptBase ninjascript, IPrintService printService, Action<string, BarsPeriod, string> addDataSeriesDelegate = null);

        ///// <summary>
        ///// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        ///// </summary>
        ///// <param name="service">The service used to configure <see cref="IBarsService"/> instance.</param>
        ///// <param name="addDataSeriesDelegate">Delegate method necesary to add the 'DataSeries' to the 'NinjaScript'.</param>
        ///// <returns>An initialized <see cref="IBarsService"/> with specified user configuration.</returns>
        ///// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        //IBarsService Build(IService service, Action<string, BarsPeriod, string> addDataSeriesDelegate = null);

        /// <summary>
        /// Sets up the options for the primary data series objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="barsServiceOptions">The delegate for configuring the <see cref="BarsServiceOptions"/> that will be used
        /// to construct the <see cref="Core.Services.IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        IBarsServiceBuilder Configure(Action<IBarsServiceInfo, BarsServiceOptions> barsServiceOptions);

        ///// <summary>
        ///// Adds new series to be used in the bars service.
        ///// </summary>
        ///// <param name="configureSeries">The delegate for configuring the <see cref="SeriesServiceInfo"/> and the <see cref="SeriesServiceOptions"/> 
        ///// that will be usedto construct the <see cref="Series.ISeries"/></param>
        ///// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        //IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo> configureSeries)
        //    where TInfo : IInputSeriesInfo, new();

        ///// <summary>
        ///// Adds new series to be used in the bars service.
        ///// </summary>
        ///// <param name="configureSeries">The delegate for configuring the <see cref="SeriesServiceInfo"/> and the <see cref="SeriesServiceOptions"/> 
        ///// that will be usedto construct the <see cref="Series.ISeries"/></param>
        ///// <returns>The same instance of the <see cref="IBarsServiceBuilder"/> for chaining.</returns>
        //IBarsServiceBuilder AddSeries_Period(Action<PeriodSeriesInfo> configureSeries);

    }
}
