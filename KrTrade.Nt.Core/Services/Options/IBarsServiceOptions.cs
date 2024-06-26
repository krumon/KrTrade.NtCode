namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines the information of the service.
    /// </summary>
    public interface IBarsServiceOptions : IServiceOptions
    {
        /// <summary>
        /// Gets or sets the bars cache capacity.
        /// </summary>
        int CacheCapacity { get; set; }

        /// <summary>
        /// Gets or sets the bars removed cache capacity.
        /// </summary>
        int RemovedCacheCapacity { get; set; }

    }
}
