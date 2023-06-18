using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.NtCode
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseStrategiesManagerBuilder<TManagerScript, TManagerConfiguration,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerConfiguration,TManagerBuilder>, IStrategiesManagerBuilder
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IStrategiesManager
        where TManagerConfiguration : BaseStrategiesManagerConfiguration<TManagerConfiguration>, IStrategiesManagerConfiguration
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IStrategiesManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseStrategiesManagerBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseStrategiesManagerBuilder(IManager script) : base(script)
        {
        }

        #endregion

    }

}
