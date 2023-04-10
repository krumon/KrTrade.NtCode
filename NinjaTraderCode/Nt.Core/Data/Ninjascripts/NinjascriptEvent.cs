namespace Nt.Core.Data
{
    /// <summary>
    /// Nijascripts event types.
    /// </summary>
    public enum NinjaScriptEvent
    {
        /// <summary>
        /// Indicates every change in ninjascript state.
        /// </summary>
        StateChange,

        /// <summary>
        /// Indicates whenever a bar is updated. This state will be determined by the "Calculate" property.
        /// </summary>
        BarUpdate,

        /// <summary>
        /// Indicates every change in level one market data for the underlying instrument.
        /// </summary>
        MarketData,

        /// <summary>
        /// Indicates every change in level two market data (market depth) for the underlying instrument.
        /// </summary>
        MarketDepth,

        /// <summary>
        /// Indicates every change in fundamental data for the underlying instrument.
        /// </summary>
        FundamentalData,

        /// <summary>
        /// Indicates every change in connection status.
        /// </summary>
        ConnectionStatusUpdate,

    }
}
