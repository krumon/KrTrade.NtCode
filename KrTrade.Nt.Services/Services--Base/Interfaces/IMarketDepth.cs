using NinjaTrader.Data;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the market data changed.
    /// </summary>
    public interface IMarketDepth
    {
        /// <summary>
        /// Updates the service when market depth changed.
        /// </summary>
        /// <param name="args">The arguments of the 'MarketDepth' event.</param>
        void MarketDepth(MarketDepthEventArgs args);

        /// <summary>
        /// Method to be executed to update the service when a <see cref="IBarsService"/> market depth is updated.
        /// </summary>
        /// <param name="updatedBarsService">The <see cref="IBarsService"/> updated.</param>
        void MarketDepth(IBarsService updatedBarsService);
    }
}
