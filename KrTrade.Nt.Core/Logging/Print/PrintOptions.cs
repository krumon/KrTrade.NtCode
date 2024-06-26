using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Logging
{
    public class PrintOptions : BaseLoggerOptions<PrintFormatter>, IPrintOptions
    {

        /// <summary>
        /// Indicates whether the log information will be shown.
        /// </summary>
        public bool IsLogInfoVisible { get => Formatter.IsLogInfoVisible; set { Formatter.IsLogInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log data series information will be shown.
        /// </summary>
        public bool IsDataSeriesInfoVisible { get => Formatter.IsDataSeriesInfoVisible; set { Formatter.IsDataSeriesInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log time information will be shown.
        /// </summary>
        public bool IsTimeVisible { get => Formatter.IsTimeVisible; set { Formatter.IsTimeVisible = value; } }

        /// <summary>
        /// Indicates whether the number of the will be shown in the log message.
        /// </summary>
        public bool IsNumOfBarVisible { get => Formatter.IsNumOfBarVisible; set { Formatter.IsNumOfBarVisible = value; } }

        /// <summary>
        /// Indicates the length of the strings. This property affects to the ninjascript state string and data series string
        /// </summary>
        public FormatLength FormatLength { get => Formatter.StringFormatLength; set { Formatter.StringFormatLength = value; } }

    }
}
