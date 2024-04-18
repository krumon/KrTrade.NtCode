namespace KrTrade.Nt.Services
{

    /// <summary>
    /// Defines methods that are necesary to execute when the market depth changed.
    /// </summary>
    public interface IMarketDepthService : INinjascriptService, IMarketData
    {
    }
    /// <summary>
    /// Defines methods that are necesary to execute when the market depth changed.
    /// </summary>
    public interface IMarketDepthService<TInfo> : IMarketDepthService
        where TInfo : MarketDepthServiceInfo
    {
    }
    /// <summary>
    /// Defines methods that are necesary to execute when the market depth changed.
    /// </summary>
    public interface IMarketDepthService<TInfo, TOptions> : IMarketDepthService<TInfo>
        where TInfo : MarketDepthServiceInfo
        where TOptions : MarketDepthServiceOptions
    {
    }

}
