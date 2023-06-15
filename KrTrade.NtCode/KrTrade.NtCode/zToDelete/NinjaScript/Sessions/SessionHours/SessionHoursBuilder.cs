using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents the <see cref="SessionHours"/> builder.
    /// </summary>
    public class SessionHoursBuilder : BaseSessionBuilder<SessionHours, SessionHoursConfiguration,SessionHoursBuilder>, ISessionHoursBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public SessionHoursBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionHoursOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionHoursBuilder Configure(Action<SessionHoursOptions> options) =>
        //    (SessionHoursBuilder)Configure<SessionHours, SessionHoursOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionHoursBuilder Configure(SessionHoursOptions options) =>
        //    (SessionHoursBuilder)Configure<SessionHours, SessionHoursOptions>(options);

        #endregion

    }
}
