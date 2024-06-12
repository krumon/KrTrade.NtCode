using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core
{
    public interface IScript : IHasName
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

    public interface IScript<TOptions> : IScript, IHasOptions<TOptions>
    where TOptions : IOptions
    {
    }
    public interface IScript<TInfo, TOptions> : IScript, IHasInfo<TInfo>, IHasOptions<TOptions>
        where TInfo : IInfo
        where TOptions : IOptions
    {
    }

}
