using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using KrTrade.Nt.Console.Internal;
using KrTrade.Nt.DI.Events;
using System;
using System.Collections.Generic;
using KrTrade.Nt.DI.Data;

namespace KrTrade.Nt.Console
{
    /// <summary>
    /// Represents the trading hours for any session.
    /// </summary>
    public class SessionHours : BaseSession<SessionHours,SessionHoursConfiguration,SessionHoursBuilder>, ISessionHours
    {

        #region Private members

        /// <summary>
        /// Store the minor sessions in the main session.
        /// </summary>
        private List<TradingSession> sessions;

        /// <summary>
        /// String that contents the type of day (regular or partial holiday).
        /// </summary>
        string holidayText;

        #endregion

        #region Public properties

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session begin.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session end.
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
        /// Collection of <see cref="TradingSessionInfo"/>.
        /// </summary>
        //public List<TradingSession> Sessions
        //{
        //    get
        //    {
        //        if (sessions == null)
        //            sessions = new List<TradingSession>();

        //        return sessions;
        //    }
        //}

        /// <summary>
        /// Indicates if the <see cref="SessionHours"/> has sessionHoursList.
        /// </summary>
        //public bool HasSessions => Sessions != null && Sessions.Count >= 1;

        /// <summary>
        /// Indicates if the current time is in session.
        /// </summary>
        public bool IsInSession { get;private set; }

        /// <summary>
        /// Indicates if the trading hours is a partial partialHoliday.
        /// </summary>
        public bool? IsPartialHoliday { get; private set; } // => PartialHoliday != null; // {get; private set;}

        /// <summary>
        /// Indicates if the partial partialHoliday has a late begin time.
        /// </summary>
        public bool? IsLateBegin { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial partialHoliday has a early end.
        /// </summary>
        public bool? IsEarlyEnd { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsEarlyEnd;

        /// <summary>
        /// The number of main sessionHoursList stored.
        /// </summary>
        public int N { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHours"/> default instance.
        /// </summary>
        protected SessionHours() : base()
        {
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Creates the <see cref="SessionHoursBuilder"/> to construct the <see cref="SessionHours"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionHoursBuilder"/> to construct the <see cref="SessionHours"/> object.</returns>
        public ISessionHoursBuilder CreateSessionHoursBuilder() => CreateBuilder<SessionHours, SessionHoursBuilder>();

        #endregion

        #region State changed methods

        /// <summary>
        /// Method to set default properties
        /// This method is executed when "ConfigureProperties" methods is raised.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        protected override void SetDefault(NinjaScriptBase ninjascript)
        {
        }

        /// <summary>
        /// AddValues the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        /// <param name="bars">The bars loaded in the graphics.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            base.Load(ninjascript, bars);
        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
        }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            if (ninjascript.Time[0] > BeginTime && ninjascript.Time[0] <= EndTime)
                IsInSession = true;
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            if (ninjascript.Time[0] > BeginTime && ninjascript.Time[0] <= EndTime)
                IsInSession = true;
        }

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnSessionChanged(SessionUpdateArgs e)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// If the trading hours is in partial partialHoliday, gets the Partial Holiday object, otherwise, partial partialHoliday is null.
        /// </summary>
        /// <param name="e"></param>
        public void SetValues(SessionUpdateArgs e)
        {
            Idx = e.Idx;
            N = e.Count;
            BeginTime = e.BeginTime;
            EndTime = e.EndTime;
            IsPartialHoliday = e.IsPartialHoliday;
            IsEarlyEnd = e.IsEarlyEnd;
            IsLateBegin = e.IsLateBegin;

            holidayText = (bool)!IsPartialHoliday ? "Regular Day." : (bool)IsLateBegin ? "Partial Holiday - Late Begin." : "Partial Holiday - Early End.";

            // TODO: Add Generic SessionHours existing in the configuration object. (American, Assian, Custom,...)
            //       Add Custom SessionHours existing in the configuration object. 
        }

        /// <summary>
        /// Adds <see cref="TradingSessionInfo"/> list to session hours sessions.
        /// </summary>
        /// <param name="sessionsInfo"></param>
        //public void AddSessions(params TradingSession[] sessionsInfo)
        //{
        //    foreach (var sessionInfo in sessionsInfo)
        //        Sessions.Add(sessionInfo);
        //}



        //public void Remove(TradingSession session)
        //{
        //    if (session == null)
        //        throw new ArgumentNullException(nameof(session));

        //    if (Sessions == null)
        //        throw new ArgumentNullException(nameof(SessionHours));

        //    Sessions.Remove(session);

        //    if (Sessions.Count == 0)
        //        sessions = null;
        //}

        public void Clear()
        {
            //Sessions.Clear();

            //sessions = null;
        }

        /// <summary>
        /// Converts the <see cref="TradingSessionInfo"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
                //$"TNinjaScript: {N}. {TradingDay} \t" +
                //$"{BeginTime.ToShortDateString()} at {BeginTime.ToLongTimeString()} --> " +
                //$"{EndTime.ToShortDateString()} at {EndTime.ToLongTimeString()} \t" +
                //$"{holidayText}";
            
        }

        //public void Iterator(Action<TradingSessionInfo> action = null)
        //{
        //    action(this);
        //    if (HasSessionHours)
        //        for (int i = 0; i < SessionHours.Count; i++)
        //            SessionHours[i].Iterator(action);
        //}

        #endregion

        #region Private methods

        /// <summary>
        /// Add <see cref="TradingSession"/> to the session hours sessions in the correct place.
        /// </summary>
        /// <param name="sessionType"></param>
        /// <param name="instrumentCode"></param>
        /// <param name="includeInitialBalance"></param>
        /// <param name="includeFinalBalance"></param>
        private void AddSession(
            SessionType sessionType,
            InstrumentCode instrumentCode,
            int includeInitialBalance = 0,
            int includeFinalBalance = 0)
        {

            //Sessions.Add(sessionType.ToSessionHours(instrumentCode));
        }


        // TODO: Codificar el método "Add" para añadir sesiones conforme al enum TradingSession
        //       y organizando según queramos que se vean las sesiones.
        private void Add(TradingSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            //if (HasSessionHours)
            //{
            //    DateTime[] nextDateTimes = session.GetNextDateTimes(DateTime.Now);
            //    foreach (var s in SessionHours)
            //    {
            //        session.IsInnerSession(s);
            //    }
            //}
            //else
            //    SessionHours.Add(session);

            //Sessions.Add(session);
        }

        #endregion

    }


}
