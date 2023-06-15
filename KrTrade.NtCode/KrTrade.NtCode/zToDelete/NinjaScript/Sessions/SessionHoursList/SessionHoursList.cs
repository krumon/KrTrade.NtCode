using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Events;
using System;
using System.Collections.Generic;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents the SessionHours collection funcionality for any session.
    /// </summary>
    public class SessionHoursList : BaseSession<SessionHoursList, SessionHoursListConfiguration,SessionHoursListBuilder>, ISessionHoursList
    {
        #region Private members

        /// <summary>
        /// Represents the last session hours.
        /// </summary>
        private SessionHours lastSession;

        /// <summary>
        /// Represents the actual session hours.
        /// </summary>
        private SessionHours actualSession;

        /// <summary>
        /// Stores all session hours in a sorted list.
        /// </summary>
        private List<SessionHours> sessionHoursList;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        public bool HasSessions => sessionHoursList != null && sessionHoursList.Count > 0;

        /// <summary>
        /// Gets the number of <see cref="SessionHours"/> stored.
        /// </summary>
        public int Count => HasSessions ? sessionHoursList.Count : 0;

        /// <summary>
        /// Gets the actual session.
        /// </summary>
        public SessionHours GetActualSession => actualSession;

        /// <summary>
        /// Gets the last session.
        /// </summary>
        public SessionHours GetLastSession => lastSession;

        /// <summary>
        /// Gets the session stored in the index.
        /// </summary>
        /// <param name="sessionsAgo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SessionHours this[int sessionsAgo]
        { 
            get
            {
                if (sessionsAgo >= Count || sessionsAgo < 0)
                    throw new ArgumentOutOfRangeException(nameof(sessionsAgo));

                return sessionHoursList[Count-1-sessionsAgo];
            } 
        }

        #endregion

        #region Configure properties

        /// <summary>
        /// Max sessions to stored
        /// </summary>
        public int MaxSessionsToStored { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursList"/> default instance.
        /// </summary>
        protected SessionHoursList() : base()
        {
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Creates the <see cref="SessionHoursListBuilder"/> to construct the <see cref="SessionHoursList"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionHoursListBuilder"/> to construct the <see cref="SessionHoursList"/> object.</returns>
        public ISessionHoursListBuilder CreateSessionHoursListBuilder() => CreateBuilder<SessionHoursList, SessionHoursListBuilder>();

        #endregion

        #region State changed methods

        /// <summary>
        /// Loaded <see cref="SessionHoursList"/> in "OnStateChanged" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method to load.
            base.Load(ninjascript, bars);

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
        }

        #endregion

        #region Market data methods

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnSessionChanged(SessionUpdateArgs e)
        {
            // Update actual session
            lastSession = actualSession;
            actualSession = (SessionHours)SessionHours.CreateDefaultBuilder().Build();
            actualSession.SetValues(e);
            // TODO: Revisar esta asignación.
            actualSession.N = e.Count;

            // if List is null...create the new list.
            if (sessionHoursList == null)
                sessionHoursList = new List<SessionHours>();
            // If list is overflow...remove the oldest element
            if (Count >= MaxSessionsToStored)
                sessionHoursList.Remove(sessionHoursList[0]);
            // Add to the list the new session
            sessionHoursList.Add(actualSession);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Represent a string with the last session stored.
        /// </summary>
        /// <returns>String of the last session stored.</returns>
        public override string ToString()
        {
            return actualSession != null ? actualSession.ToString() : "Actual session is null.";
        }

        /// <summary>
        /// Represent a string with the session stored.
        /// </summary>
        /// <param name="sessionsAgo">The session index. 0 is the actual, 1 is the last,...</param>
        /// <returns>String of the last session stored.</returns>
        public string ToString(int sessionsAgo)
        {
            if (sessionsAgo < 0 || sessionsAgo >= Count)
                return "Index is out of range";

            return sessionHoursList[Count - 1 - sessionsAgo].ToString();
        }

        #endregion

    }


}
