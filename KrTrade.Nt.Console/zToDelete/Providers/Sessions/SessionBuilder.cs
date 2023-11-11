using KrTrade.Nt.Console.Internal;
using System;
using System.Collections.Generic;
using KrTrade.Nt.Core.Sessions;
using KrTrade.Nt.Core.Helpers;

namespace KrTrade.Nt.Console
{
    public class SessionBuilder : ISessionsBuilder
    {
        #region Private members

        private IList<SessionDescriptor> _descriptors = new List<SessionDescriptor>();
        private SessionFactory _sessionFactory;
        private SessionProvider _sessionProvider;
        private List<Func<SessionProviderConfiguration, SessionBuilder>> _sessionProviderConfigureActions;
        private Dictionary<string, Func<TradingSessionConfiguration, SessionProvider>> _tradingSessionConfigureActions;
        private InstrumentProvider _instrumentProvider;
        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionBuilder"/> default instance.
        /// </summary>
        public SessionBuilder()
        {

        }

        #endregion

        #region Implementation methods

        public ISessionProvider Build()
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return _sessionProvider;

            if (_descriptors == null || _descriptors.Count < 1)
                CreateDefaultSessionProvider();
            else
            {
                _sessionFactory = new SessionFactory(_descriptors);
                AddTypesToTradingSessionCollection();
                SortTradingSessionCollection();
                CreateTradingSession();
            }

            // Sets the flag to indicate the Trading session is created.
            _isBuild = true;

            return _sessionProvider;
        }

        #endregion

        #region Public Methods

        // Añadir sucesivamente todas las sesiones y sus configuraciones individuales
        public ISessionBuilder ConfigureSessions()
        {
            throw new NotImplementedException();
        }

        // Añadir las configuraciones generales
        public ISessionBuilder ConfigureTradingSession()//Func<TradingSessionConfiguration, TradingSession> configureActions)
        {
            throw new NotImplementedException();
        }

        public SessionBuilder AddSessionByType(SessionType type)
        {
            SessionDescriptor descriptor = SessionDescriptor.CreateTradingSessionByType(type,_instrumentProvider.Key);
            _descriptors.Add(descriptor);
            return this;
        }

        public SessionBuilder AddSessionCollectionByTypes(params SessionType[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
                AddSessionByType(type);

            return this;
        }

        public SessionBuilder AddSessionEnum<T>()
            where T : Enum
        {
            if (typeof(T).Name == nameof(SessionType))
            {
                EnumHelpers.ForEach<SessionType>((type) =>
                {
                    AddSessionByType(type);
                });
            }

            return this;
        }

        #endregion

        #region Private methods

        private void AddTypesToTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void SortTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void CreateTradingSession()
        {
            //throw new NotImplementedException();
        }

        private void CreateDefaultSessionProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
