using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Caches
{
    public interface ICache
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
        int LengthOfRemovedValuesCache { get; set; }

    }
    public interface ICache<T> : ICache, ISeries<T>
    {

        /// <summary>
        /// Indicates if <see cref="ICache{T}"/> is full.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// Remove the current element and add the last element removed.
        /// This method can be executed only one time.
        /// </summary>
        void RemoveLastElement();

        /// <summary>
        /// Reset the <see cref="ICache{T}"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        /// <summary>
        /// Dispose the <see cref="ICache{T}"/>. 
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Gets The element that is at the specified index.
        /// </summary>
        /// <param name="valuesAgo">The index of the element in the <see cref="ICacheElement{T}"/>.</param>
        /// <returns>The element that is at the specified index.</returns>
        T GetValue(int valuesAgo);

        /// <summary>
        /// Returns <paramref name="numOfValues"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="fromValuesAgo">The values ago where started to construct the array.</param>
        /// <param name="numOfValues">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        T[] ToArray(int fromValuesAgo, int numOfValues);

    }
}
