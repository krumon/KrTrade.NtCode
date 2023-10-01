namespace KrTrade.Nt.DI.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the bar updated.
    /// </summary>
    public interface IOnSessionUpdateService
    {
        /// <summary>
        /// Methods used to update the service when the bar is closed or in each tick of the bar.        
        /// </summary>
        void OnSessionUpdate();
        
    }
}
