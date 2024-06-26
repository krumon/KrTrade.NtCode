using KrTrade.Nt.Core.Information;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Options;
using System;

namespace KrTrade.Nt.Core
{
    public interface IElement : IHasKey<IElement>, IHasName, IConfigure, IDataLoaded, ITerminated
    {
        IPrintService PrintService { get; }
    }
    public interface IElement<TType> : IElement, IScript<TType>
        where TType: Enum
    {
    }
    public interface IInfoElement<TType,TInfo> : IElement<TType>, IInfoScript<TType, TInfo>
        where TInfo : IInfo<TType>
        where TType : Enum
    {
    }
    public interface IOptionsElement<TType,TOptions> : IElement<TType>, IOptionsScript<TType, TOptions>
        where TOptions : IOptions
        where TType : Enum
    {
        /// <summary>
        /// Indicates if the element is enabled.
        /// </summary>
        bool IsEnable { get; }
    }
    public interface IElement<TType,TInfo,TOptions> : IInfoElement<TType,TInfo>, IOptionsElement<TType,TOptions>, IScript<TType,TInfo,TOptions>
        where TInfo : IInfo<TType>
        where TOptions : IOptions
        where TType : Enum
    {
    }
}
