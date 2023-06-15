using Nt.Core.Hosting;
using KrTrade.NtCode.Ninjascripts;
using System;

namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="SessionsIterator"/>.
    /// </summary>
    public interface ISessionsIterator : IConfigurable, IRecalculableOnBarUpdate
    {

        /// <summary>
        /// Notify session changed.
        /// </summary>
        event SessionChangedEventHandler SessionChanged;

        /// <summary>
        /// Represents the actual session start time converted to the user's configured Time Zone.
        /// </summary>
        DateTime ActualSessionBegin { get; set; }

        /// <summary>
        /// Represents the actual session end time converted to the user's configured Time Zone.
        /// </summary>
        DateTime ActualSessionEnd { get; }

        ///// <summary>
        ///// Represents the user's configured <see cref="TimeZoneInfo"/>.
        ///// </summary>
        //TimeZoneInfo UserTimeZoneInfo { get; }

        ///// <summary>
        ///// Represents the bar's configured <see cref="TimeZoneInfo"/>.
        ///// </summary>
        //TimeZoneInfo BarsTimeZoneInfo { get; }

        /// <summary>
        /// The sessions counter.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Is true when a new session bars enter in a new session.
        /// </summary>
        bool IsSessionUpdated { get; }

        /// <summary>
        /// Is true if the bars configured by the 
        /// </summary>
        bool? BarsTypeIsIntraday { get; }

        /// <summary>
        /// Indicates the current bar.
        /// </summary>
        int CurrentBar { get; }

        /// <summary>
        /// Indicates the current time (Time[0]).
        /// </summary>
        DateTime CurrentTime { get; }
        
        /// <summary>
        /// Indicates if the session is in a partial holiday.
        /// </summary>
        bool? IsPartialHoliday { get; }

        /// <summary>
        /// Indicates if the session is in a partial holiday with late begin.
        /// </summary>
        bool? IsLateBegin { get; }

        /// <summary>
        /// Indicates if the session is in a partial holiday with early end.
        /// </summary>
        bool? IsEarlyEnd { get; }

        /// <summary>
        /// Prints the actual session values.
        /// </summary>
        /// <returns>The actual session values.</returns>
        string Print();

    }
}
