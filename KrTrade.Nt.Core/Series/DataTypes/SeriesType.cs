namespace KrTrade.Nt.Core.Series
{
    /// <summary>
    /// The type of the 'NinjaScript.ISeries'.
    /// </summary>
    public enum SeriesType
    {
        // UNKNOWN TYPE
        UNKNOWN,
        // BARS SERIES
        CURRENT_BAR,
        TIME,
        VOLUME,
        TICK,
        // PRICE SERIES
        INPUT,
        OPEN,
        HIGH,
        LOW,
        CLOSE,
        // STATS SERIES
        MAX,
        MIN,
        SUM,
        AVG,
        DEVSTD,
        // INDICATOR SERIES
        RANGE,


        SWING_HIGH,
        SWING_LOW



        //MEDIAN,
        //TYPICAL,
        //WEIGHTED,
    }
}
