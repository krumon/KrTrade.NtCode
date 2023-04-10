using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace ConsoleApp
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseIndicatorsManager<TManagerScript, TManagerConfiguration,TManagerBuilder> : BaseManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IIndicatorsManager
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IIndicatorsManager
        where TManagerConfiguration : BaseIndicatorsManagerConfiguration<TManagerConfiguration>, IIndicatorsManagerConfiguration
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IIndicatorsManagerBuilder
    {
    }

}
