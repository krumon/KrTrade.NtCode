using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core
{
    public interface IScript
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }
    }

    public interface IScript<TType> : IScript, IHasName
        where TType : Enum
    {
        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        TType Type { get; }
    }

    public interface IOptionsScript<TOptions> : IScript, IHasOptions<TOptions>
        where TOptions : IOptions
    {
    }
    public interface IInfoScript<TInfo> : IScript, IHasInfo<TInfo>
        where TInfo : IInfo
    {
    }
    public interface IInfoScript<TType,TInfo> : IScript<TType>, IHasInfo<TInfo>
        where TInfo : IInfo
        where TType: Enum
    {
    }
    public interface IScript<TInfo, TOptions> : IScript, IHasInfo<TInfo>, IHasOptions<TOptions>
        where TInfo : IInfo
        where TOptions : IOptions
    {
    }
    public interface IScript<TType,TInfo, TOptions> : IScript<TType>, IHasInfo<TInfo>, IHasOptions<TOptions>
        where TType : Enum
        where TInfo : IInfo
        where TOptions : IOptions
    {
    }

}
