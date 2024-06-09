using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Elements
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
    public interface IElement<TInfo> : IElement, IHasInfo<TInfo> where TInfo : IInfo
    {
    }
}
