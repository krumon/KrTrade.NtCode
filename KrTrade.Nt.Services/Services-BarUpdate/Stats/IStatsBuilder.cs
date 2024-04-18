namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IStatsService"/> objects. 
    /// </summary>
    public interface IStatsBuilder : IBarUpdateBuilder<IStatsService,StatsInfo,StatsOptions, IStatsBuilder>
    {
    }
}
