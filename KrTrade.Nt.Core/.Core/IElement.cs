using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core
{
    public interface IElement : IHasInfo, IHasName, IHasKey<IElement>
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        ElementType Type { get; }
    }
    public interface IElement<TInfo> : IElement, IHasInfo<TInfo> 
        where TInfo : IInfo
    {
        new TInfo Info { get; }
    }
    public interface IElement<TInfo,TOptions> : IElement<TInfo>, IHasOptions<TOptions>
        where TInfo : IInfo
        where TOptions : IOptions
    {
        new TOptions Options { get; }
    }
}
