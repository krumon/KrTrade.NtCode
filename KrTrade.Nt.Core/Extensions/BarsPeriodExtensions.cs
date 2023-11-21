using KrTrade.Nt.Core.Data;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Core.Extensions
{

    /// <summary>
    /// Extensions methods of <see cref="BarsPeriod"/> object.
    /// </summary>
    public static class BarsPeriodExtensions
    {

        /// <summary> 
        /// Converts from <see cref="BarsPeriod"/> object to long string.
        /// </summary>
        /// <param name="barsPeriod"><see cref="BarsPeriod"/> object to convert.</param>
        /// <returns>The <see cref="BarsPeriod"/> long string.</returns>
        public static string ToString(this BarsPeriod barsPeriod, FormatType
            formatType)
        {
            if (barsPeriod == null) throw new ArgumentNullException(nameof(barsPeriod));
            switch (formatType)
            {
                case FormatType.Default:
                    return barsPeriod.ToLongString();
                case FormatType.Log:
                case FormatType.File:
                case FormatType.Chart:
                    return barsPeriod.ToShortString();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary> 
        /// Converts from <see cref="BarsPeriod"/> object to short string.
        /// </summary>
        /// <param name="barsPeriod"><see cref="BarsPeriod"/> object to convert.</param>
        /// <returns>The <see cref="BarsPeriod"/> short string.</returns>
        public static string ToShortString(this BarsPeriod barsPeriod)
        {
            string periodType;
            switch (barsPeriod.BarsPeriodType)
            {
                case BarsPeriodType.Day: periodType = "d"; break;
                case BarsPeriodType.Week: periodType = "w"; break;
                case BarsPeriodType.Month: periodType = "M"; break;
                case BarsPeriodType.Year: periodType = "y"; break;
                case BarsPeriodType.Minute: periodType = "m"; break;
                case BarsPeriodType.Second: periodType = "s"; break;
                case BarsPeriodType.Tick: periodType = "t"; break;
                default: return string.Empty;
            }
            //return "(" + periodType + barsPeriod.Value + ")";
            return periodType + barsPeriod.Value;
        }

        /// <summary> 
        /// Converts from <see cref="BarsPeriod"/> object to long string.
        /// </summary>
        /// <param name="barsPeriod"><see cref="BarsPeriod"/> object to convert.</param>
        /// <returns>The <see cref="BarsPeriod"/> long string.</returns>
        public static string ToLongString(this BarsPeriod barsPeriod)
        {
            return (barsPeriod.ToString()).Trim();
        }

        /// <summary> 
        /// Converts from <see cref="BarsPeriod"/> object to long string.
        /// </summary>
        /// <param name="barsPeriod"><see cref="BarsPeriod"/> object to convert.</param>
        /// <returns>The <see cref="BarsPeriod"/> long string.</returns>
        private static string ToString(this BarsPeriod barsPeriod, FormatLength formatLength)
        {
            if (barsPeriod == null) throw new ArgumentNullException(nameof(barsPeriod));
            switch (formatLength)
            {
                case FormatLength.Long:
                    return barsPeriod.ToLongString();
                case FormatLength.Short:
                    return barsPeriod.ToShortString();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Converts from <see cref="BarsPeriod"/> object to <see cref="TimeFormat"/>.
        /// </summary>
        /// <param name="barsPeriod"><see cref="BarsPeriod"/> object to convert.</param>
        /// <returns>The <see cref="TimeFormat"/> object.</returns>
        public static TimeFormat ToTimeFormat(this BarsPeriod barsPeriod)
        {
            switch (barsPeriod.BarsPeriodType)
            {
                case BarsPeriodType.Day:
                case BarsPeriodType.Week:
                case BarsPeriodType.Month:
                case BarsPeriodType.Year:
                    return TimeFormat.Day;
                case BarsPeriodType.Minute:
                    return TimeFormat.Minute;
                case BarsPeriodType.Second:
                    return TimeFormat.Second;
                case BarsPeriodType.Tick:
                    return TimeFormat.Millisecond;
                default:
                    return TimeFormat.Minute;
            }
        }

        /// <summary> 
        /// Converts from <see cref="BarsPeriod"/> object to time string.
        /// </summary>
        /// <param name="barsPeriodType"><see cref="BarsPeriodType"/> object to decide what time format to return.</param>
        /// <returns>The <see cref="BarsPeriod"/> short string.</returns>
        public static TimeFormat ToTimeFormat(this BarsPeriodType barsPeriodType)
        {
            switch (barsPeriodType)
            {
                case BarsPeriodType.Day:
                case BarsPeriodType.Week:
                case BarsPeriodType.Month:
                case BarsPeriodType.Year:
                    return TimeFormat.Day;
                case BarsPeriodType.Minute:
                    return TimeFormat.Minute;
                case BarsPeriodType.Second:
                    return TimeFormat.Second;
                case BarsPeriodType.Tick:
                    return TimeFormat.Millisecond;
                default:
                    return TimeFormat.Minute;
            }
        }
    }
}
