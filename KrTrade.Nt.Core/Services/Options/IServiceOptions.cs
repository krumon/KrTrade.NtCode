using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines the options of the service.
    /// </summary>
    public interface IServiceOptions : IOptions
    {
        /// <summary>
        /// Indicates if the object logger is enable.
        /// </summary>
        bool IsLogEnable { get; set; }

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
