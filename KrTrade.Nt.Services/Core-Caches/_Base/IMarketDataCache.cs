using NinjaTrader.Data;

namespace KrTrade.Nt.Services
{
    public interface IMarketDataCache
    {

        /// <summary>
        /// Add new element to cache using <see cref="MarketDataEventArgs"/> object.
        /// </summary>
        /// <param name="args"><see cref="MarketDataEventArgs"/> object, necesary for get the candidate value to be added to cache.</param>
        /// <returns><c>true</c> if the element has been added, otherwise <c>false</c>.</returns>
        bool Add(MarketDataEventArgs args);

        /// <summary>
        /// Update the current element of cache using <see cref="MarketDataEventArgs"/> object.
        /// </summary>
        /// <param name="args"><see cref="MarketDataEventArgs"/> object, necesary for update cache elements.</param>
        /// <returns><c>true</c> if the element has been updated, otherwise <c>false</c>.</returns>
        bool Update(MarketDataEventArgs args);

    }
}
