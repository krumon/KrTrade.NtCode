namespace KrTrade.Nt.Core
{
    /// <summary>
    /// Defines methods that are necesary for reset any object.
    /// </summary>
    public interface IResetable
    {
        /// <summary>
        /// Method to reset the service.
        /// </summary>
        void Reset();

        /// <summary>
        /// Indicates the service is reset.
        /// </summary>
        bool IsReset { get; }

    }
}
