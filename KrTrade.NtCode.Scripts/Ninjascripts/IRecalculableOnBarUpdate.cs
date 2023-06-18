namespace KrTrade.Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Represents the ninjascript services that are recalculable when the price is updated.
    /// </summary>
    public interface IRecalculableOnBarUpdate
    {
        /// <summary>
        /// Method thats recalculate the service when the price is updated.        
        /// </summary>
        void OnBarUpdate();
        
    }
}
