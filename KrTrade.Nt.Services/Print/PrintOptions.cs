using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Logging;

namespace KrTrade.Nt.Services
{
    public class PrintOptions : BaseLogOptions<PrintFormatter>
    {

        /// <summary>
        /// The minimum ninjascript level to log.
        /// </summary>
        internal NinjascriptLogLevel NinjascriptLogLevel { get; set; } = NinjascriptLogLevel.Configuration;

        /// <summary>
        /// Indicates whether the log information will be shown.
        /// </summary>
        internal bool IsLogInfoVisible { get => Formatter.IsLogInfoVisible; set { Formatter.IsLogInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log data series information will be shown.
        /// </summary>
        internal bool IsDataSeriesInfoVisible { get => Formatter.IsDataSeriesInfoVisible; set { Formatter.IsDataSeriesInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log time information will be shown.
        /// </summary>
        internal bool IsTimeVisible { get => Formatter.IsTimeVisible; set { Formatter.IsTimeVisible = value; } }

        /// <summary>
        /// Indicates whether the number of the will be shown in the log message.
        /// </summary>
        internal bool IsNumOfBarVisible { get => Formatter.IsNumOfBarVisible; set { Formatter.IsNumOfBarVisible = value; } }

        /// <summary>
        /// Indicates the length of the strings. This property affects to the ninjascript state string, the data series string
        /// </summary>
        internal FormatLength StringFormatLength { get => Formatter.StringFormatLength; set { Formatter.StringFormatLength = value; } }


    }
}
