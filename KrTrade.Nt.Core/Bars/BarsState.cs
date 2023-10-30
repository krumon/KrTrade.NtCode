namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// Represents the the bars state.
    /// </summary>
    public enum BarsState
    {
        /// <summary>
        /// Indicates that the bars have not caused any state.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the last bar has been closed.
        /// </summary>
        LastBarRemoved,

        /// <summary>
        /// Indicates that the last bar has been removed.
        /// </summary>
        BarClosed,

        /// <summary>
        /// Indicates that the first tick of the bar has occurred.
        /// This state is active when the calcule mode is 'Calculate.EachTick' or 'Calculate.PriceChanged'.
        /// </summary>
        FirstTick,

        /// <summary>
        /// Indicates that price changed.
        /// This state is active when the calcule mode is 'Calculate.PriceChanged'.
        /// </summary>
        PriceChanged,

        /// <summary>
        /// Indicates that one tick has occurred.
        /// This event is active when the calcule mode is 'Calculate.EachTick'
        /// </summary>
        Tick
    }
}
