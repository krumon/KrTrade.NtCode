namespace KrTrade.Nt.Core.Series
{
    public interface ICache
    {
        /// <summary>
        /// Gets <see cref="ICache"/> capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets the number of elements to store in cache, before will be removed forever.
        /// </summary>
        int OldValuesCapacity { get; }

        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Indicates if <see cref="ICache"/> is full.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// Remove the current element and add the last element removed.
        /// This method can be executed only one time.
        /// </summary>
        void RemoveLastElement();

        /// <summary>
        /// Reset the <see cref="ICache"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        /// <summary>
        /// Dispose the <see cref="ICache"/>. 
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        object CurrentValue { get; }

        /// <summary>
        /// Gets The element that is at the specified index.
        /// </summary>
        /// <param name="valuesAgo">The index of the element in the <see cref="ICacheElement{T}"/>.</param>
        /// <returns>The element that is at the specified index.</returns>
        object GetValue(int valuesAgo);

        /// <summary>
        /// Returns array of <see cref="ICache"/> elements from specified initial index.
        /// </summary>
        /// <param name="fromValuesAgo">The values ago where started to construct the array.</param>
        /// <param name="numOfValues">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        object[] ToArray(int fromValuesAgo, int numOfValues);

        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        object this[int index] { get; }

    }
    public interface ICache<T> : ICache, NinjaTrader.NinjaScript.ISeries<T>
    {
        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        new T this[int index] { get; }

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        new T CurrentValue { get; }

        /// <summary>
        /// Gets The element that is at the specified index.
        /// </summary>
        /// <param name="valuesAgo">The index of the element in the <see cref="ICacheElement{T}"/>.</param>
        /// <returns>The element that is at the specified index.</returns>
        new T GetValue(int valuesAgo);

        /// <summary>
        /// Returns <paramref name="numOfValues"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="fromValuesAgo">The values ago where started to construct the array.</param>
        /// <param name="numOfValues">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        new T[] ToArray(int fromValuesAgo, int numOfValues);

    }
}
