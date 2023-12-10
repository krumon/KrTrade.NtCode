namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Provides the options for any <see cref="INinjascriptService"/>.
    /// </summary>
    public class NinjascriptServiceOptions : ServiceOptions
    {
        /// <summary>
        /// Indicates if the log service is enable.
        /// </summary>
        public bool IsLogEnable { get; set; } = true;
    }
}
