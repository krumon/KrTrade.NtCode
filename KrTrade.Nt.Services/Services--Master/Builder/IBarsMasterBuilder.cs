using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IBarsMaster"/> objects. 
    /// </summary>
    public interface IBarsMasterBuilder
    {
        /// <summary>
        /// Sets up the options for the <see cref="IBarsService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="BarsMasterOptions"/> that will be used
        /// to construct the <see cref="IBarsMaster"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsMasterBuilder"/> for chaining.</returns>
        IBarsMasterBuilder ConfigureOptions(Action<BarsMasterOptions> configureDelegate);

        IBarsMasterBuilder UsePrintService(Action<PrintOptions> configureDelegate);
        IBarsMasterBuilder ConfigureDataSeries(Action<BarsServiceOptions> configureDelegate);
        IBarsMasterBuilder AddDataSeries(Action<DataSeriesInfo,IBarsServiceBuilder> configureDelegate);
        //IBarsServicesBuilder AddFilters();

        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsServices"/>. This can only be called once.
        /// </summary>
        /// <param name="ninjascript">The 'NinjaScript' used to configure <see cref="IBarsServices"/> instance.</param>
        /// <returns>An initialized <see cref="IBarsServices"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsMaster Build(NinjaScriptBase ninjascript);


    }
}
