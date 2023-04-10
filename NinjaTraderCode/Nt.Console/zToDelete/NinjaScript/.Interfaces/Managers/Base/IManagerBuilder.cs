using System;

namespace ConsoleApp
{

    /// <summary>
    /// Interface for any manager builder.
    /// </summary>
    public interface IManagerBuilder : IBuilder
    {

        /// <summary>
        /// Adds one <see cref="INinjascript"/> object to the ninjascripts collection.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object to add.</typeparam>
        /// <typeparam name="Options">The <see cref="IConfiguration"/> to configure the script.</typeparam>
        /// <typeparam name="Builder">The <see cref="IBuilder"/> to construct the script.</typeparam>
        /// <param name="options">The specific configuration to add.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        IManagerBuilder Add<Script, Options, Builder>(Action<Options> options)
            where Script : BaseNinjascript<Script, Options, Builder>, INinjascript
            where Options : BaseConfiguration<Options>, IConfiguration
            where Builder : BaseBuilder<Script, Options, Builder>, IBuilder;

    }

}
