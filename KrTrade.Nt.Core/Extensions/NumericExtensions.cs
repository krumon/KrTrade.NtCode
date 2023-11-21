using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Extensions
{

    /// <summary>
    /// Extensions methods of <see cref="double"/> structure.
    /// </summary>
    public static class NumericExtensions
    {

        #region Double extensions

        /// <summary>
        /// Converts from <see cref="double"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to convert.</param>
        /// <param name="doubleFormat">The double format of the value to convert.</param>
        /// <returns>The string with the double format indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="DoubleFormat"/> is not implemented yet.</exception>
        public static string ToString(this double value, DoubleFormat doubleFormat)
        {
            switch (doubleFormat)
            {
                case DoubleFormat.Price:
                    return value.ToString(FormatLength.Long);
                case DoubleFormat.Volume:
                    return value.ToString(FormatLength.Short);
                default: throw new NotImplementedException();
            }
        }

        ///// <summary>
        ///// Converts from <see cref="double"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="double"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this double value, FormatType formatType)
        //{
        //    switch(formatType)
        //    {
        //        case FormatType.Default: 
        //        case FormatType.File: 
        //        case FormatType.Chart: 
        //            return value.ToString(FormatLength.Long);
        //        case FormatType.Log: 
        //            return value.ToLogString();
        //        default: 
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="double"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="double"/> value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this double value) => ToString(value, FormatLength.Long);

        ///// <summary>
        ///// Converts from <see cref="double"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="double"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this double value, DoubleFormat doubleFormat) => ToString(value, doubleFormat);

        /// <summary>
        /// Converts from <see cref="double"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to convert.</param>
        /// <param name="formatLength">The format length of returned string.</param>
        /// <returns>The string with the format length indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="FormatLength"/> is not implemented yet.</exception>
        public static string ToString(this double value, FormatLength formatLength)
        {
            switch (formatLength)
            {
                case FormatLength.Long:
                    return value.ToString("#,0.00");
                case FormatLength.Short:
                    return value.ToString("#,0.##");
                default: throw new NotImplementedException();
            }
        }

        #endregion

        #region Float extensions

        ///// <summary>
        ///// Converts from <see cref="float"/> value to string value with the format length indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="float"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The string with the double format indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="DoubleFormat"/> is not implemented yet.</exception>
        //public static string ToString(this float value, DoubleFormat doubleFormat)
        //{
        //    switch (doubleFormat)
        //    {
        //        case DoubleFormat.Price:
        //            return value.ToString(FormatLength.Long);
        //        case DoubleFormat.Volume:
        //            return value.ToString(FormatLength.Short);
        //        default: throw new NotImplementedException();
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="float"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="float"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this float value, FormatType formatType)
        //{
        //    switch (formatType)
        //    {
        //        case FormatType.Default:
        //        case FormatType.File:
        //        case FormatType.Chart:
        //            return value.ToString(FormatLength.Long);
        //        case FormatType.Log:
        //            return value.ToLogString();
        //        default:
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="float"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="float"/> value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this float value) => ToString(value, FormatLength.Long);

        ///// <summary>
        ///// Converts from <see cref="float"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="float"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this float value, DoubleFormat doubleFormat) => ToString(value, doubleFormat);

        /// <summary>
        /// Converts from <see cref="float"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to convert.</param>
        /// <param name="formatLength">The format length of returned string.</param>
        /// <returns>The string with the format length indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="FormatLength"/> is not implemented yet.</exception>
        public static string ToString(this float value, FormatLength formatLength)
        {
            switch (formatLength)
            {
                case FormatLength.Long:
                    return value.ToString("#,0.00");
                case FormatLength.Short:
                    return value.ToString("#,0.##");
                default: throw new NotImplementedException();
            }
        }

        #endregion decimal

        #region Decimal extensions

        ///// <summary>
        ///// Converts from <see cref="decimal"/> value to string value with the format length indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="decimal"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The string with the double format indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="DoubleFormat"/> is not implemented yet.</exception>
        //public static string ToString(this decimal value, DoubleFormat doubleFormat)
        //{
        //    switch (doubleFormat)
        //    {
        //        case DoubleFormat.Price:
        //            return value.ToString(FormatLength.Long);
        //        case DoubleFormat.Volume:
        //            return value.ToString(FormatLength.Short);
        //        default: throw new NotImplementedException();
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="decimal"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="decimal"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this decimal value, FormatType formatType)
        //{
        //    switch (formatType)
        //    {
        //        case FormatType.Default:
        //        case FormatType.File:
        //        case FormatType.Chart:
        //            return value.ToString(FormatLength.Long);
        //        case FormatType.Log:
        //            return value.ToLogString();
        //        default:
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="decimal"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="decimal"/> value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this decimal value) => ToString(value, FormatLength.Long);

        ///// <summary>
        ///// Converts from <see cref="decimal"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="decimal"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this decimal value, DoubleFormat doubleFormat) => ToString(value, doubleFormat);

        /// <summary>
        /// Converts from <see cref="decimal"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="decimal"/> value to convert.</param>
        /// <param name="formatLength">The format length of returned string.</param>
        /// <returns>The string with the format length indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="FormatLength"/> is not implemented yet.</exception>
        public static string ToString(this decimal value, FormatLength formatLength)
        {
            switch (formatLength)
            {
                case FormatLength.Long:
                    return value.ToString("#,0.00");
                case FormatLength.Short:
                    return value.ToString("#,0.##");
                default: throw new NotImplementedException();
            }
        }

        #endregion

        #region Int extensions

        ///// <summary>
        ///// Converts from <see cref="int"/> value to string value with the format length indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="int"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The string with the double format indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="DoubleFormat"/> is not implemented yet.</exception>
        //public static string ToString(this int value, DoubleFormat doubleFormat)
        //{
        //    switch (doubleFormat)
        //    {
        //        case DoubleFormat.Price:
        //            return value.ToString(FormatLength.Long);
        //        case DoubleFormat.Volume:
        //            return value.ToString(FormatLength.Short);
        //        default: throw new NotImplementedException();
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="int"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="int"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this int value, FormatType formatType)
        //{
        //    switch (formatType)
        //    {
        //        case FormatType.Default:
        //        case FormatType.File:
        //        case FormatType.Chart:
        //            return value.ToString(FormatLength.Short);
        //        case FormatType.Log:
        //            return value.ToLogString();
        //        default:
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="int"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="int"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this int value, FormatType formatType, DoubleFormat doubleFormat)
        //{
        //    switch (formatType)
        //    {
        //        case FormatType.Default:
        //        case FormatType.File:
        //        case FormatType.Chart:
        //            return value.ToString(doubleFormat);
        //        case FormatType.Log:
        //            return value.ToLogString();
        //        default:
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="int"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="int"/> value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this int value) => ToString(value, FormatLength.Short);

        ///// <summary>
        ///// Converts from <see cref="int"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="int"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this int value, DoubleFormat doubleFormat) => ToString(value, doubleFormat);

        /// <summary>
        /// Converts from <see cref="int"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to convert.</param>
        /// <param name="formatLength">The format length of returned string.</param>
        /// <returns>The string with the format length indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="FormatLength"/> is not implemented yet.</exception>
        public static string ToString(this int value, FormatLength formatLength)
        {
            switch (formatLength)
            {
                case FormatLength.Long:
                    return value.ToString("#,0.00");
                case FormatLength.Short:
                    return value.ToString("#,0.##");
                default: throw new NotImplementedException();
            }
        }

        #endregion

        #region Long extensions

        ///// <summary>
        ///// Converts from <see cref="long"/> value to string value with the format length indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="long"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The string with the double format indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="DoubleFormat"/> is not implemented yet.</exception>
        //public static string ToString(this long value, DoubleFormat doubleFormat)
        //{
        //    switch (doubleFormat)
        //    {
        //        case DoubleFormat.Price:
        //            return value.ToString(FormatLength.Long);
        //        case DoubleFormat.Volume:
        //            return value.ToString(FormatLength.Short);
        //        default: throw new NotImplementedException();
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="long"/> value to string value with the format type indicated.
        ///// </summary>
        ///// <param name="value">The <see cref="long"/> value to convert.</param>
        ///// <param name="formatType">The format type of the value to convert.</param>
        ///// <returns>The string with the format type indicated.</returns>
        ///// <exception cref="NotImplementedException">The type of the <see cref="FormatType"/> is not implemented yet.</exception>
        //public static string ToString(this long value, FormatType formatType)
        //{
        //    switch (formatType)
        //    {
        //        case FormatType.Default:
        //        case FormatType.File:
        //        case FormatType.Chart:
        //            return value.ToString(FormatLength.Short);
        //        case FormatType.Log:
        //            return value.ToLogString();
        //        default:
        //            throw new NotImplementedException(string.Format("The {0} is not implemented yet.", formatType.ToString()));
        //    }
        //}

        ///// <summary>
        ///// Converts from <see cref="long"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="long"/> value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this long value) => ToString(value, FormatLength.Short);

        ///// <summary>
        ///// Converts from <see cref="long"/> value to log string value.
        ///// </summary>
        ///// <param name="value">The <see cref="long"/> value to convert.</param>
        ///// <param name="doubleFormat">The double format of the value to convert.</param>
        ///// <returns>The log string with long format length.</returns>
        //public static string ToLogString(this long value, DoubleFormat doubleFormat) => ToString(value, doubleFormat);

        /// <summary>
        /// Converts from <see cref="long"/> value to string value with the format length indicated.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value to convert.</param>
        /// <param name="formatLength">The format length of returned string.</param>
        /// <returns>The string with the format length indicated.</returns>
        /// <exception cref="NotImplementedException">The type of the <see cref="FormatLength"/> is not implemented yet.</exception>
        public static string ToString(this long value, FormatLength formatLength)
        {
            switch (formatLength)
            {
                case FormatLength.Long:
                    return value.ToString("#,0.00");
                case FormatLength.Short:
                    return value.ToString("#,0.##");
                default: throw new NotImplementedException();
            }
        }
        #endregion

    }
}
