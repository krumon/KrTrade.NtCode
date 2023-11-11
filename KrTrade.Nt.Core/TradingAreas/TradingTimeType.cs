
namespace KrTrade.Nt.Core.TradingAreas
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum TradingTimeType
    {
        // Custom session
        Custom,                         // Custom TradingSessionInfo.

        // MARKET EXCHANGES TIMES
        //CME_FutureIdx_EL_Open,          // CME Future Index Market Exchange - Open 
        //CME_FutureIdx_EL_Close,         // CME Future Index Market Exchange - Close 
        //CME_FutureIdx_RG_Close,         // CME Future Index Market Exchange - Close 
        //CME_FutureIdx_RG_Open,          // CME Future Index Market Exchange - Open 

        // Main TradingSessionInfo Times
        Electronic_Open,
        Electronic_Close,
        Regular_Open,
        Regular_Close,
        OVN_Open,
        OVN_Close,

        // MAJOR SESSIONS TIMES
        Asian_Open,                     // Asian TradingSessionInfo - Open 
        Asian_Close,                    // Asian TradingSessionInfo - Close 
        Asian_RS_Open,                  // Asian Residual TradingSessionInfo - Open
        Asian_RS_Close,                 // Asian Residual TradingSessionInfo - Close
        European_Open,                  // European TradingSessionInfo - Open 
        European_Close,                 // European TradingSessionInfo - Close 
        AmericanAndEuropean_Open,       // American and European TradingSessionInfo - Open 
        AmericanAndEuropean_Close,      // American and European TradingSessionInfo - Close 
        American_Open,                  // American TradingSessionInfo - Open 
        American_Close,                 // American TradingSessionInfo - Close 
        American_RS_Open,               // American Residual TradingSessionInfo - Open
        American_RS_Close,              // American Residual TradingSessionInfo - Close

        // MINOR SESSIONS TIMES
        American_RS_EXT_Open,           // American Residual TradingSessionInfo - American Extra time - Open
        American_RS_EXT_Close,          // American Residual TradingSessionInfo - American Extra time - Close
        American_RS_EOD_Open,           // American Residual TradingSessionInfo - End of day - Open
        American_RS_EOD_Close,          // American Residual TradingSessionInfo - End of day - Close
        American_RS_NWD_Open,           // American Residual TradingSessionInfo - New day - Open
        American_RS_NWD_Close,          // American Residual TradingSessionInfo - New day - Close
        
        // BALANCES SESSIONS TIMES
        // -----------------
        // Asian Balances SessionHours
        //Asian_IB_Open,                  // Asian TradingSessionInfo - Initial Balance - Open
        //Asian_IB_Close,                 // Asian TradingSessionInfo - Initial Balance - Close
        //Asian_BB_Open,                  // Asian TradingSessionInfo - Between balances - Open
        //Asian_BB_Close,                 // Asian TradingSessionInfo - Between balances - Close
        //Asian_FB_Open,                  // Asian TradingSessionInfo - Final Balance - Open
        //Asian_FB_Close,                 // Asian TradingSessionInfo - Final Balance - Close

        // Asian Residual Balances SessionHours
        //Asian_RS_IB_Open,               // Asian Residual TradingSessionInfo - New day initial balance - Open
        //Asian_RS_IB_Close,              // Asian Residual TradingSessionInfo - New day initial balance - Close
        //Asian_RS_BB_Open,               // Asian Residual TradingSessionInfo - Between balances - Open
        //Asian_RS_BB_Close,              // Asian Residual TradingSessionInfo - Between balances - Close
        //Asian_RS_FB_Open,               // Asian Residual TradingSessionInfo - New day final balance - Open
        //Asian_RS_FB_Close,              // Asian Residual TradingSessionInfo - New day final balance - Close

        // European Balances SessionHours
        //European_IB_Open,               // European TradingSessionInfo Time - Initial Balance - Open
        //European_IB_Close,              // European TradingSessionInfo Time - Initial Balance - Close
        //European_BB_Open,               // European TradingSessionInfo Time - Between balances - Open
        //European_BB_Close,              // European TradingSessionInfo Time - Between balances - Close
        //European_FB_Open,               // European TradingSessionInfo Time - Final Balance - Open
        //European_FB_Close,              // European TradingSessionInfo Time - Final Balance - Close

        // American and European Balances SessionHours
        //AmericanAndEuropean_IB_Open,    // American and European TradingSessionInfo Time - Initial Balance - Open
        //AmericanAndEuropean_IB_Close,   // American and European TradingSessionInfo Time - Initial Balance - Close
        //AmericanAndEuropean_BB_Open,    // American and European TradingSessionInfo Time - Between balances - Open
        //AmericanAndEuropean_BB_Close,   // American and European TradingSessionInfo Time - Between balances - Close
        //AmericanAndEuropean_FB_Open,    // American and European TradingSessionInfo Time - Final Balance - Open
        //AmericanAndEuropean_FB_Close,   // American and European TradingSessionInfo Time - Final Balance - Close

        // American Balances SessionHours
        //American_IB_Open,               // American TradingSessionInfo Time - Initial Balance - Open
        //American_IB_Close,              // American TradingSessionInfo Time - Initial Balance - Close
        //American_BB_Open,               // American TradingSessionInfo Time - Between balances - Open
        //American_BB_Close,              // American TradingSessionInfo Time - Between balances - Close
        //American_FB_Open,               // American TradingSessionInfo Time - Final Balance - Open
        //American_FB_Close,              // American TradingSessionInfo Time - Final Balance - Close

        // American Residual Balances SessionHours
        //American_RS_NWD_IB_Open,          // American Residual TradingSessionInfo - New day initial balance - Open
        //American_RS_NWD_IB_Close,         // American Residual TradingSessionInfo - New day initial balance - Close
        //American_RS_NWD_BB_Open,          // American Residual TradingSessionInfo - Between balances - Open
        //American_RS_NWD_BB_Close,         // American Residual TradingSessionInfo - Between balances - Close
        //American_RS_NWD_FB_Open,          // American Residual TradingSessionInfo - New day final balance - Open
        //American_RS_NWD_FB_Close,         // American Residual TradingSessionInfo - New day final balance - Close

    }
}