namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// Represents the the bars state.
    /// </summary>
    public enum BarEvent
    {
        /// <summary>
        /// Indicates that the last bar has been updated.
        /// </summary>
        Updated,

        /// <summary>
        /// Indicates that the last bar has been closed.
        /// </summary>
        Removed,

        /// <summary>
        /// Indicates that the last bar has been removed.
        /// </summary>
        Closed,

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
