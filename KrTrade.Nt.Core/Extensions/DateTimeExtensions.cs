using KrTrade.Nt.Core.Data;
using System;
using System.Globalization;

namespace KrTrade.Nt.Core.Extensions
{

    /// <summary>
    /// Helper methods of <see cref="DateTime"/> structure.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static string ToString(this DateTime time, TimeFormat timeFormat = TimeFormat.Minute, FormatType formatType = FormatType.Default, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            switch(formatType)
            {
                case FormatType.Default:
                    return time.ToDefaultString(timeFormat, formatLength);
                case FormatType.Log:
                    return time.ToLogString(timeFormat, formatLength);
                default: throw new NotImplementedException(formatType.ToString());
            }
        }

        public static string ToDefaultString(this DateTime time, TimeFormat timeFormat, FormatLength formatLength)
        {
            if (time == null) throw new ArgumentNullException("time");
            switch (timeFormat)
            {
                case TimeFormat.Day:
                    return time.ToDayDefaultString(formatLength);
                case TimeFormat.Hour:
                    return time.ToHourDefaultString(formatLength);
                case TimeFormat.Minute:
                    return time.ToMinuteDefaultString(formatLength);
                case TimeFormat.Second:
                    return time.ToSecondDefaultString(formatLength);
                case TimeFormat.Millisecond:
                    return time.ToMillisecondDefaultString(formatLength);
                default: throw new NotImplementedException();
            }
        }
        public static string ToLogString(this DateTime time, TimeFormat timeFormat, FormatLength formatLength)
        {
            if (time == null) throw new ArgumentNullException("time");
            switch (timeFormat)
            {
                case TimeFormat.Day:
                    return time.ToDayLogString(formatLength);
                case TimeFormat.Hour:
                    return time.ToHourLogString(formatLength);
                case TimeFormat.Minute:
                    return time.ToMinuteLogString(formatLength);
                case TimeFormat.Second:
                    return time.ToSecondLogString(formatLength);
                case TimeFormat.Millisecond:
                    return time.ToMillisecondLogString(formatLength);
                default: throw new NotImplementedException();
            }
        }

        private static string ToDayDefaultString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "MMM'-'yy',' dd'-'ddd";
            else if (formatLength == FormatLength.Short)
                format = "MMM'-'yy'('dd')'";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToHourDefaultString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MM'-'yy'('ddd')' HH'h'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MM'-'yy' HH";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToMinuteDefaultString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToSecondDefaultString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm:'ss's'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm':'ss";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToMillisecondDefaultString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm:'ss'.'fff's'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm':'ss'.'fff";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }

        private static string ToDayLogString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "MMM'-'yy',' dd'-'ddd";
            else if (formatLength == FormatLength.Short)
                format = "MMM'-'yy'('dd')'";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToHourLogString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MM'-'yy'('ddd')' HH'h'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MM'-'yy' HH";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToMinuteLogString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToSecondLogString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm:'ss's'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm':'ss";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }
        private static string ToMillisecondLogString(this DateTime time, FormatLength formatLength = FormatLength.Long)
        {
            if (time == null) throw new ArgumentNullException("time");
            string format = string.Empty;
            if (formatLength == FormatLength.Long)
                format = "dd'-'MMM'('ddd')' HH'h:'mm'm:'ss's 'fff'ms'";
            else if (formatLength == FormatLength.Short)
                format = "dd'-'MMM HH':'mm':'ss'.'fff";
            return time.ToString(format, CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static int ToTime(this DateTime value)
        {
            int intValue = value.Hour * 10000 + value.Minute * 100 + value.Second;
            int.TryParse($"{value.Hour.ToString()}{value.Minute.ToString()}{value.Second.ToString()}", out intValue);
            throw new Exception("the method is pending to be developed.");
        }
        public static int ToDate(this DateTime value)
        {
            throw new Exception("the method is pending to be developed.");
        }
        public static int ToMilliseconds(this DateTime value)
        {
            throw new Exception("the method is pending to be developed.");
        }
        public static TimeSpan ToTime(this int value)
        {
            throw new Exception("the method is pending to be developed.");
        }
        public static DateTime ToDate(this int value)
        {
            throw new Exception("the method is pending to be developed.");
        }
        public static string ToString(this DateTime value, string format)
        {
            if (value == null)
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return string.Empty;

            if (format.ToUpper() == "DAY")
                return $"{value.ToDate()}";

            if (format.ToUpper() == "MINUTE")
                return $"{value.ToDate()} {value.ToTime()}";

            if (format.ToUpper() == "TICK")
                return $"{value.ToDate()} {value.ToTime()}.{value.ToMilliseconds()}";

            return string.Empty;
        }
        public static DateTime ToDateTime(this string value)
        {
            throw new Exception("the method is pending to be developed.");
        }

    }
}
