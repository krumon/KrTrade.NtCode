using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// Represents the <see cref="SessionsIterator"/> builder.
    /// </summary>
    public class SessionsIteratorBuilder : BaseSessionBuilder<SessionsIterator, SessionsIteratorConfiguration,SessionsIteratorBuilder>, ISessionsIteratorBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIteratorBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public SessionsIteratorBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionsIteratorOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionsIteratorBuilder Configure(Action<SessionsIteratorOptions> options) =>
        //    (SessionsIteratorBuilder)Configure<SessionsIterator, SessionsIteratorOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionsIteratorBuilder Configure(SessionsIteratorOptions options) =>
        //    (SessionsIteratorBuilder)Configure<SessionsIterator, SessionsIteratorOptions>(options);

        #endregion


    }
}
