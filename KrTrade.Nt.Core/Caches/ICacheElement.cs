using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Caches
{
    public interface ICacheElement<T>
    {
        /// <summary>
        /// Reset <see cref="ICacheElement{T}"/> with default values. The default double values are 0, for the time is 'DateTime.MinValue' and for 'Idx' is -1.
        /// </summary>
        void Reset();

        /// <summary>
        /// Sets <see cref="ICacheElement{T}"/> values with specified 'NinjaScript' values.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript instance.</param>
        /// <param name="barsAgo">The displacement of the <see cref="ICacheElement{T}"/> respect 'NinjaScript' series.</param>
        /// <param name="barsInProgress">The 'NinjaScript' bars series where the values will be updated.</param>
        void Set(NinjaScriptBase ninjascript, int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Set the bar values when market data success.
        /// </summary>
        void Set(MarketDataEventArgs args);

        /// <summary>
        /// Copy all values to <paramref name="bar"/> object.
        /// </summary>
        /// <param name="target">The <see cref="ICacheElement{T}"/> target object.</param>
        void CopyTo(T target);

        /// <summary>
        /// Gets <see cref="Bar"/> instance with the same values of the current instance.
        /// </summary>
        /// <returns><see cref="{T}"/> element.</returns>
        T Get();

    }
}
