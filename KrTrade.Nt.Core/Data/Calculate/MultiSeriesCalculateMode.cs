namespace KrTrade.Nt.Core.Data
{
    /// <summary>
    /// The calculate mode for one series whe other series is updated.
    /// </summary>
    public enum MultiSeriesCalculateMode
    {
        PrimarySeriesClosed = 0,
        MinorSeriesClosed = 1,
        PreviousSeriesClosed = 2,
        None = 3,
    }
}
