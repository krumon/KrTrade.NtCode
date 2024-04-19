namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService : IBarUpdateService
    {
    }
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService<TInfo> : IBarUpdateService<TInfo>
        where TInfo : BarUpdateServiceInfo, new()
    {
    }
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator service.
    /// </summary>
    public interface IIndicatorService<TInfo,TOptions> : IIndicatorService<TInfo>
        where TInfo : BarUpdateServiceInfo, new()
        where TOptions : IndicatorOptions, new()
    {
    }
}
