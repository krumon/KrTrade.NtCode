using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Caches
{
    public interface ICache<T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// Gets <see cref="ICache{T}"/> capacity.
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
        /// Indicates if <see cref="ICache{T}"/> is full.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// Remove the current element of the series.
        /// </summary>
        void RemoveLastElement();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        /// <summary>
        /// Dispose the <see cref="ISeries"/>. 
        /// </summary>
        void Dispose();

        /// <summary>
        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        T this[int index] { get; }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Gets the last value before the series was updated.
        /// </summary>
        T LastValue { get; }

        /// <summary>
        /// Returns range of cache elements from specified initial index to specified count.
        /// </summary>
        /// <param name="startIndex">The start index of the elements to returns.</param>
        /// <param name="count">The number of elements to returns.</param>
        /// <returns> Array with the elements specified.</returns>
        T[] GetRange(int startIndex, int count);

        /// <summary>
        /// Indicates if index is in cache range.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True, if the index is valid, otherwise false.</returns>
        bool IsValidIndex(int index);

        /// <summary>
        /// Indicates if range of values are in cahe range.
        /// </summary>
        /// <param name="startIndex">The start index of the range</param>
        /// <param name="count">The number of elements of range.</param>
        /// <returns>True, if the range is in cache range, otherwise false.</returns>
        bool IsValidRange(int startIndex, int count);

        /// <summary>
        /// Indicates if range of values are in cahe range.
        /// </summary>
        /// <param name="startIndex">The start index of the range</param>
        /// <param name="finalIndex">The final index of the range.</param>
        /// <param name="includeIndexValues">Indicates if index values are included in the search.</param>
        /// <returns>True, if the range is in cache range, otherwise false.</returns>
        bool IsValidRange(int startIndex, int finalIndex, bool includeIndexValues);

    }

}
