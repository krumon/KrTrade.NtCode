using System;

namespace KrTrade.NtCode
{
    /// <summary>
    /// The base class to session scripts builders
    /// </summary>
    public abstract class BaseSessionBuilder<TSession,TSessionConfiguration,TSessionBuilder> : BaseBuilder<TSession,TSessionConfiguration,TSessionBuilder>, ISessionBuilder
        where TSession : BaseSession<TSession,TSessionConfiguration,TSessionBuilder>, ISession
        where TSessionConfiguration : BaseSessionConfiguration<TSessionConfiguration>, ISessionConfiguration
        where TSessionBuilder : BaseSessionBuilder<TSession, TSessionConfiguration, TSessionBuilder>, ISessionBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseSessionBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseSessionBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

    }

}
