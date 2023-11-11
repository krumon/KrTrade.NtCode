namespace KrTrade.Nt.Core.Bars
{
    /// <summary>
    /// The type of bars pattern. This pattern has more than one bar.
    /// </summary>
    public enum BarsPattern
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
    }
}
