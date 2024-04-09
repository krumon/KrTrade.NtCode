namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// The type of the 'NinjaScript.ISeries'.
    /// </summary>
    public enum SeriesType
    {
        // BARS SERIES
        INDEX,
        TIME,
        VOLUME,
        TICK,
        // PRICE SERIES
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
