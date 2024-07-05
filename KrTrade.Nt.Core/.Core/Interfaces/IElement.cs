using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Options;
using System;

namespace KrTrade.Nt.Core
{
    public interface IElement : IHasKey<IElement>, IHasName, IConfigure, IDataLoaded, ITerminated
    {
        /// <summary>
        /// The service to log any value in the 'NinjaScript.OutputWindow'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Indicates if the element is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Indicates if the object logger is enable.
        /// </summary>
        bool IsLogEnable { get; }

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
    }
    public interface IElement<TType,TInfo,TOptions> : IInfoElement<TType,TInfo>, IOptionsElement<TType,TOptions>, IScript<TType,TInfo,TOptions>
        where TInfo : IElementInfo<TType>
        where TOptions : IOptions
        where TType : Enum
    {
    }
}
