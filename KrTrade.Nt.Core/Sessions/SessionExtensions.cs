using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Sessions
{
    /// <summary>
    /// Extension methods to create generic sessions.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.Regular"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.Regular"/></returns>
        public static Session CreateRegularSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.Regular, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.OVN"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.OVN"/></returns>
        public static Session CreateOvernightSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.OVN, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.European"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.European"/></returns>
        public static Session CreateEuropeanSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.European, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.Asian"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.Asian"/></returns>
        public static Session CreateAsianSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.Asian, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.American"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American"/></returns>
        public static Session CreateAmericanSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.American, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.AmericanAndEuropean"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.AmericanAndEuropean"/></returns>
        public static Session CreateAmericanAndEuropeanSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.AmericanAndEuropean, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS"/></returns>
        public static Session CreateAmericanResidualSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.American_RS, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.Asian_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.Asian_RS"/></returns>
        public static Session CreateAsianResidualSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.Asian_RS, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.American_RS_EXT"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.American_RS_EXT"/></returns>
        public static Session CreateAmericanResidualExtraTimeSession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.American_RS_EXT, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS_EOD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS_EOD"/></returns>
        public static Session CreateAmericanResidualEndOfDaySession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.American_RS_EOD, instrumentKey);
        }

        /// <summary>
        /// Create a <see cref="Session"/> instance with the generic type <see cref="SessionType.American_RS_NWD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="Session"/> with the generic type <see cref="SessionType.American_RS_NWD"/></returns>
        public static Session CreateAmericanResidualNewDaySession(this Session _, InstrumentCode instrumentKey)
        {
            return Session.CreateTradingSessionByType(SessionType.American_RS_NWD, instrumentKey);
        }

        ///// <summary>
        ///// Create a <see cref="TradingSession"/> instance with the generic american sessions types.
        ///// </summary>
        ///// <param name="_"></param>
        ///// <returns>An instance of <see cref="TradingSession"/> with the generic american sessions types.</returns>
        //public static SessionCollection CreateAmericanSessions(this TradingSession _)
        //{
        //    SessionCollection tradingSessions = new SessionCollection
        //    {
        //        SessionType.AmericanAndEuropean,
        //        SessionType.American
        //    };

        //    return tradingSessions;
        //}

        ///// <summary>
        ///// Create a <see cref="TradingSession"/> instance with the generic american residual sessions types.
        ///// </summary>
        ///// <param name="_"></param>
        ///// <returns>An instance of <see cref="TradingSession"/> with the generic american residual sessions types.</returns>
        //public static SessionCollection CreateAmericanResidualSessions(this TradingSession _)
        //{
        //    SessionCollection tradingSessions = new SessionCollection
        //    {
        //        SessionType.American_RS_EXT,
        //        SessionType.American_RS_EOD,
        //        SessionType.American_RS_NWD
        //    };

        //    return tradingSessions;
        //}

    }
}
