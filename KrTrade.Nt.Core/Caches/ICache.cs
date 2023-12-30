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
        /// Gets the current cache value.
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Add new element to <see cref="ICache{T}"/>.
        /// </summary>
        void Add();

        /// <summary>
        /// Update the current element of <see cref="ICache{T}"/>.
        /// </summary>
        void Update();

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
        /// Sets the candidate value to be added to <see cref="ICache{T}"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        void SetCandidateValue(NinjaScriptBase ninjascript = null);

    }
}
