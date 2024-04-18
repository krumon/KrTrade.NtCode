namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a stats service.
    /// </summary>
    public interface IStatsService : IBarUpdateService<StatsInfo,StatsOptions> 
    {
        //BaseSeriesCache Value { get; }
        //BaseSeriesCache Max { get; }
        //BaseSeriesCache Min { get; }
        //BaseSeriesCache Sum { get; }
        //BaseSeriesCache Avg { get; }
        //BaseSeriesCache StdDev { get; }
    }

}
