using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Provides the options for any <see cref="INinjascriptService"/>.
    /// </summary>
    public class NinjascriptServiceOptions : ServiceOptions
    {

        /// <summary>
        /// Gets the calculate mode of the service.
        /// </summary>
        public Calculate CalculateMode { get; set; } = Calculate.OnBarClose;

        /// <summary>
        /// Gets the service calculation mode when another series is updated. 
        /// </summary>
        public MultiSeriesCalculateMode MultiSeriesCalculateMode { get; set; } = MultiSeriesCalculateMode.None;

        /// <summary>
        /// Indicates if the log service is enable.
        /// </summary>
        public bool IsLogEnable { get; set; } = true;
    }
}
