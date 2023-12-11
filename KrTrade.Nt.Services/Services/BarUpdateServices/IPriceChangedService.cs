namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IDataSeriesService"/> when <see cref="IDataSeriesService"/> current bar price changed 
    /// and defines properties and values to be used for other services when the service is updated.
    /// </summary>
    public interface IPriceChangedService : IBarUpdateService 
    {
        /// <summary>
        /// Method to be executed when <see cref="IDataSeriesService"/> current bar price changed.
        /// </summary>
        void PriceChanged();
    }
}
