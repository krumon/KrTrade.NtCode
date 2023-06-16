using NinjaTrader.NinjaScript;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.NinjatraderObjects;
using KrTrade.NtCode.Services;
using System;

namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Represents a <see cref="ISessions"/> service.
    /// </summary>
    public class Sessions : ISessions
    {
        private readonly INinjaScriptBase _ninjascript;
        private readonly IGlobalsData _globalsData;
        private readonly ILogger _logger;

        private ISessionsIterator _sessionsIterator;
        private ISessionsFilters _sessionsFilters;
        //private bool _isNewSession;
        
        public ISessionsIterator SessionsIterator => _sessionsIterator;
        public bool IsConfigured { get; internal set; }
        public bool IsNewSession { get; set; }

        public Sessions(INinjaScriptBase ninjascript, IGlobalsData globalsData, ILogger<Sessions> logger)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _globalsData = globalsData ?? throw new ArgumentNullException(nameof(globalsData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessionsIterator = new SessionsIterator(this, _ninjascript, _globalsData, _logger);
            _sessionsFilters = new SessionsFilters(_sessionsIterator);
        }

        internal Sessions()
        {
        }

        public void Configure() 
        {
            if (_ninjascript.State == State.SetDefaults)
                return;
            else
            {
                _sessionsIterator.Configure();

            }

            if (_sessionsIterator.IsConfigured)
                IsConfigured = true;
        }

        public virtual void OnBarUpdate()
        {
            _sessionsIterator.OnBarUpdate();
            //if (Filters.IsEnabled)
            //{

            //}

            // Reset the new session value when after the first bar update
            if (IsNewSession)
                IsNewSession = false;
        }

        public virtual void OnSessionChanged(SessionChangedEventArgs args)
        {
            _logger.LogInformation(_sessionsIterator.ToString());
        }

        public bool IsEnabled(Ninjascripts.NinjascriptLevel ninjascriptLevel)
        {
            throw new NotImplementedException();
        }

        public void Calculate()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
