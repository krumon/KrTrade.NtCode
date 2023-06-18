using KrTrade.NtCode.Events;
using System;

namespace KrTrade.NtCode
{

    /// <summary>
    /// Interface for any sessions iterator.
    /// </summary>
    public interface ISessionsIterator : ISession
    {

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        event Action<SessionUpdateArgs> SessionChanged;

        /// <summary>
        /// Creates the <see cref="ISessionsIteratorBuilder"/> to construct the <see cref="ISessionsIterator"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionsIteratorBuilder"/> to construct the <see cref="ISessionsIterator"/> object.</returns>
        ISessionsIteratorBuilder CreateSessionsIteratorBuilder();

    }
}
