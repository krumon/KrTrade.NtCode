using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService : IBarUpdateService<NinjascriptServiceInfo>
    {
    }
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService<TInfo> : IBarUpdateService<TInfo>
        where TInfo : IElementInfo, new()
    {
    }
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService<TInfo,TOptions> : IIndicatorService<TInfo>
        where TInfo : IElementInfo, new()
        where TOptions : IndicatorOptions, new()
    {
    }
}
