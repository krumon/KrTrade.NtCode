namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IBarsService"/> when <see cref="IBarsService"/> current bar first tick success 
    /// and defines properties and values to be used for other services when the service is updated.
    /// </summary>
    public interface IFirstTickService : IBarUpdateService 
    {
        /// <summary>
        /// Method to be executed when first tick success in <see cref="IBarsService"/> current bar.
        /// </summary>
        void FirstTick();
    }
}
