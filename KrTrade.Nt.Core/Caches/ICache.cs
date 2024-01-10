using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Caches
{
    public interface ICache<T>
    {

        /// <summary>
        /// Gets <see cref="ICache{T}"/> capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets the displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/>.
        /// </summary>
        int Displacement { get; }

        /// <summary>
        /// Indicates if <see cref="ICache{T}"/> is full.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// Gets the current cache added value 'cache[0]'.
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Gets the last interested value in cache 'cache[Displacement]'.
        /// </summary>
        T LastValue { get; }

        /// <summary>
        /// Add new element to <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="ninjascript">'Ninjatrader.NinjaScript' used to get the candidate value to be added to cache.</param>
        void Add(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Add new element to <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="marketDataEventArgs">'Ninjatrader.MarketDataEventArgs' used to get the candidate value to be added to cache.</param>
        void Add(MarketDataEventArgs marketDataEventArgs);

        /// <summary>
        /// Update the current element of <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="ninjascript">'Ninjatrader.NinjaScript' used to get the candidate value to be added to cache.</param>
        void Update(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Update the current element of <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="marketDataEventArgs">'Ninjatrader.MarketDataEventArgs' used to get the candidate value to be added to cache.</param>
        void Update(MarketDataEventArgs marketDataEventArgs);

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

        ///// <summary>
        ///// Sets the candidate value to be added to <see cref="ICache{T}"/>.
        ///// </summary>
        ///// <param name="ninjascript"></param>
        //void SetCandidateValue(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Returns <see cref="{T}"/> at specified index.
        /// </summary>
        /// <param name="index">The specified index. 0 is the most recent value.</param>
        /// <returns>The <see cref="{T}"/> cache element.</returns>
        T GetValue(int index);

        /// <summary>
        /// Returns <paramref name="numberOfElements"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="initialIdx">The initial index.</param>
        /// <param name="numberOfElements">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        T[] GetValues(int initialIdx, int numberOfElements);

    }
}
