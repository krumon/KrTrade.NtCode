namespace KrTrade.Nt.Core.Data
{
    public enum NsLogLevel
    {
        /// <summary>
        /// Represents all ninjascript levels, from configuration to terminated state.
        /// </summary>
        Historical = 0,

        /// <summary>
        /// Represents from realtime state to terminated state.
        /// </summary>
        Realtime = 1,

        /// <summary>
        /// Represents only configuration states.
        /// </summary>
        Configure = 2,

        /// <summary>
        /// Represents none level.
        /// </summary>
        None = 3
    }
}
