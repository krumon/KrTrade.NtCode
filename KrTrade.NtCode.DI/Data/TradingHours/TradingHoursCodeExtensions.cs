using System;

namespace KrTrade.NtCode.Data
{

    /// <summary>
    /// Helper methods of <see cref="TradingHoursCode"/> enum.
    /// </summary>
    public static class TradingHoursCodeExtensions
    {
        /// <summary>
        /// Converts a <see cref="TradingHoursCode"/> to name.
        /// </summary>
        /// <param name="tradingHoursCode">The trading hours key.</param>
        /// <returns>The trading hours name.</returns>
        public static string ToName(this TradingHoursCode tradingHoursCode)
        {
            string name = tradingHoursCode.ToString().Replace('_',' ');

            return name;
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursCode"/> to description.
        /// </summary>
        /// <param name="tradingHoursCode">The instrument code.</param>
        /// <returns>The market exchange description.</returns>
        public static string ToDescription(this TradingHoursCode tradingHoursCode)
        {
            // TODO: Rectificar.
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return "American Future Index market exchange.";
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursCode"/> to <see cref="TimeZoneInfo"/>
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeZoneInfo"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursCode"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicInitialTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(17, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursCode"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicFinalTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(16, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursCode"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularInitialTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(8, 30, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursCode"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularFinalTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursCode"/> to initial break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakInitialTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 15, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursCode"/> to final break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakFinalTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 30, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursCode"/> to initial closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedInitialTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return tradingHoursCode.ToElectronicFinalTime();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursCode"/> to final closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="tradingHoursCode">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedFinalTime(this TradingHoursCode tradingHoursCode)
        {
            switch (tradingHoursCode)
            {
                case (TradingHoursCode.CME_US_Index_Futures_ETH):
                    return tradingHoursCode.ToElectronicInitialTime();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }


    }
}
