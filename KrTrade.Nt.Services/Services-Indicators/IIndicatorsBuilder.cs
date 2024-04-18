using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IIndicatorService"/> objects. 
    /// </summary>
    public interface IIndicatorsBuilder : INinjascriptServiceBuilder<IIndicatorService,NinjascriptServiceInfo,IndicatorCollectionOptions,IIndicatorsBuilder>
    {
        //IIndicatorsBuilder Add<TIndicator,TOptions>(Action<TOptions> configureDelegate, ) where TIndicator : IBarUpdateCache;
    }
}
