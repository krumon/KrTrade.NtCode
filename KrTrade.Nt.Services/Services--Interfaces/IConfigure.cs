namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed to configure the ninjascript.
    /// </summary>
    public interface IConfigure
    {
        /// <summary>
        /// Method to configure the ninjascript.
        /// </summary>
        void Configure();

        /// <summary>
        /// Indicates the service is configure.
        /// </summary>
        bool IsConfigure {  get; }

    }
}
