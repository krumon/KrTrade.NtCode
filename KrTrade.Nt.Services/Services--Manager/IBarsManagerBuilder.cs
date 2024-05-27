using NinjaTrader.Data;
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
        IBarsManagerBuilder ConfigurePrimaryBars(Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder);

        ///// <summary>
        ///// Sets up the options abd services for the primary <see cref="IBarsService"/> object. This can be called multiple times and
        ///// the results will be additive.
        ///// </summary>
        ///// <param name="name">The pseudoname for called the primary <see cref="IBarsService"/> from <see cref="IBarsManager"/>.</param>
        ///// <param name="configurePrimaryBarsServiceBuilder">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        ///// to construct the primary <see cref="IBarsService"/>.</param>
        ///// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        //IBarsManagerBuilder ConfigurePrimaryBars(string name, Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder);

        /// <summary>
        /// Sets up the options and services for the <see cref="IBarsService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configureBarsServiceBuilder">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        /// to construct the <see cref="IBarsService"/>.</param>
        /// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        IBarsManagerBuilder AddDataSeries(Action<IBarsServiceBuilder> configureBarsServiceBuilder);

        ///// <summary>
        ///// Sets up the options and services for the <see cref="IBarsService"/> objects. This can be called multiple times and
        ///// the results will be additive.
        ///// </summary>
        ///// <param name="name">The pseudoname for called the <see cref="IBarsService"/> from <see cref="IBarsManager"/>.</param>
        ///// <param name="configureDataSeriesInfo">The delegate for configuring the <see cref="IBarsServiceBuilder"/> that will be used
        ///// to construct the <see cref="IBarsService"/>.</param>
        ///// <returns>The same instance of the <see cref="IBarsManagerBuilder"/> for chaining.</returns>
        //IBarsManagerBuilder AddBars(string name, Action<BarsServiceInfo> configureDataSeriesInfo, Action<IBarsServiceBuilder> configureBarsServiceBuilder);

        //IBarsServicesBuilder AddFilters();
        
        /// <summary>
        /// Run the given actions to initialize the <see cref="IBarsService"/>. This can only be called once.
        /// </summary>
        /// <param name="ninjascript">The 'NinjaScript' used to configure <see cref="IBarsService"/> instance.</param>
        /// <param name="addDataSeriesMethod">The NinjaScript.AddDataSeries method, used to configure <see cref="BarsManager"/> 
        /// when we ar going to use plot reprentation in the chart panel.</param>
        /// <returns>An initialized <see cref="IBarsService"/></returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        IBarsManager Build(NinjaScriptBase ninjascript, Action<string,BarsPeriod,string> addDataSeriesMethod);


    }
}
