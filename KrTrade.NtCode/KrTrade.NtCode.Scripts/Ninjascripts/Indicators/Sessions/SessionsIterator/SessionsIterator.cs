using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.NinjatraderObjects;
using KrTrade.NtCode.Services;
using System;
using System.Collections.Generic;

namespace KrTrade.NtCode.Ninjascripts.Indicators
{

    /// <summary>
    /// Determinates all properties of the sessions configure by ninjatrader and user.
    /// indicates when the sessions configured changes.
    /// </summary>
    public class SessionsIterator : ISessionsIterator
    {

        #region Private members

        private readonly ISessions _sessionsIndicator;
        private readonly INinjaScriptBase _ninjascript;
        private readonly IGlobalsData _globalsData;
        private readonly ILogger _logger;

        private bool _configured;
        private DateTime _currentSessionEnd;
        private DateTime _sessionDateTmp;
        private readonly List<int> newSessionBarIdx = new List<int>();
        private SessionIterator _sessionIterator;
        private PartialHoliday _partialHoliday;
        private TimeZoneInfo _userTimeZoneInfo;
        private TimeZoneInfo _barsTimeZoneInfo;

        #endregion

        #region Public properties

        public DateTime ActualSessionBegin { get; set; }
        public DateTime ActualSessionEnd { get; protected set; }
        public int Count => newSessionBarIdx.Count;
        // TODO: Delete. Is not necesary in this moment...
        public bool IsSessionUpdated { get; protected set; }
        public bool IsConfigured { get; protected set; }
        //public TimeZoneInfo UserTimeZoneInfo { get; protected set; }
        //public TimeZoneInfo BarsTimeZoneInfo { get; protected set; }

        public bool? BarsTypeIsIntraday => _ninjascript.Instance.Bars?.BarsType.IsIntraday;
        public int CurrentBar { get; protected set; }
        public DateTime CurrentTime { get; protected set; }
        public bool? IsPartialHoliday => _partialHoliday != null;
        public bool? IsLateBegin => _partialHoliday?.IsLateBegin;
        public bool? IsEarlyEnd => _partialHoliday?.IsEarlyEnd;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIterator"/> default instance.
        /// </summary>
        /// <param name="sessionsIndicator">The sessions engine.</param>
        /// <param name="ninjascript"></param>
        /// <param name="globalsData">The global data necesary to create the service.</param>
        /// <param name="logger">Log service to register the trace.</param>
        public SessionsIterator(ISessions sessionsIndicator, INinjaScriptBase ninjascript, IGlobalsData globalsData, ILogger logger)
        {
            _sessionsIndicator = sessionsIndicator ?? throw new ArgumentNullException(nameof(sessionsIndicator));
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _globalsData = globalsData ?? throw new ArgumentNullException(nameof(globalsData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public methods

        public virtual void Configure() 
        {
            if (_ninjascript.Instance.State == State.SetDefaults)
            {
                //_configured = false;
                //IsConfigured = false;
                return;
            }
            if (_ninjascript.Instance.State == State.Configure || _ninjascript.Instance.State == State.DataLoaded)
            {
                if (!_configured && _ninjascript.Instance.State == State.Configure)
                {
                    _currentSessionEnd = _globalsData.MinDate;
                    _sessionDateTmp = _globalsData.MinDate;
                    _userTimeZoneInfo = _globalsData.UserConfigureTimeZoneInfo;
                    ActualSessionBegin = _globalsData.MinDate;
                    ActualSessionEnd = _globalsData.MinDate;
                    _configured = true;
                }
                if (_configured && _ninjascript.Instance.State == State.DataLoaded)
                {
                    try
                    {
                        _sessionIterator = new SessionIterator(_ninjascript.Instance.Bars);
                        _barsTimeZoneInfo = _ninjascript.Instance.Bars.TradingHours.TimeZoneInfo;
                        IsConfigured = _configured;
                    }
                    catch
                    {
                        _logger.LogError("SessionsIterator cannot be configure when the data is loaded.");
                    }
                }
            }
            else if (_ninjascript.Instance.State == State.Historical)
            {
                if (IsConfigured)
                    _logger.LogInformation("SessionsIterator has been configured successfully.");
                else
                    _logger.LogInformation("SessionsIterator has NOT been configured successfully.");
            }
        }

        public void OnBarUpdate()
        {
            CurrentBar = _ninjascript.Instance.CurrentBar;
            CurrentTime = _ninjascript.Instance.Time[0];

            LastBarUpdate();
        }
        public virtual void OnMarketData() => OnBarUpdate();
        public virtual void OnSessionChanged(SessionChangedEventArgs args) { }
        public string Print()
        {
            string holidayText;
            if (IsPartialHoliday == true)
            {
                holidayText = "Partial Holiday. ";
                if (IsLateBegin == true)
                    holidayText += "Late Begin.";
                if (IsEarlyEnd == true)
                    holidayText += "Early End";
            }
            else
                holidayText = "Regular Session";

            return $"Session {Count}: Begin: {ActualSessionBegin.ToShortDateString()} End: {ActualSessionEnd.ToShortDateString()} | {holidayText}";
        }
        public override string ToString()
        {
            return $"SessionsIterator[{_ninjascript.Instance.Instrument.MasterInstrument.Name}] Period: {_ninjascript.Instance.Bars.BarsPeriod.BarsPeriodType} Value: {_ninjascript.Instance.Bars.BarsPeriod.Value}";
        }

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event SessionChangedEventHandler SessionChanged = (e) => { };

        #endregion

        #region Private methods

        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(CurrentTime);

            if (lastBarTimeStamp != _currentSessionEnd)
            {
                // Update the current value
                _currentSessionEnd = lastBarTimeStamp;
                // The new session begin
                IsSessionUpdated = true;
                // Changes the new session property in session manager.
                _sessionsIndicator.IsNewSession = true;
                // Check if it's a partial holiday
                TryGetPartialHoliday();
                // Create the session changed arguments
                var args = new SessionChangedEventArgs(CurrentBar, Count, ActualSessionBegin, ActualSessionEnd, _userTimeZoneInfo, IsPartialHoliday, IsLateBegin, IsEarlyEnd);
                // Call the parent
                OnSessionChanged(args);
                // Call to session manager
                _sessionsIndicator.OnSessionChanged(args);
                // Call to listeners
                SessionChanged?.Invoke(args);
            }            
        }

        private DateTime GetLastBarSessionDate(DateTime time)
        {
            // Make sure the session is terminated
            if (time <= ActualSessionEnd)
                return _sessionDateTmp;
            
            // Make sure the bars type are intraday
            if (BarsTypeIsIntraday == false)
                return _sessionDateTmp;

            // Get and update the next session values
            GetAndUpdateNextSessionValues(time);
            
            // Converts the start and end time to bar's configured time zone.
            _sessionDateTmp = TimeZoneInfo.ConvertTime(ActualSessionEnd.AddSeconds(-1), _userTimeZoneInfo, _barsTimeZoneInfo);

            // Store the bar index of the session.
            if (newSessionBarIdx.Count == 0 ||
                newSessionBarIdx.Count > 0 && CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
                newSessionBarIdx.Add(CurrentBar);

            return _sessionDateTmp;
        }
        private void GetAndUpdateNextSessionValues(DateTime time, bool includeEndTimeStamp = true)
        {
            // Get the next session value.
            _sessionIterator.GetNextSession(time, includeEndTimeStamp);
            // Update the next session value.
            ActualSessionBegin = _sessionIterator.ActualSessionBegin;
            ActualSessionEnd = _sessionIterator.ActualSessionEnd;
        }
        private bool? TryGetPartialHoliday() => _ninjascript.Instance.Bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd.Date, out _partialHoliday);

        #endregion

    }
}
