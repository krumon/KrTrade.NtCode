using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Options
{
    /// <summary>
    /// Provides the options for any service.
    /// </summary>
    public class ServiceOptions : Options, IServiceOptions
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
