namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// The type of the 'NinjaScript.ISeries'.
    /// </summary>
    public enum SeriesType
    {
        UNKNOWN,
        //SERIES TYPE
        MEDIAN,
        TYPICAL,
        WEIGHTED,
        // BARS SERIES
        CURRENT_BAR,
        TIME,
        VOLUME,
        TICK,
        INPUT,
        OPEN,
        HIGH,
        LOW,
        CLOSE,
        // PERIOD SERIES
        MAX,
        MIN,
        SUM,
        AVG,
        DEVSTD,
        RANGE,
        // STRENGTH SERIES
        SWING_HIGH,
        SWING_LOW,
    }
}
