using NinjaTrader.NinjaScript;
using System;

namespace ConsoleApp
{
    /// <summary>
    /// Represents the <see cref="SessionFilters"/> configuration.
    /// </summary>
    public class SessionFiltersConfiguration : BaseSessionConfiguration<SessionFiltersConfiguration>, ISessionFiltersConfiguration
    {

        #region Default values

        /// <summary>
        /// Indicates if the ninjascript enter in historial data bars.
        /// </summary>
        private bool includeHistoricalData = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays.
        /// </summary>
        private bool includePartialHolidays = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays with late begin.
        /// </summary>
        private bool includeLateBegin = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays with early end.
        /// </summary>
        private bool includeEarlyEnd = true;

        /// <summary>
        /// Represents the minimum date to enter with the ninjascript.
        /// </summary>
        private DateTime initialDate = SessionFilters.MIN_DATE;

        /// <summary>
        /// Represents the maximum date to enter with the ninjascript.
        /// </summary>
        private DateTime finalDate = SessionFilters.MAX_DATE;

        /// <summary>
        /// Represents the year of the initial date.
        /// </summary>
        private int initialYear;

        /// <summary>
        /// Represents the month of the initial date.
        /// </summary>
        private int initialMonth;

        /// <summary>
        /// Represents the day of the initial date.
        /// </summary>
        private int initialDay;

        /// <summary>
        /// Represents the year of the final date.
        /// </summary>
        private int finalYear;

        /// <summary>
        /// Represents the month of the final date.
        /// </summary>
        private int finalMonth;

        /// <summary>
        /// Represents the day of the final date.
        /// </summary>
        private int finalDay;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets if the ninjascript enter in historial data bars.
        /// </summary>
        public bool IncludeHistoricalData {get {return includeHistoricalData;} set { includeHistoricalData = value; } }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays.
        /// </summary>
        public bool IncludePartialHolidays 
        { 
            get => includePartialHolidays; 
            set 
            { 
                // Make sure value changed
                if (includePartialHolidays == value)
                    return;

                //Update values
                includePartialHolidays = value; 

                if (!value)
                {
                    includeEarlyEnd = value;
                    includeLateBegin = value;
                }
            } 
        }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with late begin.
        /// </summary>
        public bool IncludeLateBegin 
        { 
            get => includeLateBegin; 
            set 
            {
                // Make sure value changed
                if (includeLateBegin == value)
                    return;

                //Update values
                includeLateBegin = value;

                if (value)
                    includePartialHolidays = value;
            } 
        }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with early end.
        /// </summary>
        public bool IncludeEarlyEnd
        {
            get => includeEarlyEnd;
            set
            {
                // Make sure value changed
                if (includeEarlyEnd == value)
                    return;

                //Update values
                includeEarlyEnd = value;

                if (value)
                    includePartialHolidays = value;
            }
        }

        /// <summary>
        /// Gets the minimum date to enter with the ninjascript.
        /// </summary>
        public DateTime InitialDate { get { return initialDate; } set { initialDate = value; } }

        /// <summary>
        /// Gets the maximum date to enter with the ninjascript.
        /// </summary>
        public DateTime FinalDate { get { return finalDate; } set { finalDate = value; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to add the date filters.
        /// </summary>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(DateTime initialDate, DateTime finalDate)
        {
            if(initialDate < finalDate)
            {
                this.initialDate = initialDate;
                this.finalDate = finalDate;
            }

            if (initialDate < SessionFilters.MIN_DATE)
                this.initialDate = SessionFilters.MIN_DATE;

            if (finalDate > SessionFilters.MAX_DATE)
                this.finalDate = SessionFilters.MAX_DATE;

            return this;

        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="initialDay"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <param name="finalDay"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int initialYear, 
            int initialMonth, 
            int initialDay, 
            int finalYear, 
            int finalMonth, 
            int finalDay)
        {

            CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, finalYear, finalMonth, finalDay);
            CheckDayOfMonth(initialYear, initialMonth, finalYear,finalMonth);

            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year, 
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month, 
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ), 
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year, 
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month, 
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int year, 
            int month, 
            int day,
            bool isInitial = false
            )
        {
            if (isInitial)
            {
                CheckAndUpdateDateValues(year, month, day, finalYear, finalMonth, finalDay);
                CheckDayOfMonth(year, month, finalYear,finalMonth);
            }
            else
            {
                CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, year, month, day);
                CheckDayOfMonth(initialYear, initialMonth, year, month);
            }


            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isInitial"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int year, 
            int month, 
            bool isInitial = false
            )
        {
            if (isInitial)
            {
                CheckAndUpdateDateValues(year, month, initialDay, finalYear, finalMonth, finalDay);
                CheckDayOfMonth(year, month, finalYear,finalMonth);
            }
            else
            {
                CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, year, month, finalDay);
                CheckDayOfMonth(initialYear, initialMonth, year, month);
            }


            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="isInitial"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int year, 
            bool isInitial = false
            )
        {
            if (isInitial)
            {
                CheckAndUpdateDateValues(year, initialMonth, initialDay, finalYear, finalMonth, finalDay);
                CheckDayOfMonth(year, initialMonth, finalYear,finalMonth);
            }
            else
            {
                CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, year, finalMonth, finalDay);
                CheckDayOfMonth(initialYear, initialMonth, year, finalMonth);
            }


            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="initialDay"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <param name="finalDay"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int finalYear,
            int finalMonth,
            int finalDay
            )
        {

            CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, finalYear, finalMonth, finalDay);
            CheckDayOfMonth(initialYear, initialMonth, finalYear, finalMonth);

            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Method to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int initialYear, 
            int initialMonth, 
            int finalYear, 
            int finalMonth)
        {
            CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, finalYear, finalMonth, finalDay);
            CheckDayOfMonth(initialYear, initialMonth, finalYear, finalMonth);

            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        /// <summary>
        /// Method to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="finalYear"></param>
        /// <returns></returns>
        public SessionFiltersConfiguration AddDateFilters(
            int initialYear,
            int finalYear)
        {

            CheckAndUpdateDateValues(initialYear, initialMonth, initialDay, finalYear, finalMonth, finalDay);
            CheckDayOfMonth(initialYear, initialMonth, finalYear, finalMonth);

            AddDateFilters(
                new DateTime(
                    initialYear != 0 ? initialYear : SessionFilters.MIN_DATE.Year,
                    initialMonth != 0 ? initialMonth : SessionFilters.MIN_DATE.Month,
                    initialDay != 0 ? initialDay : SessionFilters.MIN_DATE.Day
                    ),
                new DateTime(
                    finalYear != 0 ? finalYear : SessionFilters.MAX_DATE.Year,
                    finalMonth != 0 ? finalMonth : SessionFilters.MAX_DATE.Month,
                    finalDay != 0 ? finalDay : SessionFilters.MAX_DATE.Day
                    ));

            return this;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Check and update the values.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="initialDay"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <param name="finalDay"></param>
        private void CheckAndUpdateDateValues(
            int initialYear,int initialMonth, int initialDay, 
            int finalYear, int finalMonth, int finalDay)
        {
            if (CheckYear(initialYear))
                this.initialYear = initialYear;

            if (CheckMonth(initialMonth))
                this.initialMonth = initialMonth;

            if (CheckDay(initialDay))
                this.initialDay = initialDay;

            if (CheckYear(finalYear))
                this.finalYear = finalYear;

            if (CheckMonth(finalMonth))
                this.finalMonth = finalMonth;

            if (CheckDay(finalDay))
                this.finalDay = finalDay;

        }

        /// <summary>
        /// Checks if the year is betwen min and max  script years.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private bool CheckYear(int year)
        {
            return (year >= SessionFilters.MIN_DATE.Year && year <= SessionFilters.MAX_DATE.Year);
        }

        /// <summary>
        /// Checks if the month value is valid.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private bool CheckMonth(int month)
        {
            return (month >= 1 && month <= 12);
        }

        /// <summary>
        /// Checks if the day value is valid.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private bool CheckDay(int day)
        {
            return (day >= 1 && day <= 31);
        }

        /// <summary>
        /// Checks if the day value is valid.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        private void CheckDayOfMonth(int initialYear, int initialMonth, int finalYear, int finalMonth)
        {
            if (initialDay > 28)
                initialDay = DateTime.DaysInMonth(initialYear, initialMonth);
            if (finalDay > 28)
                finalDay = DateTime.DaysInMonth(finalYear, finalMonth);
        }

        #endregion

    }
}
