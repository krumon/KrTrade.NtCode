using System;

namespace KrTrade.Nt.Core.Data
{

    /// <summary>
    /// Helper methods of <see cref="InstrumentCode"/> enum.
    /// </summary>
    public static class InstrumentCodeExtensions
    {
        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to string.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The instrument name.</returns>
        public static string ToName(this InstrumentCode instrumentCode) => instrumentCode.ToString();

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to description.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The instrument description.</returns>
        public static string ToDescription(this InstrumentCode instrumentCode)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return "MICRO E-MINI S&P 500 INDEX FUTURES";
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to <see cref="MarketExchange"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The <see cref="MarketExchange"/> value.</returns>
        public static MarketExchange ToMarketExchange(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return MarketExchange.CME;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to default <see cref="TradingHoursCode"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument key.</param>
        /// <returns>The <see cref="TradingHoursCode"/> value.</returns>
        public static TradingHoursCode ToDefaultTradingHoursKey(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return TradingHoursCode.CME_US_Index_Futures_ETH;
                case (InstrumentCode.M6A):
                case (InstrumentCode.M6B):
                case (InstrumentCode.M6E):
                    return TradingHoursCode.CME_FX_Futures_ETH;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to default <see cref="TradingHoursCode"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument key.</param>
        /// <returns>The <see cref="TradingHoursCode"/> value.</returns>
        public static TradingHoursCode ToTradingHoursKey(this InstrumentCode instrumentCode, string tradingHoursName)
        {
            if (Enum.TryParse(tradingHoursName, out TradingHoursCode key))
                return key;

            return TradingHoursCode.Default;
        }

        /// <summary>
        /// Converts a string to <see cref="InstrumentCode"/> enum.
        /// </summary>
        /// <param name="key">The string to converts to enum.</param>
        /// <returns>The <see cref="InstrumentCode"/> enum.</returns>
        /// <exception cref="Exception">Returns a exception if the string cannot be convert.</exception>
        public static bool TryGetInstrumentKey(this string key, out InstrumentCode instrumentCode)
        {
            if (Enum.TryParse(key, out instrumentCode))
                return true;
            return false;
            //throw new Exception("The string 'key' cannot be convert to InstrumentKey enum.");
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to <see cref="MarketExchange"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The <see cref="MarketExchange"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return instrumentCode.ToDefaultTradingHoursKey().ToTimeZoneInfo();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="InstrumentCode"/> to instrument tick size.
        /// </summary>
        /// <param name="instrumentCode">The instrument key.</param>
        /// <returns>The instrument tick size value.</returns>
        /// <exception cref="Exception">Throw an error if converter is not implemented yet.</exception>
        public static double ToTickSize(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return 0.25;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="InstrumentCode"/> to instrument tick size.
        /// </summary>
        /// <param name="instrumentCode">The instrument key.</param>
        /// <returns>The instrument tick size value.</returns>
        /// <exception cref="Exception">Throw an error if converter is not implemented yet.</exception>
        public static double ToPointValue(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.MES):
                    return 5.0;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts the string to <see cref="InstrumentCode"/>.
        /// </summary>
        /// <param name="instrumentName">The instrument name.</param>
        /// <returns>The instrument code value.</returns>
        /// <exception cref="Exception">Throw an error if converter is not implemented yet.</exception>
        public static InstrumentCode ToInstrumentCode(this string instrumentName)
        {
            instrumentName = instrumentName.Trim().ToUpper();
            switch (instrumentName)
            {
                case ("MES"):
                    return InstrumentCode.MES;
                case ("ES"):
                    return InstrumentCode.ES;
                case ("EMD"):
                    return InstrumentCode.EMD;
                case ("MNQ"):
                    return InstrumentCode.MNQ;
                case ("NQ"):
                    return InstrumentCode.NQ;
                case ("RTY"):
                    return InstrumentCode.RTY;
                case ("M2K"):
                    return InstrumentCode.M2K;
                case ("BTC"):
                    return InstrumentCode.BTC;
                case ("MBT"):
                    return InstrumentCode.MBT;
                case ("MET"):
                    return InstrumentCode.MET;
                case ("GE"):
                    return InstrumentCode.GE;
                case ("GF"):
                    return InstrumentCode.GF;
                case ("HE"):
                    return InstrumentCode.HE;
                case ("LE"):
                    return InstrumentCode.LE;
                case ("_6A"):
                    return InstrumentCode._6A;
                case ("M6A"):
                    return InstrumentCode.M6A;
                case ("_6B"):
                    return InstrumentCode._6B;
                case ("M6B"):
                    return InstrumentCode.M6B;
                case ("_6C"):
                    return InstrumentCode._6C;
                case ("MICD"):
                    return InstrumentCode.MICD;
                case ("_6E"):
                    return InstrumentCode._6E;
                case ("E7"):
                    return InstrumentCode.E7;
                case ("M6E"):
                    return InstrumentCode.M6E;
                case ("_6J"):
                    return InstrumentCode._6J;
                case ("J7"):
                    return InstrumentCode.J7;
                case ("_6M"):
                    return InstrumentCode._6M;
                case ("_6N"):
                    return InstrumentCode._6N;
                case ("_6S"):
                    return InstrumentCode._6S;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }


    }
}
