using System;

namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// Extensions methods to converts <see cref="BarEvent"/> to other objects.
    /// </summary>
    public static class BarEventExtensions
    {
        /// <summary>
        /// Converts from <see cref="BarEvent"/> to string.
        /// </summary>
        /// <param name="barsEvent">The type of the string to convert.</param>
        /// <returns>The bars state string.</returns>
        public static string ToLogString(this BarEvent barsEvent)
        {
            switch (barsEvent)
            {
                case BarEvent.Removed: return "LastBarRemoved";
                case BarEvent.Closed: return "BarClosed";
                case BarEvent.FirstTick: return "FirstTick";
                case BarEvent.PriceChanged: return "PriceChanged";
                case BarEvent.Tick: return "Tick";
                default: return "None";
            }
        }

        /// <summary>
        /// Converts from <see cref="BarEvent"/> to <see cref="BarsLogLevel"/>.
        /// </summary>
        /// <param name="barsEvent">The type of <see cref="BarEvent"/> to convert.</param>
        /// <returns>The <see cref="BarsLogLevel"/> that corresponds to the <see cref="BarEvent"/>.</returns>
        /// <exception cref="NotImplementedException">The type of <see cref="BarEvent"/> is not implemented.</exception>
        public static BarsLogLevel ToBarsLogLevel(this BarEvent barsEvent)
        {
            switch (barsEvent)
            {
                case BarEvent.Removed: 
                case BarEvent.Closed: 
                    return BarsLogLevel.BarClosed;
                case BarEvent.FirstTick: 
                case BarEvent.PriceChanged: 
                    return BarsLogLevel.PriceChanged;
                case BarEvent.Tick: 
                    return BarsLogLevel.Tick;
                default: 
                    throw new NotImplementedException(barsEvent.ToString());
            }
        }
    }
}
