using System;

namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// Extensions methods to converts <see cref="BarsEvent"/> to other objects.
    /// </summary>
    public static class BarsEventExtensions
    {
        /// <summary>
        /// Converts from <see cref="BarsEvent"/> to string.
        /// </summary>
        /// <param name="barsEvent">The type of the string to convert.</param>
        /// <returns>The bars state string.</returns>
        public static string ToLogString(this BarsEvent barsEvent)
        {
            switch (barsEvent)
            {
                case BarsEvent.LastBarRemoved: return "LastBarRemoved";
                case BarsEvent.BarClosed: return "BarClosed";
                case BarsEvent.FirstTick: return "FirstTick";
                case BarsEvent.PriceChanged: return "PriceChanged";
                case BarsEvent.Tick: return "Tick";
                default: return "None";
            }
        }

        /// <summary>
        /// Converts from <see cref="BarsEvent"/> to <see cref="BarsLogLevel"/>.
        /// </summary>
        /// <param name="barsEvent">The type of <see cref="BarsEvent"/> to convert.</param>
        /// <returns>The <see cref="BarsLogLevel"/> that corresponds to the <see cref="BarsEvent"/>.</returns>
        /// <exception cref="NotImplementedException">The type of <see cref="BarsEvent"/> is not implemented.</exception>
        public static BarsLogLevel ToBarsLogLevel(this BarsEvent barsEvent)
        {
            switch (barsEvent)
            {
                case BarsEvent.LastBarRemoved: 
                case BarsEvent.BarClosed: 
                    return BarsLogLevel.BarClosed;
                case BarsEvent.FirstTick: 
                case BarsEvent.PriceChanged: 
                    return BarsLogLevel.PriceChanged;
                case BarsEvent.Tick: 
                    return BarsLogLevel.Tick;
                default: 
                    throw new NotImplementedException(barsEvent.ToString());
            }
        }
    }
}
