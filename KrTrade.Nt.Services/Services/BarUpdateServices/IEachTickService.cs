namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IDataSeriesService"/> when <see cref="IDataSeriesService"/> current bar new tick success 
    /// and defines properties and values to be used for other services when the service is updated.
    /// </summary>
    public interface IEachTickService : IBarUpdateService
    {
        /// <summary>
        /// Method to be executed when new tick success in <see cref="IDataSeriesService"/> current bar.
        /// </summary>
        void EachTick();
    }
}
