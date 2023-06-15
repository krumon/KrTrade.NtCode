using System;

namespace KrTrade.NtCode.Extensions
{

    /// <summary>
    /// Helper methods of <see cref="DateTime"/> structure.
    /// </summary>
    public static class DateTimeExtensions
    {

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
