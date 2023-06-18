using System;

namespace KrTrade.Nt.Console
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerConfiguration, TManagerBuilder> : BaseBuilder<TManagerScript, TManagerConfiguration, TManagerBuilder>, IManagerBuilder
        where TManagerScript : BaseManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IManager
        where TManagerConfiguration : BaseManagerConfiguration<TManagerConfiguration>, IManagerConfiguration
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseManagerBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseManagerBuilder(IManager script) : base(script)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds one <see cref="INinjascript"/> object to the ninjascripts collection.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object to add object.</typeparam>
        /// <typeparam name="Options">The <see cref="INinjascript"/> configuration object.</typeparam>
        /// <param name="options">The specific configuration to add.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder Add<Script, Options, Builder>(Action<Options> options)
            where Script : BaseNinjascript<Script, Options, Builder>, INinjascript
            where Options : BaseConfiguration<Options>, IConfiguration
            where Builder : BaseBuilder<Script,Options,Builder>, IBuilder
        {
            Script script = 
                (Script)BaseNinjascript<Script, Options, Builder>
                .CreateDefaultBuilder()
                .Configure<Script, Options>(options)
                .Build();

            this.script.Add(script);
            
            return this;

        }

        #endregion

    }
}
