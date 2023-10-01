namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// The type pattern of the chart.
    /// </summary>
    public enum ChartPattern
    {

        /// <summary>
        /// Represents the swing points based on the strength (number of bars to the left and right of the swing point). 
        /// Only after the strength number of bars has passed since the extreme point,the swing return value could be definitely set.
        /// </summary>
        Swing,

        /// <summary>
        /// Represents the swing points based on the strength (number of bars to the left and right of the swing point). 
        /// Only after the strength number of bars has passed since the extreme point,the swing return value could be definitely set.
        /// </summary>
        SwingHigh,

        /// <summary>
        /// Represents the swing points based on the strength (number of bars to the left and right of the swing point). 
        /// Only after the strength number of bars has passed since the extreme point,the swing return value could be definitely set.
        /// </summary>
        SwingLow,

        Marubozu, // Vela larga casi sin mecha

        SppinningTops, // Peonza, trompo

    }
}
