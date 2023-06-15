using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents the <see cref="SessionHoursList"/> builder.
    /// </summary>
    public class SessionHoursListBuilder : BaseSessionBuilder<SessionHoursList, SessionHoursListConfiguration,SessionHoursListBuilder>, ISessionHoursListBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursListBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public SessionHoursListBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionHoursListOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionHoursListBuilder Configure(Action<SessionHoursListOptions> options) =>
        //    (SessionHoursListBuilder)Configure<SessionHoursList, SessionHoursListOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionHoursListBuilder Configure(SessionHoursListOptions options) =>
        //    (SessionHoursListBuilder)Configure<SessionHoursList, SessionHoursListOptions>(options);

        #endregion


    }
}
