using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using KrTrade.NtCode.Events;
using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents the filters for any session object.
    /// </summary>
    public class SessionFilters : BaseSession<SessionFilters,SessionFiltersConfiguration,SessionFiltersBuilder>, ISessionFilters
    {

        #region static consts

        /// <summary>
        /// The minimum date for the filters.
        /// </summary>
        public readonly static DateTime MIN_DATE = new DateTime(1970, 1, 1);

        /// <summary>
        /// The maximum date for the filters.
        /// </summary>
        public readonly static DateTime MAX_DATE = new DateTime(2050, 12, 31);

        #endregion

        #region Private members

        /// <summary>
        /// The partial partialHoliday object.
        /// </summary>
        //private PartialHoliday partialHoliday;

        /// <summary>
        /// The current date and time.
        /// </summary>
        private DateTime currentDateTime = DateTime.Now;

        /// <summary>
        /// Represents the datetime when the object is initialized.
        /// </summary>
        private DateTime startTime = DateTime.Now;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionFilters"/> is sessionHoursListIsConfigured.
        /// </summary>
        public bool configured;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets if the session is a partial partialHoliday.
        /// </summary>
        public bool? IsPartialHoliday { get; private set; }

        /// <summary>
        /// Indicates if the partial partialHoliday is late begin.
        /// </summary>
        public bool? IsLateBegin { get; private set; }

        /// <summary>
        /// Indicates if the partial partialHoliday is early end.
        /// </summary>
        public bool? IsEarlyEnd { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionFilters"/> default instance.
        /// </summary>
        public SessionFilters() : base()
        {
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Creates the <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.</returns>
        public ISessionFiltersBuilder CreateSessionFiltersBuilder() => CreateBuilder<SessionFilters, SessionFiltersBuilder>();

        #endregion

        #region State changed methods

        /// <summary>
        /// Loaded <see cref="SessionFilters"/> in "OnStateChanged" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method
            base.Load(ninjascript, bars);

            // Save now time for the historical data
            startTime = DateTime.Now;

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
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            // Store the current date time
            currentDateTime = ninjascript.Time[0];
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            // Store the current date time
            currentDateTime = ninjascript.Time[0];
        }

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnSessionChanged(SessionUpdateArgs e)
        {
            IsPartialHoliday = e.IsPartialHoliday;
            IsEarlyEnd = e.IsEarlyEnd;
            IsLateBegin = e.IsLateBegin;
        }

        ///// <summary>
        ///// Changed any object or property when the session changed.
        ///// </summary>
        ///// <param name="e"></param>
        //public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
        //{
        //    // if date is a partial hoilday...store the partial partialHoliday
        //    //if (!(bars.TradingHours.PartialHolidays.TryGetValue(ninjascript.Time[0], out partialHoliday)))
        //    //    partialHoliday = null;
        //}

        #endregion

        #region Public methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated.
        /// Evaluated if the ninjascript pass the filter conditions.
        /// </summary>
        /// <returns>True if the ninjascript pass the filter conditions.</returns>
        public bool CanEntry()
        {
            return Check();
        }

        /// <summary>
        /// Event driven method which is called whenever a bar is updated.
        /// Evaluated if the ninjascript pass the filter conditions.
        /// </summary>
        /// <returns>True if the ninjascript pass the filter conditions.</returns>
        public bool OnBarUpdateAndCheckFilters()
        {
            OnBarUpdate();

            return Check();
        }


        #endregion

        #region Protected methods

        /// <summary>
        /// Check the filters.
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            bool historicalDataFilter   =   configuration.IncludeHistoricalData || currentDateTime > startTime;
            bool dateFilter             =   configuration.InitialDate <= currentDateTime && configuration.FinalDate >= currentDateTime;
            bool holidaysFilter         =   configuration.IncludePartialHolidays == IsPartialHoliday && configuration.IncludeEarlyEnd == IsEarlyEnd && configuration.IncludeLateBegin == IsLateBegin;
            bool dayOfWeekFilters       =   true;
            bool monthOfYearFilters     =   true;

            return
                historicalDataFilter &&
                dateFilter &&
                holidaysFilter &&
                monthOfYearFilters &&
                dayOfWeekFilters
                ;
        }

        #endregion

    }
}
