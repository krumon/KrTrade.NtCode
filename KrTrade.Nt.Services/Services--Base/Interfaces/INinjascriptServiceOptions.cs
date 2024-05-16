using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines the options of the ninjascript service.
    /// </summary>
    public interface INinjascriptServiceOptions : IServiceOptions
    {
        /// <summary>
        /// Gets the calculate mode of the service.
        /// </summary>
        Calculate CalculateMode { get; set; }

        /// <summary>
        /// Gets the service calculation mode when another series is updated. 
        /// </summary>
        MultiSeriesCalculateMode MultiSeriesCalculateMode { get; set; }
    }
}
