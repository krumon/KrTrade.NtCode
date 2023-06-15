using System;

namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class SessionsFilters : ISessionsFilters
    {
        private readonly ISessionsIterator _sessionsIterator;
        private readonly SessionsFiltersOptions _options = new SessionsFiltersOptions(); // Default options
        public bool IsEnabled { get;  private set; }

        public SessionsFilters(ISessionsIterator sessionsIterator)
        {
            _sessionsIterator = sessionsIterator ?? throw new ArgumentNullException(nameof(sessionsIterator));
        }

        //public SessionsFilters(ISessionsIterator sessionsIterator, IConfigureOptions<SessionsFiltersOptions> options)
        //{
        //    if (options == null)
        //        throw new ArgumentNullException(nameof(options));

        //    _sessionsIterator = sessionsIterator ?? throw new ArgumentNullException(nameof(sessionsIterator));
        //    options?.Configure(_options);
        //}

        public SessionsFiltersOptions Options => _options;

        public bool Check()
        {
            Console.WriteLine("The filters is checking....");
            bool check = true;

            if (_sessionsIterator.IsPartialHoliday == true && _options.IncludePartialHolidays == false)
                check = false;

            return check;
        }

        public void OnSessionUpdate()
        {
            IsEnabled = Check();
        }
    }
}
