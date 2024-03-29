using KrTrade.Nt.Core.DataSeries;
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

    }
}
