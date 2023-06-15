using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// <see cref="SessionsManager"/> builder.
    /// </summary>
    public class SessionsManagerBuilder : BaseManagerBuilder<SessionsManager,SessionsManagerConfiguration,SessionsManagerBuilder>, ISessionsManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursBuilder"/> default instance.
        /// </summary>
        public SessionsManagerBuilder(SessionsManager script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionsManagerOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionsManagerBuilder Configure(Action<SessionsManagerOptions> options) =>
        //    (SessionsManagerBuilder)Configure<SessionsManager, SessionsManagerOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionsManagerBuilder Configure(SessionsManagerOptions options) =>
        //    (SessionsManagerBuilder)Configure<SessionsManager, SessionsManagerOptions>(options);

        #endregion

        #region Public methods

        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public ISessionsManagerBuilder AddSessionFilters(Action<SessionFiltersConfiguration> options)
            => (SessionsManagerBuilder)Add<SessionFilters, SessionFiltersConfiguration, SessionFiltersBuilder>(options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public ISessionsManagerBuilder AddSessionHours(Action<SessionHoursConfiguration> options)
            => (SessionsManagerBuilder)Add<SessionHours, SessionHoursConfiguration, SessionHoursBuilder>(options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public ISessionsManagerBuilder AddSessionHoursList(Action<SessionHoursListConfiguration> options)
            => (SessionsManagerBuilder)Add<SessionHoursList, SessionHoursListConfiguration, SessionHoursListBuilder>(options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public ISessionsManagerBuilder AddSessionStats(Action<SessionStatsConfiguration> options)
            => (SessionsManagerBuilder)Add<SessionStats, SessionStatsConfiguration, SessionStatsBuilder>(options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public ISessionsManagerBuilder AddSessionsIterator(Action<SessionsIteratorConfiguration> options)
            => (SessionsManagerBuilder)Add<SessionsIterator, SessionsIteratorConfiguration, SessionsIteratorBuilder>(options);

        #endregion

    }
}
