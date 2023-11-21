namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Provides the options to configure the services.
    /// </summary>
    public abstract class BaseServiceOptions
    {
        /// <summary>
        /// Indicates if the service is enabled.
        /// </summary>
        public bool IsEnable { get; set; } = true;
    }
}
