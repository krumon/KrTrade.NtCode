using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IIndicatorService"/> objects. 
    /// </summary>
    public interface IIndicatorsBuilder : IBarUpdateBuilder<IIndicatorService,IndicatorsOptions,IIndicatorsBuilder>
    {
        //IIndicatorsBuilder Add<TIndicator,TOptions>(Action<TOptions> configureDelegate, ) where TIndicator : IBarUpdateCache;
    }
}
