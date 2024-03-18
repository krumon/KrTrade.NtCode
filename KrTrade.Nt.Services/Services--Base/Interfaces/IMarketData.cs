namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketData
    {
        /// <summary>
        /// Updates the service when market data changed.
        /// </summary>
        void MarketData();

        /// <summary>
        /// Method to be executed to update the service when a <see cref="IBarsService"/> market data is updated.
        /// </summary>
        /// <param name="updatedBarsSeries"><see cref="IBarsService"/> updated.</param>
        void MarketData(IBarsService updatedBarsSeries);
    }
}
