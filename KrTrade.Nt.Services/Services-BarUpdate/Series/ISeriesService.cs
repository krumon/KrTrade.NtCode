namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a series service.
    /// </summary>
    public interface ISeriesService<TSeries> : IBarUpdateService
    {
        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Series element located at specified index.</returns>
        object this[int index] { get; }

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Represents the removed values cache capacity.
        /// </summary>
        int OldValuesCapacity { get; }

        /// <summary>
        /// Gets the series period.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        bool IsFull { get; }

    }
}
