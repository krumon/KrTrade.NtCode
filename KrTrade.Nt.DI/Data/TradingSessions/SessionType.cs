
namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// Trading session types.
    /// </summary>
    public enum SessionType
    {
        // Main session
        Electronic,                 // Electronic TradingSessionInfo  

        // Regular and overnight sessionHoursList
        Regular,                    // Regular TradingSessionInfo.
        OVN,                        // Overnight TradingSessionInfo 

        // Major sessionHoursList
        Asian,                      // Asian TradingSessionInfo 
        Asian_RS,                   // Asian Residual TradingSessionInfo 
        European,                   // European TradingSessionInfo 
        AmericanAndEuropean,        // American and European TradingSessionInfo 
        American,                   // American TradingSessionInfo 
        American_RS,                // American Residual TradingSessionInfo 

        // American Minor sessionHoursList
        American_RS_EXT,            // American Residual TradingSessionInfo - American Extra Time
        American_RS_EOD,            // American Residual TradingSessionInfo - End Of Day
        American_RS_NWD,            // American Residual TradingSessionInfo - New Day

        Custom,                     // Custom session
        
        //// BALANCES
        //// --------
        //// Regular Balances.
        //Regular_IB,                 // Asian TradingSessionInfo - Initial Balance
        //Regular_BB,                 // Asian TradingSessionInfo - Between Balances
        //Regular_FB,                 // Asian TradingSessionInfo - Final Balance

        //// Overnight SessionHours
        //OVN_IB,                     // Overnight TradingSessionInfo - Initial Balance
        //OVN_BB,                     // Overnight TradingSessionInfo - Between Balances
        //OVN_FB,                     // Overnight TradingSessionInfo - Final Balance

        //// Asian Balances
        //Asian_IB,                   // Asian TradingSessionInfo - Initial Balance
        //Asian_BB,                   // Asian TradingSessionInfo - Between Balances
        //Asian_FB,                   // Asian TradingSessionInfo - Final Balance

        //// Asian Residual Balances
        //Asian_RS_IB,                // American Residual TradingSessionInfo - New Day Initial Balance
        //Asian_RS_BB,                // American Residual TradingSessionInfo - New Day Between Balances
        //Asian_RS_FB,                // American Residual TradingSessionInfo - New Day Final Balance

        //// European Balances
        //European_IB,                // European TradingSessionInfo - Initial Balance
        //European_BB,                // European TradingSessionInfo - Between Balances
        //European_FB,                // European TradingSessionInfo - Final Balance

        //// American and European Balances
        //AmericanAndEuropean_IB,     // American and European TradingSessionInfo - Initial Balance
        //AmericanAndEuropean_BB,     // American and European TradingSessionInfo - Between Balances
        //AmericanAndEuropean_FB,     // American and European TradingSessionInfo - Final Balance

        //// American Balances
        //American_IB,                // American TradingSessionInfo - Initial Balance
        //American_BB,                // American TradingSessionInfo - Between Balances
        //American_FB,                // American TradingSessionInfo - Final Balance

        //// American Residual Balances
        //American_RS_IB,             // American Residual TradingSessionInfo - New Day Initial Balance
        //American_RS_BB,             // American Residual TradingSessionInfo - New Day Between Balances
        //American_RS_FB,             // American Residual TradingSessionInfo - New Day Final Balance

        //// American Residual New Day Balances
        //American_RS_NWD_IB,         // American Residual TradingSessionInfo - New Day Initial Balance
        //American_RS_NWD_BB,         // American Residual TradingSessionInfo - New Day Between Balances
        //American_RS_NWD_FB,         // American Residual TradingSessionInfo - New Day Final Balance

    }
}
