using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Options;

namespace KrTrade.Nt.Core.Logging
{
    /// <summary>
    /// Represents properties and methods for all logging services.
    /// </summary>
    public interface ILoggerOptions<TFormatter> : IServiceOptions
        where TFormatter : IFormatter, new()
    {
        /// <summary>
        /// Represents the minimum log level. 0:Trace, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Critical, 6:None
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// The minimum <see cref="NinjascriptLogLevel"/> to be logged. 0:Historical, 1:Configuration, 2:Realtime
        /// Historical level logs in all states.
        /// Configuration level not logs in Historical state.
        /// Realtime logs only in Realtime state.
        /// </summary>
        NinjascriptLogLevel NinjascriptLogLevel { get; set; }

        /// <summary>
        /// The minimum <see cref="BarsLogLevel"/> to be logged. 0:Tick, 1:PriceChanged, 2:BarClosed, 3:None
        /// </summary>
        BarsLogLevel BarsLogLevel { get; set; }

        /// <summary>
        /// gets the format to logging in any environment.
        /// </summary>
        TFormatter Formatter { get; }

    }
}
