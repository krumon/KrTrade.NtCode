using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Caches
{
    public interface ICache<T> : ISeries<T>
    {
        /// <summary>
        /// Gets <see cref="ICache{T}"/> capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets <see cref="ICache{T}"/> period.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Gets the displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/>.
        /// </summary>
        int Displacement { get; }

        /// <summary>
        /// Gets the number of elements to store in cache, before will be removed forever.
        /// </summary>
        int RemovedValuesCacheLength { get; set; }

        /// <summary>
        /// Indicates if <see cref="ICache{T}"/> is full.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        T CurrentValue { get; }

        ///// <summary>
        ///// Gets the last removed value in cache.
        ///// </summary>
        //T RemovedValue { get; }

        /// <summary>
        /// Add new element to <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="ninjascript">'Ninjatrader.NinjaScript' used to get the candidate value to be added to cache.</param>
        /// <returns>True if the element has been added, otherwise <c></c>.</returns>
        bool Add(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Add new element to <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="marketDataEventArgs">'Ninjatrader.MarketDataEventArgs' used to get the candidate value to be added to cache.</param>
        /// <returns>True if the element has been added, otherwise <c></c>.</returns>
        bool Add(MarketDataEventArgs marketDataEventArgs);

        /// <summary>
        /// Update the current element of <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="ninjascript">'Ninjatrader.NinjaScript' used to get the candidate value to be added to cache.</param>
        /// <returns><c>true</c> if the element has been updated, otherwise <c>false</c>.</returns>
        bool Update(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Update the current element of <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="marketDataEventArgs">'Ninjatrader.MarketDataEventArgs' used to get the candidate value to be added to cache.</param>
        /// <returns><c>true</c> if the element has been updated, otherwise <c>false</c>.</returns>
        bool Update(MarketDataEventArgs marketDataEventArgs);

        /// <summary>
        /// Remove the current element and add the last element removed.
        /// This method can be executed only one time.
        /// </summary>
        void ReDo();

        /// <summary>
        /// Reset the <see cref="ICache{T}"/>.
        /// </summary>
        void Reset();

        /// <summary>
        /// Release the <see cref="ICache{T}"/>.
        /// </summary>
        void Release();

        /// <summary>
        /// Returns <paramref name="numberOfElements"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="initialIdx">The initial index.</param>
        /// <param name="numberOfElements">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        T[] GetValues(int initialIdx, int numberOfElements);

    }
}
