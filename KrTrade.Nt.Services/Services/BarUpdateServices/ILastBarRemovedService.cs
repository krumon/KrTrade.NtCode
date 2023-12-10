namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IBarsService"/> when <see cref="IBarsService"/> last bar is removed 
    /// and defines properties and values to be used for other services when service is updated.
    /// </summary>
    public interface ILastBarRemovedService : IBarUpdateService 
    {
        /// <summary>
        /// Method to be executed when <see cref="IBarsService"/> last bar is removed.
        /// </summary>
        void LastBarRemoved();
    }
}
