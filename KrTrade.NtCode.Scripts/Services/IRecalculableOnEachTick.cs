namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Represents the services that are recalculable on each tick.
    /// </summary>
    public interface IRecalculableOnEachTick
    {
        /// <summary>
        /// Method thats recalculate the service on each tick.        
        /// </summary>
        void OnMarketData();
        
    }
}
