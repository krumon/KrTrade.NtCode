namespace KrTrade.Nt.Core
{
    public interface IElement : IScript, IHasInfo, IHasKey<IElement>, IConfigure, IDataLoaded, ITerminated
    {
        IPrintService PrintService { get; }
    }
    public interface IElement<TInfo> : IElement, IHasInfo<TInfo> 
        where TInfo : IInfo
    {
        new TInfo Info { get; }
    }
    public interface IElement<TInfo,TOptions> : IElement, IScript<TInfo,TOptions>
        where TInfo : IInfo
        where TOptions : IOptions
    {
        new TOptions Options { get; }
    }
}
