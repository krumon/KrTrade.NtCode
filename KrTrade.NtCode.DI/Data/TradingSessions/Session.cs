using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// Contents trading session information.
    /// </summary>
    public class Session : BaseElement, ISessions
    {

        #region Consts

        DateTime TIME_REFERENCE = new DateTime(1978,9,20,0,0,0,DateTimeKind.Local);

        #endregion

        #region Private members

        /// <summary>
        /// The trading session type.
        /// </summary>
        private SessionType _sessionType;

        /// <summary>
        /// The children sessions.
        /// </summary>
        private readonly SessionCollection _sessions = new SessionCollection();

        private TradingTime _endSessionTime;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading session type.
        /// </summary>
        public SessionType SessionType
        {
            private set
            {
                _sessionType = value;

                if (_sessionType == SessionType.Custom)
                {
                    Code = ToDefaultCode();
                    if (string.IsNullOrEmpty(Description))
                        Description = $"Custom TradingSessionInfo Hours {BeginSessionTime.LocalTime.TotalHours}.{EndSessionTime.LocalTime.TotalHours}.";
                }
                else
                {
                    Code = _sessionType.ToCode();
                    Description = _sessionType.ToDescription();
                }
            }
            get => _sessionType;
        }

        /// <summary>
        /// The children sessions.
        /// </summary>
        public ITradingSessionCollection Sessions => _sessions;

        /// <summary>
        /// Gets the unique code of the <see cref="Session"/>.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the description of the <see cref="Session"/>.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The initial <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime BeginSessionTime { get; set; }

        /// <summary>
        /// The final <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime EndSessionTime 
        {
            get => _endSessionTime; 
            set
            {
                _endSessionTime = value;

                if (_endSessionTime <= BeginSessionTime)
                    _endSessionTime += TimeSpan.FromDays(1);
            }
        }

        /// <summary>
        /// TradingSessionInfo hours duration.
        /// </summary>
        public TimeSpan Duration => EndSessionTime.Time - BeginSessionTime.Time;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="Session"/> class.
        /// </summary>
        public Session()
        {
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="Session"/> class with <see cref="Session"/>.
        /// </summary>
        /// <param name="sessionType">the <see cref="Session"/> to create the <see cref="Session"/> class.</param>
        /// <param name="instrumentKey">The unique code of the instrument.</param>
        /// <param name="beginTimeDisplacement">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="Session"/> class.</returns>
        public static Session CreateTradingSessionByType(SessionType sessionType, InstrumentCode instrumentKey, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            return new Session
            {
                BeginSessionTime = sessionType.ToBeginSessionTime(instrumentKey, beginTimeDisplacement),
                EndSessionTime = sessionType.ToEndSessionTime(instrumentKey, endTimeDisplacement),
                SessionType = sessionType,
            };
        }

        /// <summary>
        /// Create a new instance of <see cref="Session"/> collection with <see cref="SessionType"/> collection.
        /// </summary>
        /// <param name="sessionTypes">The <see cref="SessionType"/> collection to create the <see cref="Session"/> collection.</param>
        /// <param name="instrumentKey">The unique code of the instrument.</param>
        /// <param name="beginTimeDisplacement">The displacement minutes to the intial balance of the session.</param>
        /// <param name="endTimeDisplacement">The displacement minutes to the final balance of the session.</param>
        /// <returns>A new instance of <see cref="Session"/> collection.</returns>
        public static Session[] CreateTradingSessionByTypes(SessionType[] sessionTypes, InstrumentCode instrumentKey, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            if (sessionTypes == null || sessionTypes.Length < 1)
                throw new ArgumentNullException(nameof(sessionTypes));

            Session[] tradingSessions = new Session[sessionTypes.Length];
            for (int i = 0; i < sessionTypes.Length; i++)
            {
                Session ts = Session.CreateTradingSessionByType(sessionTypes[i], instrumentKey);
                tradingSessions[i] = ts;
            }

            return tradingSessions;
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTimeType"/> types and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginSessionTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="endSessionTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(InstrumentCode instrumentKey, TradingTimeType beginSessionTimeType, TradingTimeType endSessionTimeType, string description = "")
        {
            return new Session
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginSessionTimeType,instrumentKey),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endSessionTimeType,instrumentKey),
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="Session"/> object.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(InstrumentCode instrumentKey, TradingTime beginTradingTime, TradingTimeType endTradingTimeType, string description = "")
        {
            return new Session
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType, instrumentKey),
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="Session"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(InstrumentCode instrumentKey, TradingTimeType beginTradingTimeType, TradingTime endTradingTime, string description = "")
        {
            return new Session
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType, instrumentKey),
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTime"/> objects and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="Session"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="Session"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(TradingTime beginTradingTime, TradingTime endTradingTime, string description = "")
        {
            return new Session
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(InstrumentCode instrumentKey, TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TradingTimeType endTradingTimeType, string description = "")
        {
            return new Session
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType, instrumentKey),
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="Session"/> object.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(InstrumentCode instrumentKey, TradingTimeType beginTradingTimeType, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new Session
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType, instrumentKey),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="Session"/> objects with specific <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="Session"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="Session"/> object.</returns>
        public static Session CreateCustomTradingSession(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new Session
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Close Time" : description + " - Close"),
                Description = description,
                SessionType = SessionType.Custom,
            };
        }

        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public ISessions this[int index]
        {
            get => _sessions[index];
            set => _sessions[index] = value;
        }

        /// <inheritdoc/>
        public int Count => _sessions.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(ISessions session)
        {
            _sessions.Add(session);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _sessions.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(ISessions item)
        {
            return _sessions.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(ISessions[] array, int arrayIndex)
        {
            _sessions.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<ISessions> GetEnumerator()
        {
            return _sessions.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(ISessions item)
        {
            return _sessions.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, ISessions item)
        {
            _sessions.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(ISessions item)
        {
            return _sessions.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _sessions.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the initial <see cref="DateTime"/> of the <see cref="Session"/>.
        /// </summary>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time)
        {
            return BeginSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time,sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time,sourceTimeZoneInfo,destinationTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time)
        {
            return EndSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time,sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time,sourceTimeZoneInfo,destinationTimeZoneInfo);
        }

        // TODO: Terminar este método. ES COMPLICADO!!!!!!

        public DateTime? GetNextSessionEndTime(DateTime currentTime, DateTime actualSessionBeginTime, DateTime actualSessionEndTime)
        {
            TimeZoneInfo sourceTimeZoneInfo = 
                currentTime.Kind == DateTimeKind.Local ? TimeZoneInfo.Local : 
                currentTime.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : null;

            if (sourceTimeZoneInfo != null)
            {
                DateTime beginTime = GetBeginTime(currentTime);
                DateTime endTime = GetEndTime(currentTime);

                bool dayChanges = actualSessionEndTime.Date - actualSessionBeginTime.Date != TimeSpan.Zero;

                if (endTime <= beginTime && dayChanges)
                    return GetEndTime(actualSessionEndTime);
                
                if (beginTime > actualSessionBeginTime)
                {

                }
            }
                // Returns the TradingTimeInfo TimeSpan for the date passed as parameter.
                //return GetTime(time, sourceTimeZoneInfo);

            throw new Exception("The kind of the " + nameof(currentTime) + " must be Local or Utc");

        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime GetNextEndTime(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            DateTime beginDateTime = BeginSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            if (endDateTime <= beginDateTime)
                return endDateTime.AddHours(24);

            return EndSessionTime.GetTime(currentDate);
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="Session"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The initial and final <see cref="DateTime"/> structures of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime[] GetNextDateTimes(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null,
            bool sessionComplete = false)
        {
            DateTime[] sessionDateTimes = new DateTime[2];
            DateTime beginDateTime = BeginSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            if (sessionComplete && (endDateTime <= beginDateTime))
                endDateTime += TimeSpan.FromHours(24);

            sessionDateTimes[0] = beginDateTime;
            sessionDateTimes[1] = endDateTime;

            return sessionDateTimes;
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

        #region Equals methods

        /// <summary>
        /// Compare <see cref="Session"/> objects and return true if the elements are equals.
        /// The <see cref="Session"/> objects are equals if the times and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Session ts)
                return BeginSessionTime.Equals(ts.BeginSessionTime) && EndSessionTime.Equals(ts.EndSessionTime);

            return false;
        }

        /// <summary>
        /// Compare <see cref="Session"/> objects and return true if the elements are equals.
        /// the <see cref="Session"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="Session"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="Session"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="Session"/>object passed as parameter cannot be null.</exception>
        public bool Equals(ISessions ts)
        {

            if (ts is null)
                return false;

            if (ts is Session t)
                return BeginSessionTime.Equals(t.BeginSessionTime) && EndSessionTime.Equals(t.EndSessionTime);

            return false;
        }

        /// <summary>
        /// Compare <see cref="Session"/> objects and return true if the elements are equals.
        /// the <see cref="Session"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="ts1">The first <see cref="Session"/> object to compare with the second.</param>
        /// <param name="ts2">The second <see cref="Session"/> object to compare with the first.</param>
        /// <returns>True if <see cref="Session"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(ISessions ts1, ISessions ts2)
        {

            if (ts1 is null && ts2 is null)
                return true;

            if (ts1 is null || ts2 is null)
                return false;

            if (ts1 is Session t1)
                if (ts2 is Session t2)
                    return TradingTime.Equals(t1.BeginSessionTime, t2.BeginSessionTime) && TradingTime.Equals(t1.EndSessionTime, t2.EndSessionTime);

            return false;
        }

        #endregion

        #region Compare methods

        /// <summary>
        /// Compare <paramref name="value1"/> and <paramref name="value2"/> objects and 
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first object to compare with the second.</param>
        /// <param name="value2">The second object to compare with the first.</param>
        /// <returns>1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="Session"/> objects.</exception>
        public int Compare(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is Session ts1)
                {
                    if (value2 is Session ts2)
                    {
                        return Compare(ts1, ts2);
                    }
                    else
                        throw new ArgumentException(nameof(value2));
                }
                else
                    throw new ArgumentException(nameof(value1));
            }

            throw new ArgumentNullException("The value1 and value2 cannot be null.");
        }

        /// <summary>
        /// Compare <see cref="Session"/> objects and 
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first <see cref="Session"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="Session"/> object to compare with the first.</param>
        /// <returns>1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        public int Compare(ISessions value1, ISessions value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            if (value1 is Session v1)
                if (value2 is Session v2)
                    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

            throw new ArgumentException("The arguments must be KrTrade.Nt.DI.Trading.Session.");
        }

        /// <summary>
        /// Compare the <see cref="Session"/> to <paramref name="value"/> object and 
        /// return 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="Session"/>.</param>
        /// <returns>1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="Session"/> objects.</exception>
        public int CompareTo(object value)
        {
            if (!(value is null))
            {
                if (value is Session ts)
                {
                    return Compare(this, ts);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="Session"/> to <see cref="Session"/> object and 
        /// return 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="Session"/>.</param>
        /// <returns>1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        public int CompareTo(ISessions value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return Compare(this, value);
        }

        /// <summary>
        /// Compare <paramref name="value1"/> and <paramref name="value2"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first object to compare with the second.</param>
        /// <param name="value2">The second object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="Session"/> objects.</exception>
        public SessionCompareResult CompareSession(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is Session ts1)
                {
                    if (value2 is Session ts2)
                    {
                        return CompareSession(ts1, ts2);
                    }
                    else
                        throw new ArgumentException(nameof(value2));
                }
                else
                    throw new ArgumentException(nameof(value1));
            }

            throw new ArgumentNullException("The value1 and value2 cannot be null.");
        }

        /// <summary>
        /// Compare <see cref="Session"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first <see cref="Session"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="Session"/> object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        public SessionCompareResult CompareSession(ISessions value1, ISessions value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            return CompareSessions((Session)value1, (Session)value2);
            
        }

        /// <summary>
        /// Compare the <see cref="Session"/> to <paramref name="value"/> object and 
        /// return 3 if <see cref="Session"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="Session"/> is the parent,
        /// return 2 if <see cref="Session"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="Session"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="Session"/>.</param>
        /// <returns>3 if <see cref="Session"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="Session"/> is the parent,
        /// 2 if <see cref="Session"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="Session"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="Session"/> objects.</exception>
        public SessionCompareResult CompareSessionTo(object value)
        {
            if (!(value is null))
            {
                if (value is Session ts)
                {
                    return CompareSession(this, ts);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="Session"/> to <see cref="Session"/> object and 
        /// return 3 if <see cref="Session"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="Session"/> is the parent,
        /// return 2 if <see cref="Session"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="Session"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="Session"/>.</param>
        /// <returns>3 if <see cref="Session"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="Session"/> is the parent,
        /// 2 if <see cref="Session"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="Session"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="Session"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="Session"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="Session"/>objects passed as parameter cannot be null.</exception>
        public SessionCompareResult CompareSessionTo(ISessions value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return CompareSession(this, value);
        }

        #endregion

        #region Opertator methods

        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> that is greater than another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is greater than <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator >(Session ts1, Session ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            SessionCompareResult result = CompareSessions(ts1, ts2);
            if (
                result == SessionCompareResult.Later ||
                result == SessionCompareResult.InnerAndLater
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> that is earlier than another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is less than <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator <(Session ts1, Session ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            SessionCompareResult result = CompareSessions(ts1, ts2);
            if (
                result == SessionCompareResult.Before ||
                result == SessionCompareResult.BeforeAndInner
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> that is the parent of another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is parent of <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator >=(Session ts1, Session ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            SessionCompareResult result = CompareSessions(ts1, ts2);
            if (
                result == SessionCompareResult.Equals ||
                result == SessionCompareResult.Later ||
                result == SessionCompareResult.InnerAndLater
               )
                return true;

            return false;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> that is the child of another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is the child of <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator <=(Session ts1, Session ts2)
        {

            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            SessionCompareResult result = CompareSessions(ts1, ts2);
            if (
                result == SessionCompareResult.Equals ||
                result == SessionCompareResult.Before ||
                result == SessionCompareResult.BeforeAndInner
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> and <paramref name="ts2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(Session ts1, Session ts2)
        {
            if (ts1 is null && ts2 is null)
                return true;

            if (ts1 is null || ts2 is null)
                return false;

            SessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == SessionCompareResult.Equals)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Session"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> and <paramref name="ts2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(Session ts1, Session ts2)
        {
            return !(ts1 == ts2);
        }

        /// <summary>
        /// Adds a specified session time to a specified session time, generating a new time span.
        /// </summary>
        /// <param name="sh1">TradingSessionInfo time value to add.</param>
        /// <param name="sh2">TradingSessionInfo time value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="sh1"/> and <paramref name="sh2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(Session sh1, Session sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return sh1.BeginSessionTime.UtcTime + sh2.EndSessionTime.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified time span, generating a new time span.
        /// </summary>
        /// <param name="sh">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="sh"/> and <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(Session sh, TimeSpan ts)
        {
            if (sh is null)
                throw new ArgumentNullException($"the argument {nameof(sh)} cannot be null.");

            return new TimeSpan((sh.EndSessionTime.UtcTime + ts).Ticks);
        }

        /// <summary>
        /// Subtracts a specified session time from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="sh1">TradingSessionInfo time value to substract.</param>
        /// <param name="sh2">TradingSessionInfo time value to substract.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="sh1"/> minus the value of <paramref name="sh2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(Session sh1, Session sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return new TimeSpan((sh1.EndSessionTime.UtcTime - sh2.BeginSessionTime.UtcTime).Ticks);
        }

        /// <summary>
        /// Subtracts a specified time span from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="sh">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="sh"/> minus the value of <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(Session sh, TimeSpan ts)
        {
            if (sh is null)
                throw new ArgumentNullException($"the argument {nameof(sh)} cannot be null.");

            return new TimeSpan((sh.BeginSessionTime.UtcTime - ts).Ticks);
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Converts the <see cref="Session"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString("U");

        /// <summary>
        /// Returns the string that represents the <see cref="Session"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            return $"{SessionType.ToName()} - Begin: {BeginSessionTime.ToShortString(TimeZoneInfo.Local, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(TimeZoneInfo.Local, true)}";
        }

        /// <summary>
        /// Converts the <see cref="Session"/> to short string.
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return String.Format($"{BeginSessionTime.ToShortString()} => {EndSessionTime.ToShortString()}");
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToShortString(string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            return $"{SessionType.ToName()} - Begin: {BeginSessionTime.ToShortString(TimeZoneInfo.Local, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(TimeZoneInfo.Local, true)}";

        }

        /// <summary>
        /// Converts the <see cref="Session"/> to long string.
        /// </summary>
        /// <returns></returns>
        public string ToLongString() => ToLongString("U");

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToLongString(string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            // TODO: Cambiar los métodos ToString().
            return $"{SessionType.ToDescription()} - Begin: {BeginSessionTime.ToShortString(TimeZoneInfo.Local, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(TimeZoneInfo.Local, true)}";
        }

        /// <summary>
        /// Converts the <see cref="Session"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(bool onlyActualSession)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(DateTime.Now);
            string sessions = String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
            if (!onlyActualSession)
            {
                //if (HasSessionHours)
                //    for (int i = 0; i < SessionHours.Count; i++)
                //        sessionHoursList += Environment.NewLine + SessionHours[i].ToString(onlyActualSession);
            }

            return sessions;
        }

        /// <summary>
        /// Converts the <see cref="Session"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(DateTime referenceDateTime)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(referenceDateTime);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns default code for custom session times.
        /// </summary>
        /// <returns>Returns a string with the hours and minutes in specific time zone info and utc time zone info.</returns>
        private string ToDefaultCode()
        {
            return $"CTM-{BeginSessionTime.Time.TotalHours}{EndSessionTime.Time.TotalHours}-{BeginSessionTime.UtcTime.TotalHours}{EndSessionTime.UtcTime.TotalHours}";
        }

        private static SessionCompareResult CompareSessions(Session ts1, Session ts2)
        {
            if (
                ts1.BeginSessionTime == ts2.BeginSessionTime &&
                ts1.EndSessionTime == ts2.EndSessionTime
                )
                // Returns 0
                return SessionCompareResult.Equals;
            else if (
                ts1.BeginSessionTime >= ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.EndSessionTime
                )
                // Returns -3
                return SessionCompareResult.Inner;
            else if (
                ts1.BeginSessionTime <= ts2.BeginSessionTime &&
                ts1.EndSessionTime >= ts2.EndSessionTime
                )
                // Returns 3
                return SessionCompareResult.Outer;
            else if (
                ts1.BeginSessionTime < ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.BeginSessionTime
                )
                // Returns -2
                return SessionCompareResult.Before;
            else if (
                ts1.BeginSessionTime >= ts2.EndSessionTime &&
                ts1.EndSessionTime > ts2.EndSessionTime
                )
                // Returns 2
                return SessionCompareResult.Later;
            else if (
                ts1.BeginSessionTime < ts2.BeginSessionTime &&
                ts1.EndSessionTime > ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.EndSessionTime
                )
                // Returns -1
                return SessionCompareResult.BeforeAndInner;
            else if (
                ts1.BeginSessionTime >= ts2.BeginSessionTime &&
                ts1.BeginSessionTime < ts2.EndSessionTime &&
                ts1.EndSessionTime > ts2.EndSessionTime
                )
                // Returns 1
                return SessionCompareResult.InnerAndLater;

            // Alert an calculate error
            Debug.Assert(false);
            return SessionCompareResult.Later;
        }

        #endregion

    }
}
