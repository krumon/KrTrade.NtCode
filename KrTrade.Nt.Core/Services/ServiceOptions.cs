namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Provides the options for any object.
    /// </summary>
    public class ServiceOptions
    {
        /// <summary>
        /// Indicates if the object is enabled.
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// Indicates if the object logger is enable.
        /// </summary>
        public bool IsLogEnable { get; set; } = true;

    }
}
