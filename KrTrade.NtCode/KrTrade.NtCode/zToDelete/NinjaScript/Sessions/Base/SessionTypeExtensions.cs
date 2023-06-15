using Nt.Core.Data;
using Nt.Core.Data.Internal;
using System;
using System.Linq;

namespace KrTrade.NtCode
{

    /// <summary>
    /// <see cref="SessionType"/> enum helper methods.
    /// </summary>
    public static class SessionTypeExtensions
    {

        /// <summary>
        /// Returns the unique code of the <see cref="SessionType"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the unique code of the <see cref="SessionType"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionType"/> doesn´t exists.</exception>
        public static string ToCode(this SessionType tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case SessionType.Electronic:
                    return "EL";
                case SessionType.Regular:
                    return "RG";
                case SessionType.OVN:
                    return "OV";
                case SessionType.American:
                    return "AM";
                case SessionType.AmericanAndEuropean:
                    return "AE";
                case SessionType.Asian:
                    return "AS";
                case SessionType.European:
                    return "EU";
                case SessionType.American_RS:
                    return "AM-RS";
                case SessionType.Asian_RS:
                    return "AS-RS";
                case SessionType.American_RS_EXT:
                    return "AM-RS-EXT";
                case SessionType.American_RS_EOD:
                    return "AM-RS-EOD";
                case SessionType.American_RS_NWD:
                    return "AM-RS-NWD";

                default:
                    throw new Exception("The specific trading session doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="SessionType"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the description of the <see cref="SessionType"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionType"/> doesn´t exists.</exception>
        public static string ToDescription(this SessionType tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case SessionType.Electronic:
                    return "Electronic TradingSession.";
                case SessionType.Regular:
                    return "Regular TradingSession.";
                case SessionType.OVN:
                    return "Overnight TradingSession.";
                case SessionType.American:
                    return "American TradingSession.";
                case SessionType.AmericanAndEuropean:
                    return "American and European TradingSession.";
                case SessionType.Asian:
                    return "Asian TradingSession.";
                case SessionType.European:
                    return "Asian TradingSession.";
                case SessionType.American_RS:
                    return "American Residual TradingSession.";
                case SessionType.Asian_RS:
                    return "Asian Residual TradingSession.";
                case SessionType.American_RS_EXT:
                    return "American Residual Extra time TradingSession.";
                case SessionType.American_RS_EOD:
                    return "American Residual End Of Day TradingSession.";
                case SessionType.American_RS_NWD:
                    return "American Residual New Day TradingSession.";

                default:
                    return "TradingSession Custom";
            }
        }

        /// <summary>
        /// Returns the name of the <see cref="SessionType"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the name of the <see cref="SessionType"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionType"/> doesn´t exists.</exception>
        public static string ToName(this SessionType tradingSessionType)
        {
            switch (tradingSessionType)
            {
                case SessionType.Electronic:
                    return "Electronic";
                case SessionType.Regular:
                    return "Regular";
                case SessionType.OVN:
                    return "Overnight";
                case SessionType.American:
                    return "American";
                case SessionType.AmericanAndEuropean:
                    return "AmericanAndEuropean";
                case SessionType.Asian:
                    return "Asian";
                case SessionType.European:
                    return "European";
                case SessionType.American_RS:
                    return "AmericanResidual";
                case SessionType.Asian_RS:
                    return "AsianResidual";
                case SessionType.American_RS_EXT:
                    return "AmericanResidualExtratime";
                case SessionType.American_RS_EOD:
                    return "AmericanResidualEndOfDay";
                case SessionType.American_RS_NWD:
                    return "AmericanResidualNewDay";

                default:
                    return "Custom";
            }
        }

        /// <summary>
        /// Converts the <see cref="SessionType"/> to initial <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Initial <see cref="TradingTime"/> of the <see cref="SessionType"/>.</returns>
        public static TradingTime ToBeginSessionTime(this SessionType tradingSessionType, InstrumentCode instrumentKey, int offset = 0)
        {
            switch (instrumentKey)
            {
                case (InstrumentCode.MES):
                {

                    switch (tradingSessionType)
                    {
                        case (SessionType.Electronic):
                            return TradingTimeType.Electronic_Open.ToSessionTime(instrumentKey,offset);
                        case (SessionType.Regular):
                            return TradingTimeType.Regular_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.OVN):
                            return TradingTimeType.OVN_Open.ToSessionTime(instrumentKey, offset);

                        case (SessionType.American):
                            return TradingTimeType.American_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.AmericanAndEuropean):
                            return TradingTimeType.AmericanAndEuropean_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.Asian):
                            return TradingTimeType.Asian_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.European):
                            return TradingTimeType.European_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.American_RS):
                            return TradingTimeType.American_RS_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.Asian_RS):
                            return TradingTimeType.Asian_RS_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.American_RS_EXT):
                            return TradingTimeType.American_RS_EXT_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.American_RS_EOD):
                            return TradingTimeType.American_RS_EOD_Open.ToSessionTime(instrumentKey, offset);
                        case (SessionType.American_RS_NWD):
                            return TradingTimeType.American_RS_NWD_Open.ToSessionTime(instrumentKey, offset);

                        default:
                            throw new Exception("The trading session doesn't exists.");
                    }
                }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SessionType"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="SessionType"/>.</returns>
        public static TradingTime ToEndSessionTime(this SessionType tradingSessionType, InstrumentCode instrumentKey, int offset = 0)
        {
            switch (instrumentKey)
            {
                case (InstrumentCode.MES):
                    {

                        switch (tradingSessionType)
                        {
                            case (SessionType.Electronic):
                                return TradingTimeType.Electronic_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.Regular):
                                return TradingTimeType.Regular_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.OVN):
                                return TradingTimeType.OVN_Close.ToSessionTime(instrumentKey, offset);

                            case (SessionType.American):
                                return TradingTimeType.American_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.AmericanAndEuropean):
                                return TradingTimeType.AmericanAndEuropean_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.Asian):
                                return TradingTimeType.Asian_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.European):
                                return TradingTimeType.European_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.American_RS):
                                return TradingTimeType.American_RS_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.Asian_RS):
                                return TradingTimeType.Asian_RS_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.American_RS_EXT):
                                return TradingTimeType.American_RS_EXT_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.American_RS_EOD):
                                return TradingTimeType.American_RS_EOD_Close.ToSessionTime(instrumentKey, offset);
                            case (SessionType.American_RS_NWD):
                                return TradingTimeType.American_RS_NWD_Close.ToSessionTime(instrumentKey, offset);

                            default:
                                throw new Exception("The trading session doesn't exists.");
                        }
                    }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static TradingSession ToSessionHours(this SessionType type, InstrumentCode instrumentCode, int balanceMinutes = 0)
        {
            return TradingSession.CreateTradingSessionByType(type,instrumentCode,balanceMinutes);
        }

        public static SessionType[] ToArray(this SessionType type)
        {
            return Enum.GetValues(typeof(SessionType)).Cast<SessionType>().ToArray();
        }

    }
}
