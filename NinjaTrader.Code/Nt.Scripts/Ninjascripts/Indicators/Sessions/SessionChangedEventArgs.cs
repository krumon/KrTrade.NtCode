using System;

namespace Nt.Scripts.Ninjascripts.Indicators
{

    /// <summary>
    /// Represents the arguments of the <see cref="SessionChangedEventHandler"/>.
    /// </summary>
    public class SessionChangedEventArgs : EventArgs
    {

        #region Public properties

        /// <summary>
        /// The index of the first bar of the session.
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// The number of the session.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Represents the actual session begin time.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// Represents the actual session end time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Represents the actual session begin <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek BeginDay => BeginTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session end <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek EndDay => EndTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session <see cref="DayOfWeek"/>.
        /// The day represents the <see cref="DayOfWeek"/> of the EndTime./>
        /// </summary>
        public DayOfWeek TradingDay => EndTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session time zone info.
        /// </summary>
        public TimeZoneInfo SessionTimeZoneInfo { get; set; }

        /// <summary>
        /// Indicates if the trading hours is a partial partialHoliday.
        /// </summary>
        public bool? IsPartialHoliday { get; set; }

        /// <summary>
        /// Indicates if the partial partialHoliday has a late begin time.
        /// </summary>
        public bool? IsLateBegin { get; set; }

        /// <summary>
        /// Indicates if the partial partialHoliday has a early end.
        /// </summary>
        public bool? IsEarlyEnd { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionUpdateArgs"/> default instance.
        /// </summary>
        public SessionChangedEventArgs()
        {

        }

        /// <summary>
        /// Create <see cref="SessionUpdateArgs"/> instance with specific parameters.
        /// </summary>
        /// <param name="idx">The bar index.</param>
        /// <param name="count">The session count.</param>
        /// <param name="beginTime">The begin time of the session.</param>
        /// <param name="endTime">The end time of the session.</param>
        /// <param name="timeZoneInfo">The time zone info of the session.</param>
        /// <param name="isPartialHoliday">Indicates if the session is in partial holiday.</param>
        /// <param name="IsLateBegin">Indicates if the partial holidat is a late begin session.</param>
        /// <param name="IsEarlyEnd">Indicates if the partial holiday is a early end session.</param>
        public SessionChangedEventArgs(int idx, int count, DateTime beginTime, DateTime endTime, TimeZoneInfo timeZoneInfo, bool? isPartialHoliday, bool? isLateBegin, bool? isEarlyEnd)
        {
            Idx = idx;
            Count = count;
            BeginTime = beginTime;
            EndTime = endTime;
            SessionTimeZoneInfo = timeZoneInfo;
            IsPartialHoliday = isPartialHoliday;
            IsLateBegin = isLateBegin;
            IsEarlyEnd = isEarlyEnd;
        }

        #endregion

    }
}
