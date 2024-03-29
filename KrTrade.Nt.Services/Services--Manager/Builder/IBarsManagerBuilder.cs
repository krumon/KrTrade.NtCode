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
        /// <param name="configureBarsManagerOptions">The delegate for configuring the <see cref="BarsManagerOptions"/> that will be used
        /// to construct the <see cref="IBarsManager"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder ConfigureOptions(Action<BarsManagerOptions> configureBarsManagerOptions);

        /// <summary>
        /// Sets up the options for the <see cref="IPrintService"/> object. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configurePrintServiceOptions">The delegate for configuring the <see cref="PrintOptions"/> that will be used
        /// to construct the <see cref="IPrintService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder AddPrintService(Action<PrintOptions> configurePrintServiceOptions);

        /// <summary>
        /// Sets up the options and services for the primary <see cref="IBarsService"/> object. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configurePrimaryBarsServiceBuilder">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        /// to construct the primary <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder ConfigurePrimaryDataSeries(Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder);

        /// <summary>
        /// Sets up the options abd services for the primary <see cref="IBarsService"/> object. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="name">The pseudoname for called the primary <see cref="IBarsService"/> from <see cref="IBarsManager"/>.</param>
        /// <param name="configurePrimaryBarsServiceBuilder">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        /// to construct the primary <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder ConfigurePrimaryDataSeries(string name, Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder);

        /// <summary>
        /// Sets up the options and services for the <see cref="IBarsService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configureDataSeriesOptions">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        /// to construct the <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder AddDataSeries(Action<DataSeriesOptions> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder);

        /// <summary>
        /// Sets up the options and services for the <see cref="IBarsService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="name">The pseudoname for called the <see cref="IBarsService"/> from <see cref="IBarsManager"/>.</param>
        /// <param name="configureDataSeriesOptions">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        /// to construct the <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder AddDataSeries(string name, Action<DataSeriesOptions> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder);

        //IBarsServicesBuilder AddFilters();
        
        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        /// </summary>
        /// <param name="ninjascript">The 'NinjaScript' used to configure <see cref="IBarsService"/> instance.</param>
        /// <returns>An initialized <see cref="IBarsService"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsManager Build(NinjaScriptBase ninjascript);


    }
}
