namespace KrTrade.NtCode.Events
{
    /// <summary>
    /// The type of the bar.
    /// </summary>
    public enum EventType
    {

        /// <summary>
        /// An event driven method which is called whenever a bar is updated.
        /// </summary>
        BarUpdate,

        /// <summary>
        /// An event driven method which is called on every change in level one market.
        /// </summary>
        MarketData,

        /// <summary>
        /// An event driven method which is called on every session changed.
        /// </summary>
        SessionChanged,

        /// <summary>
        /// Event driven method which is called on every change in level one market.
        /// </summary>
        MarketDepth,

        /// <summary>
        /// An event driven method used which is called for every change in connection status.
        /// </summary>
        ConnectionStatusUpdate,

        /// <summary>
        /// An event driven method which is called for every change in fundamental data for the underlying instrument.
        /// </summary>
        FundamentalData,

        /// <summary>
        /// Occurs when displaying objects in a UI list such as the Indicators dialogue window 
        /// since temporary objects are created for the purpose of UI display.
        /// </summary>
        SetDefaults,

        /// <summary>
        /// Occurs once after a user adds an object to the applied list of objects.
        /// and presses the OK or Apply button.  
        /// This state is called only once for the life of the object.
        /// </summary>
        Configure,

        /// <summary>
        /// Occurs once after the object is configured and is ready to process data. 
        /// and presses the OK or Apply button.  
        /// This state is called only once for the life of the object.
        /// </summary>
        Active,

        /// <summary>
        /// Occurs only once after all data series have been loaded.
        /// </summary>
        DataLoaded,

        /// <summary>
        /// Occurs whenever state changed to Historical.
        /// </summary>
        Historical,

        /// <summary>
        /// Occurs once as the object has finished processing historical data 
        /// but before it starts to process realtime data.
        /// </summary>
        Transition,

        /// <summary>
        /// Occurs whenever state changed to real-time.
        /// </summary>
        Realtime,

        /// <summary>
        /// Occurs whenever state changed to Terminate.
        /// </summary>
        Terminated,

    }
}
