using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents the Trading Hours information returned from the current bars series. 
    /// The Trading Hours object contains several methods and properties for working with various trading sessions.
    /// </summary>
    public interface ITradingHours
    {

        /// <summary>
        /// Indicates the name of the trading hours template applied to the Bars series object.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indicates a time zone that is configured by a Trading Hour template
        /// </summary>
        TimeZoneInfo TimeZoneInfo { get; }

        /// <summary>
        /// A collection of session definitions of the trading hours template.
        /// </summary>
        Collection<ISession> Sessions { get; }

        /// <summary>
        /// A <see cref="Dictionary{TKey, TValue}"/> holding a collection of holiday Dates and Descriptions of each holiday.
        /// </summary>
        Dictionary<DateTime, string> Holidays { get; }

        /// <summary>
        /// A <see cref="Dictionary{TKey, TValue}"/> holding a collection of holiday Dates and Descriptions of each holiday.
        /// </summary>
        Dictionary<DateTime, IPartialHoliday> PartialHolidays { get; }

    }
}
