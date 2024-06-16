using System;

namespace KrTrade.Nt.Core
{
    public interface IBaseElement
    {
        IPrintService PrintService { get; }
    }
    public interface IElement<TType> : IBaseElement, IInfoScript<TType,IInfo<TType>>, IHasInfo<IInfo<TType>>, IHasKey<IElement<TType>>, IConfigure, IDataLoaded, ITerminated
        where TType: Enum
    {
    }
    public interface IElement<TType,TInfo> : IBaseElement, IInfoScript<TType, TInfo>, IHasInfo<TInfo>, IHasKey<IElement<TType>>, IConfigure, IDataLoaded, ITerminated
        where TInfo : IInfo<TType>
        where TType : Enum
    {
    }
    public interface IElement<TType,TInfo,TOptions> : IBaseElement, IInfoScript<TType, TInfo>, IHasInfo<TInfo>, IHasKey<IElement<TType>>, IConfigure, IDataLoaded, ITerminated
        where TInfo : IInfo<TType>
        where TOptions : IOptions
        where TType : Enum
    {
    }
}
