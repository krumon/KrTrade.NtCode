using System;

namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// Extensions methods to converts <see cref="BarsState"/> to other objects.
    /// </summary>
    public static class BarsStateExtensions
    {
        /// <summary>
        /// Converts from <see cref="BarsState"/> to string.
        /// </summary>
        /// <param name="barsState">The type of the string to convert.</param>
        /// <returns>The bars state string.</returns>
        public static string ToLogString(this BarsState barsState)
        {
            switch (barsState)
            {
                case BarsState.LastBarRemoved: return "LastBarRemoved";
                case BarsState.BarClosed: return "BarClosed";
                case BarsState.FirstTick: return "FirstTick";
                case BarsState.PriceChanged: return "PriceChanged";
                case BarsState.Tick: return "Tick";
                default: return "None";
            }
        }

        /// <summary>
        /// Converts from <see cref="BarsState"/> to <see cref="BarsLogLevel"/>.
        /// </summary>
        /// <param name="barsState">The type of <see cref="BarsState"/> to convert.</param>
        /// <returns>The <see cref="BarsLogLevel"/> that corresponds to the <see cref="BarsState"/>.</returns>
        /// <exception cref="NotImplementedException">The type of <see cref="BarsState"/> is not implemented.</exception>
        public static BarsLogLevel ToLogLevel(this BarsState barsState)
        {
            switch (barsState)
            {
                case BarsState.LastBarRemoved: 
                case BarsState.BarClosed: 
                    return BarsLogLevel.BarClosed;
                case BarsState.FirstTick: 
                case BarsState.PriceChanged: 
                    return BarsLogLevel.PriceChanged;
                case BarsState.Tick: 
                    return BarsLogLevel.Tick;
                default: 
                    throw new NotImplementedException(barsState.ToString());
            }
        }
    }
}
