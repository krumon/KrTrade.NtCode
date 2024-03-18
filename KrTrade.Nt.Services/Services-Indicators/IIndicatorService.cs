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
    public interface IIndicatorService<TOptions> : IIndicatorService, IBarUpdateService<TOptions>
        where TOptions : IndicatorOptions, new()
    {
    }
}
