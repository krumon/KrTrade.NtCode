namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Represents the services thats are recalculable when the session changed.
    /// </summary>
    public interface IRecalculableOnSessionChanged
    {
        /// <summary>
        /// Method that recalculates the service when the session changed.        
        /// </summary>
        /// <param name="args">Argiments of the sessions changed event.</param>
        void OnSessionChanged(SessionChangedEventArgs args);
        
    }
}
