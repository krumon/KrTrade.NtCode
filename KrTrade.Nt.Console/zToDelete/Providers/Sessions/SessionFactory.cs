using KrTrade.Nt.Console.Internal;
using KrTrade.Nt.Core.Sessions;
using KrTrade.Nt.DI.Data;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
{
    internal class SessionFactory
    {

        #region Private members

        private Func<SessionType, SessionDescriptor> _configureActions;
        private SessionDescriptor[] _descriptors;
        private TradingSessionCollection _sessions;

        #endregion

        #region Constructor

        public SessionFactory(IList<SessionDescriptor> descriptors)
        {
            _descriptors = new SessionDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors, 0);

            // Validar los descriptores y dejarlos preparados para cuando
            // sean llamados por el constructor de sesiones.
        }

        #endregion

        #region Public methods

        public TradingSessionCollection CreateTradingSessionCollection()
        {
            _sessions = new TradingSessionCollection();

            // TODO: Create the trading sessions.

            return _sessions;
        }


        #endregion

    }
}
