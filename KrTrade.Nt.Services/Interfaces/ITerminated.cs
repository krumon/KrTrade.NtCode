namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be disposed when the ninjascript is terminated.
    /// </summary>
    public interface ITerminated
    {
        /// <summary>
        /// Method to be executed for dispose the service.
        /// </summary>
        void Terminated();

    }
}
