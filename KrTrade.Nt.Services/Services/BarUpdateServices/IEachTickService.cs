namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IBarsService"/> when <see cref="IBarsService"/> current bar new tick success 
    /// and defines properties and values to be used for other services when the service is updated.
    /// </summary>
    public interface IEachTickService : IBarUpdateService
    {
        /// <summary>
        /// Method to be executed when new tick success in <see cref="IBarsService"/> current bar.
        /// </summary>
        void EachTick();
    }
}
