using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public interface IBarUpdateCache : ICache
    {

        /// <summary>
        /// Gets <see cref="ICache{T}"/> count.
        /// </summary>
        int Count { get; }

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
        /// Add new element to cache using cache input series.
        /// </summary>
        /// <returns><c>true</c> if the element has been added, otherwise <c>false</c>.</returns>
        bool Add();

        /// <summary>
        /// Update the current element of cache using cache input series.
        /// </summary>
        /// <returns><c>true</c> if the element has been updated, otherwise <c>false</c>.</returns>
        bool Update();

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
        /// Gets the value of the specified index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache vaulue in the specified index.</returns>
        object this[int index] { get; }


    }
}
