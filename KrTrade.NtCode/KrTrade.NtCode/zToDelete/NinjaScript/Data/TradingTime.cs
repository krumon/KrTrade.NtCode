using Kr.Core.Helpers;
using Nt.Core.Data;
using Nt.Core.Events;
using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents a trading time information.
    /// </summary>
    public class TradingTime : BaseElement,
        IComparable,
        IComparable<TradingTime>,
        IEquatable<TradingTime>
    {
        #region Private members

        /// <summary>
        /// The trading time type.
        /// </summary>
        private TradingTimeType tradingTimeType;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading time type.
        /// </summary>
        public TradingTimeType TradingTimeType 
        {
            private set 
            { 
                tradingTimeType = value;

                if (tradingTimeType == TradingTimeType.Custom)
                {
                    Key = ToDefaultCode();
                    if (string.IsNullOrEmpty(Description))
                        Description = $"Custom TradingSessionInfo Time {LocalTime.TotalHours}.";
                }
                else
                {
                    Key = tradingTimeType.ToCode();
                    Description = tradingTimeType.ToDescription();
                }
            }
            get => tradingTimeType;
        }

        /// <summary>
        /// Gets the unique key of the <see cref="TradingTime"/>.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the description of the <see cref="TradingTime"/>.
        /// </summary>
        public string Description {get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="System.TimeZoneInfo"/> of the <see cref="TradingTime"/>.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the time of the <see cref="TradingTime"/>.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Converts the <see cref="Time"/> property to UTC time.
        /// </summary>
        public TimeSpan UtcTime
        {
            get
            {
                if (TimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(TimeZoneInfo));

                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan utcTime = Time - TimeZoneInfo.BaseUtcOffset;

                return utcTime;
            }
        }

        /// <summary>
        /// Converts the Time property to Local time.
        /// </summary>
        public TimeSpan LocalTime
        {
            get
            {
                if (TimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(TimeZoneInfo));

                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan localTime = UtcTime + TimeZoneInfo.Local.BaseUtcOffset;

                return localTime;
            }
        }

        #endregion

        #region Constructors



        /// <summary>
        /// Create a default instance of <see cref="TradingTime"/>.
        /// </summary>
        private TradingTime()
        {
        }

        /// <summary>
        /// Create <see cref="TradingTime"/> instance with a specific Time and TimeZoneInfo.
        /// </summary>
        /// <param name="time">The specific <see cref="TimeSpan"/> of the <see cref="TradingTime"/>.</param>
        /// <param name="timeZoneInfo">the specific <see cref="TimeZoneInfo"/> of the <see cref="TradingTime"/>.</param>
        public TradingTime(TimeSpan time, TimeZoneInfo timeZoneInfo)
        {
            TimeZoneInfo = timeZoneInfo;
            Time = time;
            // TODO: Esto tengo que modificarlo. No es un tipo "custom".
            TradingTimeType = TradingTimeType.Custom;
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a default instance of <see cref="Core.TradingTime"/> by specific <see cref="Nt.Core.TradingTimeKind"/>.
        /// </summary>
        /// <param name="tradingTimeType">The specific session time to create the instance.</param>
        /// <param name="e">The snew session args.</param>
        /// <param name="instrumentKey">The unique code of the financial instrument session.</param>
        /// <param name="timeDisplacement">The offset of the <see cref="DateTime"/> in minutes.</param>
        /// <returns>The session time instance.</returns>
        public static TradingTime CreateSessionTimeByType(TradingTimeType tradingTimeType, InstrumentCode instrumentKey, int timeDisplacement = 0)
        {
            return new TradingTime 
            {
                TimeZoneInfo = tradingTimeType.ToTimeZoneInfo(instrumentKey),
                Time = tradingTimeType.ToTime(instrumentKey, timeDisplacement),
                TradingTimeType = tradingTimeType
            };
        }

        /// <summary>
        /// Create a default instance of <see cref="Core.TradingTime"/> by specific <see cref="Nt.Core.TradingTimeKind"/>.
        /// </summary>
        /// <param name="tradingTimeType">The specific session time to create the instance.</param>
        /// <param name="e">The snew session args.</param>
        /// <param name="instrumentKey">The unique code of the financial instrument session.</param>
        /// <param name="timeDisplacement">The offset of the <see cref="DateTime"/> in minutes.</param>
        /// <returns>The session time instance.</returns>
        public static TradingTime CreateSessionTimeByType(TradingTimeType tradingTimeType, SessionUpdateArgs e, InstrumentCode instrumentKey, int timeDisplacement = 0)
        {
            return new TradingTime 
            {
                TimeZoneInfo = tradingTimeType.ToTimeZoneInfo(instrumentKey),
                Time = tradingTimeType.ToTime(instrumentKey, timeDisplacement),
                TradingTimeType = tradingTimeType
            };
        }

        /// <summary>
        /// Create a cutom instance of <see cref="Core.TradingTime"/> with specific <see cref="TimeSpan"/> and <see cref="System.TimeZoneInfo"/>.
        /// </summary>
        /// <param name="time">The specific time.</param>
        /// <param name="timeZoneInfo">The specific time zone info.</param>
        /// <param name="code">The code of the custom session.</param>
        /// <param name="description">The description of the custom session time.</param>
        /// <returns>A new instance of the <see cref="Core.TradingTime"/> with the specific parameters.</returns>
        public static TradingTime CreateCustomSessionTime(TimeSpan time, TimeZoneInfo timeZoneInfo = null, string description = "")
        {
            return new TradingTime
            {
                Time = time,
                TimeZoneInfo = timeZoneInfo,
                Description = description,
                TradingTimeType = TradingTimeType.Custom,
            };
        }

        /// <summary>
        /// Create a cutom instance of <see cref="Core.TradingTime"/> with specific <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="hour">The session <see cref="TimeSpan"/> hour.</param>
        /// <param name="minute">The session <see cref="TimeSpan"/> minute.</param>
        /// <param name="seconds">The session <see cref="TimeSpan"/> seconds.</param>
        /// <param name="timeZoneInfo">The specific time zone info.</param>
        /// <param name="code">The custom session code.</param>
        /// <param name="description">The custom session time description.</param>
        /// <returns></returns>
        public static TradingTime CreateCustomSessionTime(int hour, int minute, int seconds, TimeZoneInfo timeZoneInfo = null, string description = "")
        {
            return new TradingTime
            {
                Time = new TimeSpan(hour, minute, seconds),
                TimeZoneInfo = timeZoneInfo,
                Description = description,
                TradingTimeType = TradingTimeType.Custom,
            };
        }

        #endregion

        #region Handler methods

        public virtual void OnBarUpdate(DateTime userConfigureTime)
        {

        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the session <see cref="Time"/> for the time passed as parameter.
        /// </summary>
        /// <param name="timeZoneInfo">The current date to create the date time structure.</param>
        /// <returns></returns>
        public TimeSpan GetTime(TimeZoneInfo timeZoneInfo)
        {
            if (timeZoneInfo == null)
                throw new ArgumentNullException(nameof(timeZoneInfo));

            return UtcTime + timeZoneInfo.BaseUtcOffset;

        }

        /// <summary>
        /// Gets the session <see cref="DateTime"/> for the time passed as parameter.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime time)
        {
            TimeZoneInfo sourceTimeZoneInfo = 
                time.Kind == DateTimeKind.Local ? TimeZoneInfo.Local : 
                time.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : null;

            if (sourceTimeZoneInfo != null)
                // Returns the TradingTimeInfo TimeSpan for the date passed as parameter.
                return GetTime(time,sourceTimeZoneInfo);

            throw new Exception("The kind of the " + nameof(time) + " must be Local or Utc");
        }

        /// <summary>
        /// Gets the session <see cref="DateTime"/> for the <see cref="DateTime"/> and <see cref="TimeZoneInfo"/> passed as parameters.
        /// </summary>
        /// <param name="sourceTime">The specific time to convert in session <see cref="DateTime"/>.</param>
        /// <param name="sourceTimeZoneInfo">The source TimeZoneInfo.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo)
        {

            // Converts the time to the TradingTimeInfo.TimeZoneInfo
            DateTime date = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = ToSessionTime(date);
            // Converts the time to the sourceTimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, sourceTimeZoneInfo);

        }

        /// <summary>
        /// Gets the session <see cref="DateTime"/> respect destination <see cref="TimeZoneInfo"/> passed as parameter.
        /// </summary>
        /// <param name="sourceTime">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The source <see cref="TimeZoneInfo"/>.</param>
        /// <param name="destinationTimeZoneInfo">The destination <see cref="TimeZoneInfo"/>.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo, TimeZoneInfo destinationTimeZoneInfo)
        {

            // Converts the time to the TradingTimeInfo.TimeZoneInfo
            DateTime date = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = ToSessionTime(date);
            // Converts the time to the destination TimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, destinationTimeZoneInfo);

        }

        /// <summary>
        /// Indicates if <paramref name="st"/> exists in the <see cref="TradingTime"/> enum.
        /// </summary>
        /// <param name="tradingTimeType"><see cref="TradingTime"/> to check exists.</param>
        /// <returns>True if the session time object exists in the trading time type.</returns>
        public static bool Exist(TradingTimeType tradingTimeType)
        {
            bool exist = false;

            EnumHelpers.ForEach<TradingTimeType>((t) =>
            {
                if (!exist)
                {
                    if (t == tradingTimeType)
                        exist = true;
                }
            });

            return exist;
        }

        /// <summary>
        /// Returns the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// The default string that represents the <see cref="TradingTime"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString(TimeZoneInfo.Local,true);

        /// <summary>
        /// The string that represents the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="timeZoneInfo">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToString(TimeZoneInfo timeZoneInfo, bool showTimeZoneInfoName = true)
        {
            if (timeZoneInfo == null)
                throw new ArgumentNullException(nameof(timeZoneInfo));

            TimeSpan time = GetTime(timeZoneInfo);
            string s = $"{tradingTimeType.ToName()} - {time:%h\\:mm}h";

            if (showTimeZoneInfoName)
                s += string.Format($" {GetTimeZoneInfoName(timeZoneInfo)}");

            return s;

        }

        /// <summary>
        /// The default short string that represents the <see cref="TradingTime"/>.
        /// </summary>
        /// <returns></returns>
        public string ToShortString() => ToShortString(TimeZoneInfo.Local, true);

        /// <summary>
        /// The short string that represents the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToShortString(TimeZoneInfo timeZoneInfo, bool showTimeZoneInfoName = true)
        {
            if (timeZoneInfo == null)
                throw new ArgumentNullException(nameof(timeZoneInfo));

            TimeSpan time = GetTime(timeZoneInfo);
            string s = $"{time:%h\\:mm}h";

            if (showTimeZoneInfoName)
                s += string.Format($" {GetTimeZoneInfoName(timeZoneInfo)}");

            return s;
        }

        /// <summary>
        /// Gets the long string that represents the <see cref="TradingTime"/>. 
        /// </summary>
        /// <returns>The short string that represents the <see cref="TradingTime"/> in the specific time format,
        /// "L" for local time, "U" for UTC time, otherwise returns the specific time of the <see cref="TradingTime"/>.</returns>
        public string ToLongString() => ToLongString(TimeZoneInfo.Local, true);

        /// <summary>
        /// Gets the long string that represents the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to represents in the string. The time can be Utc, Local or Unspecific.</param>
        /// <returns>The long string that represents the <see cref="TradingTime"/> in the specific time format,
        /// "L" for local time, "U" for UTC time, otherwise returns the specific time of the <see cref="TradingTime"/></returns>
        public string ToLongString(TimeZoneInfo timeZoneInfo, bool showTimeZoneInfoName = true)
        {
            if (timeZoneInfo == null)
                throw new ArgumentNullException(nameof(timeZoneInfo));

            TimeSpan time = GetTime(timeZoneInfo);
            string s = $"{tradingTimeType.ToDescription()} - {time:%h\\:mm}h";

            if (showTimeZoneInfoName)
                s += string.Format($" {GetTimeZoneInfoName(timeZoneInfo)}");

            return s;
        }

        #endregion

        #region Equals methods

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is TradingTime tt)
                return Time == tt.Time && UtcTime == tt.UtcTime && Key == tt.Key;

            return false;
        }

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="TradingTime"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="TradingTime"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingTime"/>object passed as parameter cannot be null.</exception>
        public bool Equals(TradingTime tt)
        {

            if (tt is null)
                return false;

            return this.Time == tt.Time && this.UtcTime == tt.UtcTime && this.Key == tt.Key;
        }

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="tt1">The first <see cref="TradingTime"/> object to compare with the second.</param>
        /// <param name="tt2">The second <see cref="TradingTime"/> object to compare with the first.</param>
        /// <returns>True if <see cref="TradingTime"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingTime"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(TradingTime tt1, TradingTime tt2)
        {

            if (tt1 is null && tt2 is null)
                return true;

            if (tt1 is null || tt2 is null)
                return false;

            return tt1.Time == tt2.Time && tt1.UtcTime == tt2.UtcTime & tt1.Key == tt2.Key;

        }

        #endregion

        #region Compare methods

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return 1 if <paramref name="tt1"/> is major than <paramref name="tt2"/>, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// </summary>
        /// <param name="tt1">The first <see cref="TradingTime"/> object to compare with the second.</param>
        /// <param name="tt2">The second <see cref="TradingTime"/> object to compare with the first.</param>
        /// <returns>1 if <paramref name="tt1"/>is major than <paramref name="tt2"/>,
        /// -1 if <paramref name="tt1"/>is minor than <paramref name="tt1"/>,
        /// otherwise are equals and returns 0.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingTime"/>objects passed as parameter cannot be null.</exception>
        public static int Compare(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 == null || tt2 == null)
                throw new ArgumentException("The arguments can not be null.");

            TimeSpan tt1UtcTime = tt1.UtcTime;
            TimeSpan tt2UtcTime = tt2.UtcTime;

            if (tt1UtcTime > tt2UtcTime)
            {
                return 1;
            }

            if (tt1UtcTime < tt2UtcTime)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Compare <paramref name="value"/> object with the object instance and return 1 if the instance is major than the second, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare.</param>
        /// <returns>1 if <paramref name="value"/>is minor than the object instance,
        /// -1 if <paramref name="value"/>is major than the object instance,
        /// otherwise are equals and returns 0.</returns>
        /// <exception cref="ArgumentException">The object passed as parameter cannot be null.</exception>
        public int CompareTo(object value)
        {
            if (value == null)
            {
                throw new ArgumentException("Argument cannot be null");
            }

            if (!(value is TradingTime))
            {
                throw new ArgumentException("Argument must be TradingTimeInfo");
            }

            TimeSpan valueUtcTime = ((TradingTime)value).UtcTime;

            if (this.UtcTime > valueUtcTime)
            {
                return 1;
            }

            if (this.UtcTime < valueUtcTime)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Compare <see cref="TradingTime"/> to <see cref="TradingTime"/> instance and return 1 
        /// if the instance is major than the second, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// <param name="value">The target object to compare.</param>
        /// <returns></returns>
        public int CompareTo(TradingTime value)
        {
            if (value == null)
            {
                throw new ArgumentException("Argument cannot be null");
            }

            TimeSpan valueUtcTime = ((TradingTime)value).UtcTime;

            if (UtcTime > valueUtcTime)
            {
                return 1;
            }

            if (UtcTime < valueUtcTime)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #region Operators methods

        /// <summary>
        /// Determines whether two specified instances of <see cref="Core.TradingTime"/> that is greater than another specified.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> is greater than <paramref name="tt2"/>; otherwise, false.</returns>
        public static bool operator >(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            return tt1.UtcTime > tt2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingTime"/> that is earlier than another specified.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> is less than <paramref name="tt2"/>; otherwise, false.</returns>
        public static bool operator <(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            return tt1.UtcTime < tt2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingTime"/> that is the same as or greater than another specified.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> is equal to or greater than <paramref name="tt2"/>; otherwise, false.</returns>
        public static bool operator >=(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            return tt1.UtcTime >= tt2.UtcTime;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingTime"/> that is the same as or earlier than another specified.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> is equal to or less than <paramref name="tt2"/>; otherwise, false.</returns>
        public static bool operator <=(TradingTime tt1, TradingTime tt2)
        {

            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            return tt1.UtcTime <= tt2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingTime"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> and <paramref name="tt2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null && tt2 is null)
                return true;

            if (tt1 is null || tt2 is null)
                return false;

            return tt1.UtcTime == tt2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingTime"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="tt1">The first object to compare.</param>
        /// <param name="tt2">The second object to compare.</param>
        /// <returns>True if <paramref name="tt1"/> and <paramref name="tt2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null && tt2 is null)
                return false;

            if (tt1 is null && !(tt2 is null))
                return true;

            if (!(tt1 is null) || tt2 is null)
                return true;

            return tt1.UtcTime != tt2.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified session time, generating a new time span.
        /// </summary>
        /// <param name="tt1">TradingSessionInfo time value to add.</param>
        /// <param name="tt2">TradingSessionInfo time value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="tt1"/> and <paramref name="tt2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TradingTime operator +(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            TimeSpan newTime = (tt1.UtcTime + tt2.UtcTime)+tt1.TimeZoneInfo.BaseUtcOffset;
            return TradingTime.CreateCustomSessionTime(newTime,tt1.TimeZoneInfo);
        }

        /// <summary>
        /// Adds a specified session time to a specified time span, generating a new time span.
        /// </summary>
        /// <param name="tt">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns><see cref="TradingTime"/> that is the sum of the values ​​of <paramref name="tt"/> and <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TradingTime operator +(TradingTime tt, TimeSpan ts)
        {
            if (tt is null)
                throw new ArgumentNullException($"the argument {nameof(tt)} cannot be null.");
            tt.Time += ts;
            return tt;
        }

        /// <summary>
        /// Subtracts a specified session time from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="tt1">TradingSessionInfo time value to substract.</param>
        /// <param name="tt2">TradingSessionInfo time value to substract.</param>
        /// <returns>An <see cref="TradingTime"/> whose value is the value of <paramref name="tt1"/> minus the value of <paramref name="tt2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TradingTime operator -(TradingTime tt1, TradingTime tt2)
        {
            if (tt1 is null)
                throw new ArgumentNullException($"the argument {nameof(tt1)} cannot be null.");

            if (tt2 is null)
                throw new ArgumentNullException($"the argument {nameof(tt2)} cannot be null.");

            TimeSpan newTime = (tt1.UtcTime - tt2.UtcTime) + tt1.TimeZoneInfo.BaseUtcOffset;
            return TradingTime.CreateCustomSessionTime(newTime, tt1.TimeZoneInfo);
        }

        /// <summary>
        /// Subtracts a specified time span from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="tt">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns>An <see cref="TradingTime"/> whose value is the value of <paramref name="tt"/> minus the value of <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TradingTime operator -(TradingTime tt, TimeSpan ts)
        {
            if (tt is null)
                throw new ArgumentNullException($"the argument {nameof(tt)} cannot be null.");

            tt.Time -= ts;
            return tt;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns default code for custom session times.
        /// </summary>
        /// <returns>Returns a string with the hours and minutes in specific time zone info and utc time zone info.</returns>
        private string ToDefaultCode()
        {
            return $"CTM-{Time.Hours}{Time.Minutes}-{UtcTime.Hours}{UtcTime.Minutes}";
        }

        /// <summary>
        /// Added the session time to the date passed as parameter and returns it.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The <see cref="DateTime"/> that represents the session time.</returns>
        private DateTime ToSessionTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, Time.Hours, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        public static string GetTimeZoneInfoName(TimeZoneInfo timeZoneInfo)
        {
            string timeZoneInfoName = timeZoneInfo.StandardName;

            if (timeZoneInfo == TimeZoneInfo.Local)
                timeZoneInfoName = "Local";

            else if (timeZoneInfo == TimeZoneInfo.Utc)
                timeZoneInfoName = "Utc";

            return timeZoneInfoName;

        }

        #endregion

    }
}
