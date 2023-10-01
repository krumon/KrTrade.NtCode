namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// The type of the bar.
    /// </summary>
    public enum BarType
    {

        /// <summary>
        /// The close price of the bar is greater than open price.
        /// </summary>
        Bullish,

        /// <summary>
        /// The open price of the bar is greater than close price.
        /// </summary>
        Bearish,

        /// <summary>
        /// The open price of the bar is equal than close price.
        /// </summary>
        Neutral,

    }
}
