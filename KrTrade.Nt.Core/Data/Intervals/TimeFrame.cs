namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// The time frame of the data series.
    /// </summary>
    public enum TimeFrame
    {
        /// <summary>
        /// The same time frame of the primary bars.
        /// </summary>
        Default,
        /// <summary>
        /// 1 tick data frame.
        /// </summary>
        t1,
        /// <summary>
        /// 150 ticks data frame.
        /// </summary>
        t150,
        /// <summary>
        /// 15 seconds data frame.
        /// </summary>
        s15,
        /// <summary>
        /// 1 minute data frame.
        /// </summary>
        m1,
        /// <summary>
        /// 5 minutes data frame.
        /// </summary>
        m5,
        /// <summary>
        /// 15 minutes data frame.
        /// </summary>
        m15,
        /// <summary>
        /// 30 minutes data frame.
        /// </summary>
        m30,
        /// <summary>
        /// 1 hour data frame.
        /// </summary>
        h1,
        /// <summary>
        /// 4 hours data frame.
        /// </summary>
        h4,
        /// <summary>
        /// 1 day data frame.
        /// </summary>
        d1,
        /// <summary>
        /// 1 week data frame.
        /// </summary>
        w1
    }
}
