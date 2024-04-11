namespace KrTrade.Nt.Core.Caches
{
    public interface IHasKey
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the service key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the unique key of the service.
        /// </summary>
        /// <returns>The string thats represents the unique key of the service.</returns>
        string GetKey();

    }
}
