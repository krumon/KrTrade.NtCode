using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Provides the options for any service.
    /// </summary>
    public class ServiceOptions : BaseOptions, IServiceOptions
    {
        /// <summary>
        /// Indicates if the log service is enable.
        /// </summary>
        public bool IsLogEnable { get; set; } = true;

        /// <summary>
        /// Gets the calculate mode of the service.
        /// </summary>
        public Calculate CalculateMode { get; set; } = Calculate.OnBarClose;

        /// <summary>
        /// Gets the service calculation mode when another series is updated. 
        /// </summary>
        public MultiSeriesCalculateMode MultiSeriesCalculateMode { get; set; } = MultiSeriesCalculateMode.None;

    }
}
