namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// Represents the direrent states of the ninjascript allow your live cicle.
    /// </summary>
    public enum NinjascriptState
    {

        /// <summary>
        /// Indicates when displaying objects in a UI list such as the Indicators dialogue window since temporary objects are created for the purpose of UI display.
        /// </summary>
        SetDefaults,

        /// <summary>
        /// Indicates the user presses the OK or Apply button in a UI dialogue.
        /// </summary>
        Configure,

        /// <summary>
        /// Indicates the ninjascript is configure and is ready to process data.
        /// </summary>
        Active,

        /// <summary>
        /// Indicates all data series have been loaded.
        /// </summary>
        DataLoaded,

        /// <summary>
        /// Indicates the ninjascript begin to precess historical data.
        /// </summary>
        Historical,

        /// <summary>
        /// Indicates the ninjascript has finished processing historical data but before it starts to precess realtime data.
        /// </summary>
        Transition,

        /// <summary>
        /// Indicates the ninjascript begins to process realtime data.
        /// </summary>
        Realtime,

        /// <summary>
        /// Indicated the ninjascript has finished.
        /// </summary>
        Terminated,

    }
}
