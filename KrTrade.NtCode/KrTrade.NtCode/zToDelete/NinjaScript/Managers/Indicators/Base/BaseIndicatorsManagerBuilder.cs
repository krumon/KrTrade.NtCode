using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseIndicatorsManagerBuilder<TManagerScript, TManagerConfiguration,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerConfiguration,TManagerBuilder>, IIndicatorsManagerBuilder
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerConfiguration, TManagerBuilder>, IIndicatorsManager
        where TManagerConfiguration : BaseIndicatorsManagerConfiguration<TManagerConfiguration>, IIndicatorsManagerConfiguration
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IIndicatorsManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseIndicatorsManagerBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseIndicatorsManagerBuilder(IManager script) : base(script)
        {
        }

        #endregion

    }

}
