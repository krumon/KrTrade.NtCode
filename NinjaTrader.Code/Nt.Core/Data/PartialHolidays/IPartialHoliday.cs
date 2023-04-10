using System;
using System.Collections.ObjectModel;

namespace Nt.Core.Data
{
    /// <summary>
    /// An object containing a DateTime representing the date of the early close or late begin, 
    /// a description of the partial holiday, and two bool properties, IsEarlyClose and IsLateBegin.
    /// </summary>
    public interface IPartialHoliday
    {
        /// <summary>
        /// Represents the <see cref="DateTime"/> of the partial holiday.
        /// </summary>
        DateTime Date { get;}

        /// <summary>
        /// Descriptions of the partial holiday.
        /// </summary>
        string Description { get;}

        /// <summary>
        /// Indicates if the partial holiday has an early end.
        /// </summary>
        bool IsEarlyEnd { get;}

        /// <summary>
        /// Indicates if the partial holiday has a late begin.
        /// </summary>
        bool IsLateBegin { get;}

        /// <summary>
        /// Collection of partial holiday sessions.
        /// </summary>
        Collection<ISession> Sessions { get;}

    }
}
