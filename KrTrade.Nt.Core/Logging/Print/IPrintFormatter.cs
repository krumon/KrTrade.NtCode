using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Logging
{
    /// <summary>
    /// Represents the print formatters properties and methods.
    /// </summary> 
    public interface IPrintFormatter : IFormatter
    {
        /// <summary>
        /// Indicates whether the log information will be shown.
        /// </summary>
        bool IsLogInfoVisible { get; set; }

        /// <summary>
        /// Indicates whether the log data series information will be shown.
        /// </summary>
        bool IsDataSeriesInfoVisible { get; set; }

        /// <summary>
        /// Indicates whether the log time information will be shown.
        /// </summary>
        bool IsTimeVisible { get; set; }

        /// <summary>
        /// Indicates whether the number of the will be shown in the log message.
        /// </summary>
        bool IsNumOfBarVisible { get; set; }

        /// <summary>
        /// Indicates the length of the strings. This property affects to the ninjascript state string, the data series string
        /// </summary>
        FormatLength StringFormatLength { get; set; }

    }
}
