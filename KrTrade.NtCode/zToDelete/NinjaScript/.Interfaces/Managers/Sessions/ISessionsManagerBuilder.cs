using System;

namespace KrTrade.NtCode
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManagerBuilder : IManagerBuilder
    {

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="Action{SessionsManagerOptions}"/> delegate.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        SessionsManagerBuilder Configure(Action<SessionsManagerConfiguration> options);

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="SessionFiltersConfiguration"/> object.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        SessionsManagerBuilder Configure(SessionsManagerConfiguration options);

        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionFilters(Action<SessionFiltersConfiguration> options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionHours(Action<SessionHoursConfiguration> options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionHoursList(Action<SessionHoursListConfiguration> options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionStats(Action<SessionStatsConfiguration> options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionsIterator(Action<SessionsIteratorConfiguration> options);

    }

}
