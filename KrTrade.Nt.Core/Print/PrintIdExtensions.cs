using KrTrade.Nt.Core.Core;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Core.Print
{
    /// <summary>
    /// Helper methods to converts <see cref="PrintId"/> to string.
    /// </summary>
    public static class PrintIdExtensions
    {
        /// <summary>
        /// Converts an object with a specific format to <see cref="PrintId"/> type string.
        /// </summary>
        /// <param name="printId">The type of the string to convert.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">The specific format of the <paramref name="value"/>. </param>
        /// <param name="formatType">The type of the generic format (Log, file, chart,...).</param>
        /// <param name="formatLength">The length of the generic format.</param>
        /// <returns>The value converts to string.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string ToString(this PrintId printId, object value, object format, FormatType formatType = FormatType.Default, FormatLength formatLength = FormatLength.Long)
        {
            if (value == null) return string.Empty;

            if (printId == PrintId.Time)
            {
                if (value is DateTime time)
                {
                    if (format is TimeFormat timeFormat)
                        return time.ToString(timeFormat, formatType, formatLength);
                    else
                        return time.ToString(TimeFormat.Minute, formatType, formatLength);
                }
                return string.Format("The '{0}' cannot be convert to DateTime string.",value.ToString());
            }
            else if (printId == PrintId.None)
                return string.Empty;
            else
                throw new NotImplementedException(printId.ToString());
        }
    }
}
