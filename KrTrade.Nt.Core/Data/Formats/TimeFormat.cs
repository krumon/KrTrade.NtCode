namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// DateTime format type.
    /// </summary>
    public enum TimeFormat
    {
        /// <summary>
        /// DateTime format based on minute time frame.
        /// </summary>
        Minute,
        /// <summary>
        /// DateTime format based on hour time frame.
        /// </summary>
        Hour,
        /// <summary>
        /// DateTime format based on day time frame.
        /// </summary>
        Day,
        /// <summary>
        /// DateTime format based on seconds time frame.
        /// </summary>
        Second,
        /// <summary>
        /// DateTime format based on tick time frame.
        /// </summary>
        Millisecond,
    }
}
