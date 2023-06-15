using System;

namespace KrTrade.NtCode.Data
{
    /// <summary>
    /// Definitions of specific session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// A <see cref="DayOfWeek"/> value representing the begin day
        /// </summary>
        DayOfWeek BeginDay { get; }

        /// <summary>
        /// An <see cref="int"/> value representing the begin time
        /// </summary>
        int BeginTime { get; }

        /// <summary>
        /// A <see cref="DayOfWeek"/> value representing the end day.
        /// </summary>
        DayOfWeek EndDay { get; }

        /// <summary>
        /// An <see cref="int"/> value representing the end time.
        /// </summary>
        int EndTime { get; }

        /// <summary>
        /// A <see cref="DayOfWeek"/> value representing the trading day this session belongs to.
        /// </summary>
        DayOfWeek TradingDay { get; }
    }
}
