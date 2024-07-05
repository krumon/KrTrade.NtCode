namespace KrTrade.Nt.Core.Options
{
    /// <summary>
    /// Provides the options for any service.
    /// </summary>
    public class ElementOptions : Options, IElementOptions
    {
        /// <summary>
        /// Indicates if the log service is enable.
        /// </summary>
        public bool IsLogEnable { get; set; } = true;

    }
}
