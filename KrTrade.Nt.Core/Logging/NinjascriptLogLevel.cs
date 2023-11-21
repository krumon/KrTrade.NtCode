namespace KrTrade.Nt.Core.Logging
{
    /// <summary>
    /// Represents states to logger in the ninjascript output window or other site.
    /// </summary>
    public enum NinjascriptLogLevel
    {
        /// <summary>
        /// Represents historical level. Prints all ninjascript states.
        /// </summary>
        Historical = 0,

        /// <summary>
        /// Represents realtime states. Prints the configuration and realtime states. Include transition state.
        /// </summary>
        Realtime = 1,

        /// <summary>
        /// Represents configuration level. Prints only configuration states.
        /// </summary>
        Configuration = 2,

        /// <summary>
        /// Represents none level.
        /// </summary>
        None = 3

    }
}
