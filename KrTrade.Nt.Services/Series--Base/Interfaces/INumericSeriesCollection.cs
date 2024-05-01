namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Defines methods that are necesary to construct a series collection.
    /// </summary>
    public interface INumericSeriesCollection<TSeries> : INinjascriptSeriesCollection<TSeries>
        where TSeries : INumericSeries
    {
    }

}
