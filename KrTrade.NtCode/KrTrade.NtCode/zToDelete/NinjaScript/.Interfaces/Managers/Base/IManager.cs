using KrTrade.NtCode.Events;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace KrTrade.NtCode
{
    /// <summary>
    /// The interfece for any ninjascripts manager.
    /// </summary>
    public interface IManager : INinjascript
    {

        /// <summary>
        /// Gets a list order by <see cref="EventType"/> script action order.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        IEnumerable<INinjascript> GetSortedList(EventType eventType);

        /// <summary>
        /// Sets the ninjascripts collection from <see cref="IManagerBuilder"/> object.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null);

    }

}
