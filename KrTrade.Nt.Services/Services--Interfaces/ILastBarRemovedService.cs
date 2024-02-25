namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods to be executed in <see cref="IDataSeriesService"/> when <see cref="IDataSeriesService"/> last bar is removed 
    /// and defines properties and values to be used for other services when service is updated.
    /// </summary>
    public interface ILastBarRemovedService : IBarUpdateService 
    {
        /// <summary>
        /// Method to be executed when <see cref="IDataSeriesService"/> last bar is removed.
        /// </summary>
        void LastBarRemoved();
    }
}
