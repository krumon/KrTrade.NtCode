using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;

namespace KrTrade.Nt.Core
{
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketData
    {
        /// <summary>
        /// Updates the service when market data changed.
        /// </summary>
        /// <param name="args">The arguments of the 'MarketData' event.</param>
        void MarketData(MarketDataEventArgs args);

        /// <summary>
        /// Method to be executed to update the service when a <see cref="IBarsService"/> market data is updated.
        /// </summary>
        /// <param name="updatedBarsService">The <see cref="IBarsService"/> updated.</param>
        void MarketData(IBarsService updatedBarsService);
    }
}
