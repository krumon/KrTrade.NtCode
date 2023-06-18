using System;

namespace KrTrade.Nt.Console
{
    /// <summary>
    /// Represents the <see cref="SessionStats"/> builder.
    /// </summary>
    public class SessionStatsBuilder : BaseSessionBuilder<SessionStats, SessionStatsConfiguration,SessionStatsBuilder>, ISessionStatsBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionStatsBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public SessionStatsBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionStatsOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionStatsBuilder Configure(Action<SessionStatsOptions> options) =>
        //    (SessionStatsBuilder)Configure<SessionStats, SessionStatsOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionStatsBuilder Configure(SessionStatsOptions options) =>
        //    (SessionStatsBuilder)Configure<SessionStats, SessionStatsOptions>(options);

        #endregion


    }
}
