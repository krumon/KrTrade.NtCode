namespace KrTrade.Nt.Services
{
    public interface IBarUpdateCache
    {

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

    }
}
