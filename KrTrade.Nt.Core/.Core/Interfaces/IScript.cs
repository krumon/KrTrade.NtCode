using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core
{
    public interface IBaseScript
    {
        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }
    }

    public interface IScript : IBaseScript, IHasName
    {
        /// <summary>
        /// Gets the type of the element
        /// </summary>
        ElementType Type { get; }
    }

    public interface IScript<TType> : IBaseScript, IHasName
        where TType : Enum
    {
        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        TType Type { get; }
    }

    //public interface IOptionsScript<TOptions> : IScript, IHasOptions<TOptions>
    //    where TOptions : IOptions
    //{
    //}
    public interface IOptionsScript<TType,TOptions> : IScript<TType>, IHasOptions<TOptions>
        where TOptions : IOptions
        where TType : Enum
    {
    }
    //public interface IInfoScript<TInfo> : IScript, IHasInfo<TInfo>
    //    where TInfo : IInfo
    //{
    //}
    public interface IInfoScript<TType,TInfo> : IScript<TType>, IHasInfo<TInfo>
        where TInfo : IInfo
        where TType: Enum
    {
    }
    //public interface IScript<TInfo, TOptions> : IScript, IHasInfo<TInfo>, IHasOptions<TOptions>
    //    where TInfo : IInfo
    //    where TOptions : IOptions
    //{
    //}
    public interface IScript<TType,TInfo, TOptions> : IInfoScript<TType,TInfo>, IOptionsScript<TType,TOptions>
        where TType : Enum
        where TInfo : IInfo
        where TOptions : IOptions
    {
    }

}
