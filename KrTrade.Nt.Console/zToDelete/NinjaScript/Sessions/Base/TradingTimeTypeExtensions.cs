using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.TradingHours;
using System;

namespace KrTrade.Nt.Console
{

    /// <summary>
    /// <see cref="TradingTimeType"/> helper methods.
    /// </summary>
    public static class TradingTimeTypeExtensions
    {

        /// <summary>
        /// Converts the <see cref="TradingTimeType"/> to unique code.
        /// </summary>
        /// <param name="tradingTimeType">The specific session time.</param>
        /// <returns></returns>
        public static string ToCode(this TradingHoursType tradingTimeType)
        {
            switch (tradingTimeType)
            {
                // MAIN SESSIONS
                case TradingHoursType.Custom:
                    return "CTM";
                case TradingHoursType.Electronic_Open:
                    return "EL-O";
                case TradingHoursType.Electronic_Close:
                    return "EL-C";
                case TradingHoursType.Regular_Open:
                    return "RG-O";
                case TradingHoursType.Regular_Close:
                    return "RG-C";
                case TradingHoursType.OVN_Open:
                    return "OVN-O";
                case TradingHoursType.OVN_Close:
                    return "OVN-C";

                // MAJOR SESSIONS
                case TradingHoursType.Asian_Open:
                    return "AS-O";
                case TradingHoursType.Asian_Close:
                    return "AS-C";
                case TradingHoursType.Asian_RS_Open:
                    return "AS-RS-O";
                case TradingHoursType.Asian_RS_Close:
                    return "AS-RS-C";
                case TradingHoursType.European_Open:
                    return "EU-O";
                case TradingHoursType.European_Close:
                    return "EU-C";
                case TradingHoursType.AmericanAndEuropean_Open:
                    return "AE-O";
                case TradingHoursType.AmericanAndEuropean_Close:
                    return "AE-C";
                case TradingHoursType.American_Open:
                    return "AM-O";
                case TradingHoursType.American_Close:
                    return "AM-C";
                case TradingHoursType.American_RS_Open:
                    return "AM-RS-O";
                case TradingHoursType.American_RS_Close:
                    return "AM-RS-C";

                // MINOR SESSIONS
                case TradingHoursType.American_RS_EXT_Open:
                    return "AM-RS-EXT-O";
                case TradingHoursType.American_RS_EXT_Close:
                    return "AM-RS-EXT-C";
                case TradingHoursType.American_RS_EOD_Open:
                    return "AM-RS-EOD-O";
                case TradingHoursType.American_RS_EOD_Close:
                    return "AM-RS-EOD-C";
                case TradingHoursType.American_RS_NWD_Open:
                    return "AM-RS-NWD-O";
                case TradingHoursType.American_RS_NWD_Close:
                    return "AM-RS-NWD-C";
                
                // BALANCES SESSIONS
                // -----------------

                // Asian balances
                //case SpecificSessionTime.Asian_IB_Open:
                //    return "AS-IB-O";
                //case SpecificSessionTime.Asian_IB_Close:
                //    return "AS-IB-C";
                //case SpecificSessionTime.Asian_BB_Open:
                //    return "AS-BB-O";
                //case SpecificSessionTime.Asian_BB_Close:
                //    return "AS-BB-C";
                //case SpecificSessionTime.Asian_FB_Open:
                //    return "AS-FB-O";
                //case SpecificSessionTime.Asian_FB_Close:
                //    return "AS-FB-C";

                //// Asian Residual balances
                //case SpecificSessionTime.Asian_RS_IB_Open:
                //    return "AS-RS-IB-O";
                //case SpecificSessionTime.Asian_RS_IB_Close:
                //    return "AS-RS-IB-C";
                //case SpecificSessionTime.Asian_RS_BB_Open:
                //    return "AS-RS-BB-O";
                //case SpecificSessionTime.Asian_RS_BB_Close:
                //    return "AS-RS-BB-C";
                //case SpecificSessionTime.Asian_RS_FB_Open:
                //    return "AS-RS-FB-O";
                //case SpecificSessionTime.Asian_RS_FB_Close:
                //    return "AS-RS-FB-C";

                //// American balances.
                //case SpecificSessionTime.European_IB_Open:
                //    return "EU-IB-O";     
                //case SpecificSessionTime.European_IB_Close:
                //    return "EU-IB-C";     
                //case SpecificSessionTime.European_BB_Open:
                //    return "EU-BB-O";     
                //case SpecificSessionTime.European_BB_Close:
                //    return "EU-BB-C";     
                //case SpecificSessionTime.European_FB_Open:
                //    return "EU-FB-O";     
                //case SpecificSessionTime.European_FB_Close:
                //    return "EU-FB-C";

                //// American and European balances.
                //case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                //    return "AE-IB-O";
                //case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                //    return "AE-IB-C";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                //    return "AE-BB-O";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                //    return "AE-BB-C";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                //    return "AE-FB-O";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                //    return "AE-FB-C";

                //// American balances.
                //case SpecificSessionTime.American_IB_Open:
                //    return "AM-IB-O";
                //case SpecificSessionTime.American_IB_Close:
                //    return "AM-IB-C";
                //case SpecificSessionTime.American_BB_Open:
                //    return "AM-BB-O";
                //case SpecificSessionTime.American_BB_Close:
                //    return "AM-BB-C";
                //case SpecificSessionTime.American_FB_Open:
                //    return "AM-FB-O";
                //case SpecificSessionTime.American_FB_Close:
                //    return "AM-FB-C";

                //// American Residual new day balances
                //case SpecificSessionTime.American_RS_NWD_IB_Open:
                //    return "AM-RS-NWD-IB-O";
                //case SpecificSessionTime.American_RS_NWD_IB_Close:
                //    return "AM-RS-NWD-IB-C";
                //case SpecificSessionTime.American_RS_NWD_BB_Open:
                //    return "AM-RS-NWD-BB-O";
                //case SpecificSessionTime.American_RS_NWD_BB_Close:
                //    return "AM-RS-NWD-BB-C";
                //case SpecificSessionTime.American_RS_NWD_FB_Open:
                //    return "AM-RS-NWD-FB-O";
                //case SpecificSessionTime.American_RS_NWD_FB_Close:
                //    return "AM-RS-NWD-FB-C";

                default:
                    throw new Exception("The trading time type is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingTimeType"/> to unique code.
        /// </summary>
        /// <param name="code">The string to convert.</param>
        /// <returns></returns>
        public static TradingHoursType ToTradingTime(this String code)
        {
            // MAIN SESSIONS
            if (code == "CTM")
                return TradingHoursType.Custom;
            if (code == "EL-O")
                return TradingHoursType.Electronic_Open;
            if (code == "EL-C")
                return TradingHoursType.Electronic_Close;
            if (code == "RG-O")
                return TradingHoursType.Regular_Open;
            if (code == "RG-C")
                return TradingHoursType.Regular_Close;
            if (code == "OVN-O")
                return TradingHoursType.OVN_Open;
            if (code == "OVN-C")
                return TradingHoursType.OVN_Close;

            // MAJOR SESSIONS
            if (code == "AS-O")
                return TradingHoursType.Asian_Open;
            if (code == "AS-C")
                return TradingHoursType.Asian_Close;
            if (code == "AS-RS-O")
                return TradingHoursType.Asian_RS_Open;
            if (code == "AS-RS-C")
                return TradingHoursType.Asian_RS_Close;
            if (code == "EU-O")
                return TradingHoursType.European_Open;
            if (code == "EU-C")
                return TradingHoursType.European_Close;
            if (code == "AE-O")
                return TradingHoursType.AmericanAndEuropean_Open;
            if (code == "AE-C")
                return TradingHoursType.AmericanAndEuropean_Close;
            if (code == "AM-O")
                return TradingHoursType.American_Open;
            if (code == "AM-C")
                return TradingHoursType.American_Close;
            if (code == "AM-RS-O")
                return TradingHoursType.American_RS_Open;
            if (code == "AM-RS-C")
                return TradingHoursType.American_RS_Close;

            // MINOR SESSIONS
            if (code == "AM-RS-EXT-O")
                return TradingHoursType.American_RS_EXT_Open;
            if (code == "AM-RS-EXT-C")
                return TradingHoursType.American_RS_EXT_Close;
            if (code == "AM-RS-EOD-O")
                return TradingHoursType.American_RS_EOD_Open;
            if (code == "AM-RS-EOD-C")
                return TradingHoursType.American_RS_EOD_Close;
            if (code == "AM-RS-NWD-O")
                return TradingHoursType.American_RS_NWD_Open;
            if (code == "AM-RS-NWD-C")
                return TradingHoursType.American_RS_NWD_Close;

            throw new Exception("The string value to converts is not a implemented code value.");

        }

        /// <summary>
        /// Converts the <see cref="TradingHoursType"/> to description.
        /// </summary>
        /// <param name="tradingTimeType">The specific session time.</param>
        /// <returns></returns>
        public static string ToDescription(this TradingHoursType tradingTimeType)
        {
            switch (tradingTimeType)
            {
                // MAIN SESSIONS
                case TradingHoursType.Custom:
                    return "Custom Time.";
                case TradingHoursType.Electronic_Open:
                    return "Electronic TradingSession. Open time.";
                case TradingHoursType.Electronic_Close:
                    return "Electronic TradingSession. Close time.";
                case TradingHoursType.Regular_Open:
                    return "Regular TradingSession. Open time.";
                case TradingHoursType.Regular_Close:
                    return "Regular TradingSession. Close time.";
                case TradingHoursType.OVN_Open:
                    return "Overnight TradingSession. Open time.";
                case TradingHoursType.OVN_Close:
                    return "Overnight TradingSession. Close time.";

                // MAJOR SESSIONS
                case TradingHoursType.Asian_Open:
                    return "Asian TradingSession. Open time.";
                case TradingHoursType.Asian_Close:
                    return "Asian TradingSession. Close time.";
                case TradingHoursType.Asian_RS_Open:
                    return "Asian residual TradingSession. Open time.";
                case TradingHoursType.Asian_RS_Close:
                    return "Asian residual TradingSession. Close time.";
                case TradingHoursType.European_Open:
                    return "European TradingSession. Open time.";
                case TradingHoursType.European_Close:
                    return "European TradingSession. Close time.";
                case TradingHoursType.AmericanAndEuropean_Open:
                    return "American and European TradingSession. Open time.";
                case TradingHoursType.AmericanAndEuropean_Close:
                    return "American and European TradingSession. Close time.";
                case TradingHoursType.American_Open:
                    return "American TradingSession. Open time.";
                case TradingHoursType.American_Close:
                    return "American TradingSession. Close time.";
                case TradingHoursType.American_RS_Open:
                    return "American residual TradingSession. Open time.";
                case TradingHoursType.American_RS_Close:
                    return "American residual TradingSession. Close time.";

                // MINOR SESSIONS
                case TradingHoursType.American_RS_EXT_Open:
                    return "American Residual TradingSession. Extra Time Open.";
                case TradingHoursType.American_RS_EXT_Close:
                    return "American Residual TradingSession. Extra Time Close.";
                case TradingHoursType.American_RS_EOD_Open:
                    return "American Residual TradingSession. End Of Day Open.";
                case TradingHoursType.American_RS_EOD_Close:
                    return "American Residual TradingSession. End Of Day Close.";
                case TradingHoursType.American_RS_NWD_Open:
                    return "American Residual TradingSession. New Day Open.";
                case TradingHoursType.American_RS_NWD_Close:
                    return "American Residual TradingSession. New Day Close.";

                // BALANCES SESSIONS
                // -----------------

                //// Asian balances
                //case SpecificSessionTime.Asian_IB_Open:
                //    return "Open time of Asian initial balance.";
                //case SpecificSessionTime.Asian_IB_Close:
                //    return "Close time of Asian initial balance.";
                //case SpecificSessionTime.Asian_BB_Open:
                //    return "Open time between balance of Asian session.";
                //case SpecificSessionTime.Asian_BB_Close:
                //    return "Close time between balance of Asian session.";
                //case SpecificSessionTime.Asian_FB_Open:
                //    return "Open time of Asian final balance.";
                //case SpecificSessionTime.Asian_FB_Close:
                //    return "Close time of Asian final balance.";

                //// Asian Residual new day balances
                //case SpecificSessionTime.Asian_RS_IB_Open:
                //    return "Open time of Asian residual intial balance.";
                //case SpecificSessionTime.Asian_RS_IB_Close:
                //    return "Close time of Asian residual initial balance.";
                //case SpecificSessionTime.Asian_RS_BB_Open:
                //    return "Open time between balance of Asian residual session.";
                //case SpecificSessionTime.Asian_RS_BB_Close:
                //    return "Close time between balances of Asian residual session balance.";
                //case SpecificSessionTime.Asian_RS_FB_Open:
                //    return "Open time of Asian residual final balance.";
                //case SpecificSessionTime.Asian_RS_FB_Close:
                //    return "Close time of Asian residual final balance.";

                //// European balances
                //case SpecificSessionTime.European_IB_Open:
                //    return "Open time of European_IB_Open initial balance.";
                //case SpecificSessionTime.European_IB_Close:
                //    return "Close time of European_IB_Open initial balance.";
                //case SpecificSessionTime.European_BB_Open:
                //    return "Open time between balance of European session.";
                //case SpecificSessionTime.European_BB_Close:
                //    return "Close time between balance of European session.";
                //case SpecificSessionTime.European_FB_Open:
                //    return "Open time of European final balance.";
                //case SpecificSessionTime.European_FB_Close:
                //    return "Close time of European final balance.";

                //// American and European balances
                //case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                //    return "Open time of American and European initial balance.";
                //case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                //    return "Close time of American and European initial balance.";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                //    return "Open time between balance of American and European session.";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                //    return "Close time between balance of American and European session.";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                //    return "Open time of American and European final balance.";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                //    return "Close time of American and European final balance.";

                //// American balances
                //case SpecificSessionTime.American_IB_Open:
                //    return "Open time of American initial balance.";
                //case SpecificSessionTime.American_IB_Close:
                //    return "Close time of American initial balance.";
                //case SpecificSessionTime.American_BB_Open:
                //    return "Open time between balance of American session.";
                //case SpecificSessionTime.American_BB_Close:
                //    return "Close time between balance of American session.";
                //case SpecificSessionTime.American_FB_Open:
                //    return "Open time of American final balance.";
                //case SpecificSessionTime.American_FB_Close:
                //    return "Close time of American final balance.";

                //// American Residual new day balances
                //case SpecificSessionTime.American_RS_NWD_IB_Open:
                //    return "Open time of new day intial balance.";
                //case SpecificSessionTime.American_RS_NWD_IB_Close:
                //    return "Close time of new day initial balance.";
                //case SpecificSessionTime.American_RS_NWD_BB_Open:
                //    return "Open time between balance of new day session.";
                //case SpecificSessionTime.American_RS_NWD_BB_Close:
                //    return "Close time between balances of new day session balance.";
                //case SpecificSessionTime.American_RS_NWD_FB_Open:
                //    return "Open time of new day final balance.";
                //case SpecificSessionTime.American_RS_NWD_FB_Close:
                //    return "Close time of new day final balance.";

                default:
                    throw new Exception("The trading time type is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingHoursType"/> to name.
        /// </summary>
        /// <param name="tradingTimeType">The specific trading time.</param>
        /// <returns>The name of the <see cref="TradingTimeType"/>.</returns>
        public static string ToName(this TradingHoursType tradingTimeType)
        {
            switch (tradingTimeType)
            {
                // MAIN SESSIONS
                case TradingHoursType.Custom:
                    return "TradingTime - Custom";
                case TradingHoursType.Electronic_Open:
                    return "TradingTime - Electronic Open";
                case TradingHoursType.Electronic_Close:
                    return "TradingTime - Electronic Close";
                case TradingHoursType.Regular_Open:
                    return "TradingTime - Regular Open";
                case TradingHoursType.Regular_Close:
                    return "TradingTime - Regular Close";
                case TradingHoursType.OVN_Open:
                    return "TradingTime - Ovenight Open";
                case TradingHoursType.OVN_Close:
                    return "TradingTime - Overnight Close";

                // MAJOR SESSIONS
                case TradingHoursType.Asian_Open:
                    return "TradingTime - Asian Open";
                case TradingHoursType.Asian_Close:
                    return "TradingTime - Asian Close";
                case TradingHoursType.Asian_RS_Open:
                    return "TradingTime - AsianResidual Open";
                case TradingHoursType.Asian_RS_Close:
                    return "TradingTime - AsianResidual Close";
                case TradingHoursType.European_Open:
                    return "TradingTime - European Open";
                case TradingHoursType.European_Close:
                    return "TradingTime - European Close";
                case TradingHoursType.AmericanAndEuropean_Open:
                    return "TradingTime - AmericanAndEuropean Open";
                case TradingHoursType.AmericanAndEuropean_Close:
                    return "TradingTime - AmericanAndEuropean Close";
                case TradingHoursType.American_Open:
                    return "TradingTime - American Open";
                case TradingHoursType.American_Close:
                    return "TradingTime - American Close";
                case TradingHoursType.American_RS_Open:
                    return "TradingTime - AmericanResidual Open";
                case TradingHoursType.American_RS_Close:
                    return "TradingTime - AmericanResidual Close";

                // MINOR SESSIONS
                case TradingHoursType.American_RS_EXT_Open:
                    return "TradingTime - AmericanResidualExtratime Open";
                case TradingHoursType.American_RS_EXT_Close:
                    return "TradingTime - AmericanResidualExtratime Close";
                case TradingHoursType.American_RS_EOD_Open:
                    return "TradingTime - AmericanResidualEndOfDay Open";
                case TradingHoursType.American_RS_EOD_Close:
                    return "TradingTime - AmericanResidualEndOfDay Close";
                case TradingHoursType.American_RS_NWD_Open:
                    return "TradingTime - AmericanResidualNewDay Open";
                case TradingHoursType.American_RS_NWD_Close:
                    return "TradingTime - AmericanResidualNewDay Close";

                default:
                    throw new Exception("The trading time type is not implemented.");
            }
        }

        ///// <summary>
        ///// Converts the <see cref="TradingTime"/> to <see cref="TimeZoneInfo"/>.
        ///// </summary>
        ///// <param name="tradingTime"></param>
        ///// <returns><see cref="TimeZoneInfo"/> of the <see cref="TradingTime"/>.</returns>
        //public static TimeZoneInfo ToTimeZoneInfo(this TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        //{
        //    switch (instrumentCode)
        //    {
        //        case (InstrumentCode.Default):
        //        case (InstrumentCode.MES):
        //            {
        //                switch (tradingTime)
        //                {
        //                    // MARKET EXCHANGE TIME ZONES
        //                    case (TradingTime.Electronic_Open):
        //                    case (TradingTime.Electronic_Close):
        //                    case (TradingTime.Regular_Open):
        //                    case (TradingTime.Regular_Close):
        //                    case (TradingTime.OVN_Open):
        //                    case (TradingTime.OVN_Close):
        //                        return instrumentCode.ToMarketExchange().ToTimeZoneInfo();

        //                    // MAJOR SESSIONS TIME ZONES
        //                    case (TradingTime.AmericanAndEuropean_Open):
        //                    case (TradingTime.American_Close):
        //                    case (TradingTime.American_RS_Open):
        //                    case (TradingTime.American_RS_EXT_Open):
        //                    case (TradingTime.American_RS_EXT_Close):
        //                    case (TradingTime.American_RS_EOD_Open):
        //                    case (TradingTime.American_RS_EOD_Close):
        //                    case (TradingTime.American_RS_NWD_Open):
        //                    //case (SpecificSessionTime.American_IB_Open):
        //                    //case (SpecificSessionTime.American_IB_Close):
        //                    //case (SpecificSessionTime.American_BB_Open):
        //                    //case (SpecificSessionTime.American_BB_Close):
        //                    //case (SpecificSessionTime.American_FB_Open):
        //                    //case (SpecificSessionTime.American_FB_Close):
        //                    //case (SpecificSessionTime.American_RS_NWD_IB_Open):
        //                    //case (SpecificSessionTime.American_RS_NWD_IB_Close):
        //                    //case (SpecificSessionTime.American_RS_NWD_BB_Open):
        //                    //case (SpecificSessionTime.American_RS_NWD_BB_Close):
        //                    //case (SpecificSessionTime.American_RS_NWD_FB_Open):
        //                        return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

        //                    //case (SpecificSessionTime.American_RS_NWD_FB_Close):
        //                    //    return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        //                    case (TradingTime.American_RS_Close):
        //                    case (TradingTime.American_RS_NWD_Close):
        //                    case (TradingTime.Asian_Open):
        //                    case (TradingTime.Asian_Close):
        //                    case (TradingTime.Asian_RS_Open):
        //                    //case (SpecificSessionTime.Asian_IB_Open):
        //                    //case (SpecificSessionTime.Asian_IB_Close):
        //                    //case (SpecificSessionTime.Asian_BB_Open):
        //                    //case (SpecificSessionTime.Asian_BB_Close):
        //                    //case (SpecificSessionTime.Asian_FB_Open):
        //                    //case (SpecificSessionTime.Asian_FB_Close):
        //                    //case (SpecificSessionTime.Asian_RS_Open):
        //                    //case (SpecificSessionTime.Asian_RS_IB_Open):
        //                    //case (SpecificSessionTime.Asian_RS_IB_Close):
        //                    //case (SpecificSessionTime.Asian_RS_BB_Open):
        //                        return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        //                    case (TradingTime.Asian_RS_Close):
        //                    case (TradingTime.European_Open):
        //                    case (TradingTime.European_Close):
        //                    case (TradingTime.AmericanAndEuropean_Close):
        //                    case (TradingTime.American_Open):
        //                    //case (SpecificSessionTime.European_IB_Open):
        //                    //case (SpecificSessionTime.European_IB_Close):
        //                    //case (SpecificSessionTime.European_BB_Open):
        //                    //case (SpecificSessionTime.European_BB_Close):
        //                    //case (SpecificSessionTime.European_FB_Open):
        //                    //case (SpecificSessionTime.European_FB_Close):
        //                    //case (SpecificSessionTime.Asian_RS_Close):
        //                    //case (SpecificSessionTime.Asian_RS_BB_Close):
        //                    //case (SpecificSessionTime.Asian_RS_FB_Open):
        //                    //case (SpecificSessionTime.Asian_RS_FB_Close):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
        //                    //case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
        //                        return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

        //                    case (TradingTime.Custom):
        //                        throw new Exception("the convert is not possible, the session time is a custom value.");

        //                    default:
        //                        throw new Exception("The trading time doesn't exists.");

        //                }
        //            }

        //        default:
        //            throw new Exception("The instrument code doesn't exists.");
        //    }

        //}

        ///// <summary>
        ///// Converts the <see cref="TradingTime"/> type to <see cref="Time"/>.
        ///// </summary>
        ///// <param name="tradingTime"></param>
        ///// <returns><see cref="TimeSpan"/> of the <see cref="TradingTime"/>.</returns>
        //public static TimeSpan ToTime(this TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int offset = 0)
        //{
        //    switch (instrumentCode)
        //    {
        //        case (InstrumentCode.Default):
        //        case (InstrumentCode.MES):
        //            {
        //                switch (tradingTime)
        //                {

        //                    // MAIN SESSIONS TIMES
        //                    case (TradingTime.Electronic_Open):
        //                    case (TradingTime.American_RS_NWD_Open):
        //                        return instrumentCode.ToMarketExchange().ToElectronicInitialTime() + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.Electronic_Close):
        //                    case (TradingTime.American_RS_EOD_Close):
        //                        return instrumentCode.ToMarketExchange().ToElectronicFinalTime() + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.Regular_Open):
        //                    case (TradingTime.AmericanAndEuropean_Open):
        //                    case (TradingTime.OVN_Close):
        //                        return instrumentCode.ToMarketExchange().ToRegularInitialTime() + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.Regular_Close):
        //                    case (TradingTime.American_Close):
        //                    case (TradingTime.American_RS_Open):
        //                    case (TradingTime.American_RS_EXT_Open):
        //                    case (TradingTime.OVN_Open):
        //                        return instrumentCode.ToMarketExchange().ToRegularFinalTime() + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.Asian_Open):
        //                    case (TradingTime.American_RS_Close):
        //                    case (TradingTime.American_RS_NWD_Close):
        //                        return new TimeSpan(9, 0, 0) + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.Asian_Close):
        //                    case (TradingTime.Asian_RS_Open):
        //                        return new TimeSpan(15, 0, 0);

        //                    case (TradingTime.European_Open):
        //                    case (TradingTime.Asian_RS_Close):
        //                        return new TimeSpan(8, 0, 0) + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.European_Close):
        //                    case (TradingTime.AmericanAndEuropean_Close):
        //                    case (TradingTime.American_Open):
        //                        return new TimeSpan(16, 30, 0) + TimeSpan.FromMinutes(offset);

        //                    case (TradingTime.American_RS_EXT_Close):
        //                        return instrumentCode.ToMarketExchange().ToBreakInitialTime() + TimeSpan.FromMinutes(offset);
        //                    case (TradingTime.American_RS_EOD_Open):
        //                        return instrumentCode.ToMarketExchange().ToBreakFinalTime() + TimeSpan.FromMinutes(offset);
                            
        //                    //case (SpecificSessionTime.American_RS_NWD_IB_Open):
        //                    //    return SpecificSessionTime.Electronic_Open.ToTimeSpan();
        //                    //case (SpecificSessionTime.American_RS_NWD_IB_Close):
        //                    //    return SpecificSessionTime.Electronic_Open.ToTimeSpan() + TimeSpan.FromMinutes(5);
        //                    //case (SpecificSessionTime.American_RS_NWD_BB_Open):
        //                    //    return SpecificSessionTime.Electronic_Open.ToTimeSpan() + TimeSpan.FromMinutes(5) + TimeSpan.FromSeconds(1);
        //                    //case (SpecificSessionTime.American_RS_NWD_BB_Close):
        //                    //    return SpecificSessionTime.Asian_Open.ToTimeSpan() + TimeSpan.FromMinutes(-5);
        //                    //case (SpecificSessionTime.American_RS_NWD_FB_Open):
        //                    //    return SpecificSessionTime.Asian_Open.ToTimeSpan() + TimeSpan.FromMinutes(-5) + TimeSpan.FromSeconds(1);
        //                    //case (SpecificSessionTime.American_RS_NWD_FB_Close):
        //                    //    return SpecificSessionTime.Asian_Open.ToTimeSpan();

        //                    //// Asian minor sessionHoursList
        //                    //case (SpecificSessionTime.Asian_IB_Open):
        //                    //    return new TimeSpan(9, 0, 0);
        //                    //case (SpecificSessionTime.Asian_IB_Close):
        //                    //    return new TimeSpan(9, 15, 0);
        //                    //case (SpecificSessionTime.Asian_BB_Open):
        //                    //    return SpecificSessionTime.Asian_IB_Close.ToTimeSpan();
        //                    //case (SpecificSessionTime.Asian_BB_Close):
        //                    //    return SpecificSessionTime.Asian_FB_Open.ToTimeSpan();
        //                    //case (SpecificSessionTime.Asian_FB_Open):
        //                    //    return new TimeSpan(14, 45, 0);
        //                    //case (SpecificSessionTime.Asian_FB_Close):
        //                    //    return new TimeSpan(15, 0, 0);

        //                    //// American minor sessionHoursList
        //                    //case (SpecificSessionTime.American_IB_Open):
        //                    //    return new TimeSpan(8, 30, 0);
        //                    //case (SpecificSessionTime.American_IB_Close):
        //                    //    return new TimeSpan(9, 30, 0);
        //                    //case (SpecificSessionTime.American_BB_Open):
        //                    //    return SpecificSessionTime.American_IB_Close.ToTimeSpan();
        //                    //case (SpecificSessionTime.American_BB_Close):
        //                    //    return SpecificSessionTime.American_FB_Open.ToTimeSpan();
        //                    //case (SpecificSessionTime.American_FB_Open):
        //                    //    return new TimeSpan(14, 30, 0);
        //                    //case (SpecificSessionTime.American_FB_Close):
        //                    //    return new TimeSpan(15, 0, 0);

        //                    //// American and european minor sessionHoursList
        //                    //case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
        //                    //    return new TimeSpan(8, 30, 0);
        //                    //case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
        //                    //    return new TimeSpan(9, 30, 0);
        //                    //case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
        //                    //    return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToTimeSpan();
        //                    //case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
        //                    //    return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToTimeSpan();
        //                    //case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
        //                    //    return new TimeSpan(10, 15, 0);
        //                    //case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
        //                    //    return new TimeSpan(10, 30, 0);

        //                    //// Eurepan minor sessionHoursList
        //                    //case (SpecificSessionTime.European_IB_Open):
        //                    //    return new TimeSpan(8, 0, 0);
        //                    //case (SpecificSessionTime.European_IB_Close):
        //                    //    return new TimeSpan(8, 15, 0);
        //                    //case (SpecificSessionTime.European_BB_Open):
        //                    //    return SpecificSessionTime.European_IB_Close.ToTimeSpan();
        //                    //case (SpecificSessionTime.European_BB_Close):
        //                    //    return SpecificSessionTime.European_FB_Open.ToTimeSpan();
        //                    //case (SpecificSessionTime.European_FB_Open):
        //                    //    return new TimeSpan(16, 15, 0);
        //                    //case (SpecificSessionTime.European_FB_Close):
        //                    //    return new TimeSpan(16, 30, 0);

        //                    case (TradingTime.Custom):
        //                        throw new Exception("the convert is not possible, the trading time is a custom value.");

        //                    default:
        //                        throw new Exception("The trading time doesn't exists.");

        //                }
        //            }

        //        default:
        //            throw new Exception("The instrument code doesn't exists.");
        //    }
        //}

        /// <summary>
        /// Converts the <see cref="TradingTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="TradingTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingHoursType tradingTimeType, InstrumentCode instrumentKey)
        {
                switch (tradingTimeType)
                {
                    // MARKET EXCHANGE TIME ZONES
                    case (TradingHoursType.Electronic_Open):
                    case (TradingHoursType.Electronic_Close):
                    case (TradingHoursType.Regular_Open):
                    case (TradingHoursType.Regular_Close):
                    case (TradingHoursType.OVN_Open):
                    case (TradingHoursType.OVN_Close):
                        return instrumentKey.ToDefaultTradingHoursKey().ToTimeZoneInfo();

                    // MAJOR SESSIONS TIME ZONES
                    case (TradingHoursType.AmericanAndEuropean_Open):
                    case (TradingHoursType.American_Open):
                    case (TradingHoursType.American_Close):
                    case (TradingHoursType.American_RS_Open):
                    case (TradingHoursType.American_RS_EXT_Open):
                    case (TradingHoursType.American_RS_EXT_Close):
                    case (TradingHoursType.American_RS_EOD_Open):
                    case (TradingHoursType.American_RS_EOD_Close):
                    case (TradingHoursType.American_RS_NWD_Open):
                        return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                    case (TradingHoursType.American_RS_Close):
                    case (TradingHoursType.American_RS_NWD_Close):
                    case (TradingHoursType.Asian_Open):
                    case (TradingHoursType.Asian_Close):
                    case (TradingHoursType.Asian_RS_Open):
                        return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                    case (TradingHoursType.Asian_RS_Close):
                    case (TradingHoursType.European_Open):
                    case (TradingHoursType.European_Close):
                    case (TradingHoursType.AmericanAndEuropean_Close):
                        return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                    case (TradingHoursType.Custom):
                        throw new Exception("the convert is not possible, the session time is a custom value.");

                    default:
                        throw new Exception("The trading time doesn't exists.");

                }
            }

        /// <summary>
        /// Converts the <see cref="TradingHoursType"/> type to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingTimeType"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="TradingHoursType"/>.</returns>
        public static TimeSpan ToTime(this TradingHoursType tradingTimeType, InstrumentCode instrumentKey, int offset = 0)
        {
                switch (tradingTimeType)
                {

                    // MAIN SESSIONS TIMES
                    case (TradingHoursType.Electronic_Open):
                    case (TradingHoursType.American_RS_NWD_Open):
                        return instrumentKey.ToDefaultTradingHoursKey().ToElectronicInitialTime() + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.Electronic_Close):
                    case (TradingHoursType.American_RS_EOD_Close):
                        return instrumentKey.ToDefaultTradingHoursKey().ToElectronicFinalTime() + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.Regular_Open):
                    case (TradingHoursType.American_Open):
                    case (TradingHoursType.AmericanAndEuropean_Open):
                    case (TradingHoursType.OVN_Close):
                        return instrumentKey.ToDefaultTradingHoursKey().ToRegularInitialTime() + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.Regular_Close):
                    case (TradingHoursType.American_Close):
                    case (TradingHoursType.American_RS_Open):
                    case (TradingHoursType.American_RS_EXT_Open):
                    case (TradingHoursType.OVN_Open):
                        return instrumentKey.ToDefaultTradingHoursKey().ToRegularFinalTime() + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.Asian_Open):
                    case (TradingHoursType.American_RS_Close):
                    case (TradingHoursType.American_RS_NWD_Close):
                        return new TimeSpan(9, 0, 0) + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.Asian_Close):
                    case (TradingHoursType.Asian_RS_Open):
                        return new TimeSpan(15, 0, 0);

                    case (TradingHoursType.European_Open):
                    case (TradingHoursType.Asian_RS_Close):
                        return new TimeSpan(8, 0, 0) + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.European_Close):
                    case (TradingHoursType.AmericanAndEuropean_Close):
                        return new TimeSpan(16, 30, 0) + TimeSpan.FromMinutes(offset);

                    case (TradingHoursType.American_RS_EXT_Close):
                        return instrumentKey.ToDefaultTradingHoursKey().ToBreakInitialTime() + TimeSpan.FromMinutes(offset);
                    case (TradingHoursType.American_RS_EOD_Open):
                        return instrumentKey.ToDefaultTradingHoursKey().ToBreakFinalTime() + TimeSpan.FromMinutes(offset);
                            
                    case (TradingHoursType.Custom):
                        throw new Exception("the convert is not possible, the trading time is a custom value.");

                    default:
                        throw new Exception("The trading time doesn't exists.");

                }
            }

        /// <summary>
        /// Returns <see cref="TradingTime"/> by <see cref="TradingHoursType"/>.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <returns><see cref="TradingTime"/>.</returns>
        public static TradingTime ToSessionTime(this TradingHoursType tradingTime, InstrumentCode instrumentCode, int timeDisplacement = 0)
        {
            return TradingTime.CreateSessionTimeByType(tradingTime, instrumentCode, timeDisplacement);
        }
    }
}
