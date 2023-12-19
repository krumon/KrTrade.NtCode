namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IOnMarketDataService
    {
        /// <summary>
        /// Updates the service when the market data changes.
        /// </summary>
        void OnMarketData();
    }
}
