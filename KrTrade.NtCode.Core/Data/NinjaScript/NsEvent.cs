namespace KrTrade.Nt.Core.Data
{
    public enum NsEvent
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

        /// <summary>
        /// Indicates the ninjascript bar has been updated.
        /// </summary>
        BarUpdate,

        /// <summary>
        /// Represents the ninjscript market data has been updated.
        /// </summary>
        MarketData,

        /// <summary>
        /// Represents the ninjascript market depth has been updated.
        /// </summary>
        MarketDepth,

        /// <summary>
        /// Indicates the ninjascript bar has been closed.
        /// </summary>
        BarClosed,

        /// <summary>
        /// Indicates the ninjascript price has changed.
        /// </summary>
        PriceChanged,

        /// <summary>
        /// Indicates the ninjascript tick has been produced.
        /// </summary>
        Tick,

        /// <summary>
        /// Indicates the ninjascript first tick has been produced.
        /// </summary>
        FirstTick,

        /// <summary>
        /// Indicates the ninjascript render is proccesing.
        /// </summary>
        Render,

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
