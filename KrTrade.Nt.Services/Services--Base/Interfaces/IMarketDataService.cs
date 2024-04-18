namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketDataService : INinjascriptService, IMarketData
    {
    }
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketDataService<TInfo> : IMarketDataService
        where TInfo : MarketDataServiceInfo
    {
    }
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketDataService<TInfo,TOptions> : IMarketDataService<TInfo>
        where TInfo : MarketDataServiceInfo
        where TOptions : MarketDataServiceOptions
    {
    }
}
