using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IBarsManager"/> objects. 
    /// </summary>
    public interface IBarsManagerBuilder
    {
        /// <summary>
        /// Sets up the options for the <see cref="IBarsService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="BarsManagerOptions"/> that will be used
        /// to construct the <see cref="IBarsManager"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder ConfigureOptions(Action<BarsManagerOptions> configureDelegate);

        IBarsManagerBuilder AddPrintService(Action<PrintOptions> configureDelegate);
        IBarsManagerBuilder ConfigurePrimaryDataSeries(Action<IPrimaryBarsServiceBuilder> configureDelegate);
        IBarsManagerBuilder AddDataSeries(Action<IBarsServiceBuilder> configureDelegate);
        //IBarsServicesBuilder AddFilters();

        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsServices"/>. This can only be called once.
        /// </summary>
        /// <param name="ninjascript">The 'NinjaScript' used to configure <see cref="IBarsServices"/> instance.</param>
        /// <returns>An initialized <see cref="IBarsServices"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsManager Build(NinjaScriptBase ninjascript);


    }
}
